using BradyCodeChallenge.FileParsers;
using BradyCodeChallenge.FileMonitors;
using BradyCodeChallenge.Models.GenerationInputModels;
using BradyCodeChallenge.Models.GenerationOutputModels;
using BradyCodeChallenge.Models.ReferenceDataModels;
using BradyCodeChallenge.ReportCalculations;
using BradyCodeChallenge.Models.GenerationInputModels.EnergyGenerator;
using log4net;

namespace BradyCodeChallenge.Models
{
    internal class Program
    {
        private static readonly ILog log = LoggerFactory.GetLogger();
        private readonly string _inputFileDirectory;
        private readonly string _outputFileDirectory;
        private readonly string _inputFileFilter;
        private readonly string _referenceDataFile;

        private string _inputFileName = string.Empty;
        private IFileMonitor _fileMonitor;
        private IFileParser _fileParser;

        private readonly ReferenceData _referenceData;
        private FileProcessor _processor;
        private GenerationReport _report;
        private GenerationOutput _reportOutput;

        private static readonly Dictionary<string, string> GeneratorTypeToValueFactorMap = new()
        {
            {"Offshore Wind", "Low" }, 
            {"Onshore Wind", "High" }, 
            {"Gas", "Medium" }, 
            {"Coal", "Medium" },
        };

        private Dictionary<string, string> GeneratorTypeToEmissionFactorMap = new()
        {
            {"Offshore Wind", "N/A" },
            {"Onshore Wind", "N/A" },
            {"Gas", "Medium" },
            {"Coal", "High" },
        };

        public Program()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Handle);

            _inputFileDirectory = AppSetting.GetAppSettingValue(AppSetting.InputFilePath);
            _outputFileDirectory = AppSetting.GetAppSettingValue(AppSetting.outputFilePath);
            _inputFileFilter = AppSetting.GetAppSettingValue(AppSetting.InputFileFilter);
            _referenceDataFile = AppSetting.GetAppSettingValue(AppSetting.ReferenceDataFile);
            
            InitialiseFields();
        }

        public static void Main(string[] args)
        {
            try
            {
                log.Info("Starting ReportGenerator process.");
                new Program().Run();

                Console.WriteLine("Press 'q' to quit");
                while (Console.Read() != 'q') ;

                log.Info("ReportGenerator process completed.");
            }
            catch (Exception ex)
            {
                log.Fatal("ReportGenerator process completed with an exception.", ex);
            }
        }

        private void Run()
        {
            StartFileMonitoring();
        }

        private void InitialiseFields()
        {
            _report = new GenerationReport();
            _reportOutput = new GenerationOutput();

            _fileParser = GetFileParserObject();
            _processor = new FileProcessor(_fileParser, log);

            InitialiseFileMonitor();

            InitialiseReferenceData();
        }

        private void StartFileMonitoring()
        {
            log.Info("starting File Monitoring.");
            _fileMonitor.Start();

            // TODO: Purpose of this line is testing the complete process of report generation, otherwise will keep on waiting for file.
            // MUST NOT BE PRESENT ON PROD.
            // File.Copy(".\\ReferenceData\\01-Basic.xml", "C:\\BradyCodeChallenge\\input\\01-Basic.xml", true);
        }

        private void OnFileCreated(object sender, FileSystemEventArgs args)
        {
            _inputFileName = args.FullPath;
            log.InfoFormat("File captured: {0}.", _inputFileName);

            ResetReportObject();

            LoadReportData(_inputFileName);

            GenerateReportOutput(_report);

            SaveReportOutputToFile();
        }

        private void ResetReportObject()
        {
            _reportOutput = new GenerationOutput();
            _report = new GenerationReport();
        }

        private void InitialiseFileMonitor()
        {
            _fileMonitor = new FileSystemWatcherMonitor(_inputFileDirectory, _inputFileFilter, log);
            _fileMonitor.FileCreated += OnFileCreated;
        }
        private IFileParser GetFileParserObject()
        {
            return new XmlFileParser(log);
        }

        private void InitialiseReferenceData()
        {
            log.Debug("Loading ReferenceData.");
            _referenceData = _processor.LoadFile<ReferenceData>(_referenceDataFile) ??
                throw new Exception($"ReferenceData object is null.");
        }

        private void LoadReportData(string inputFile)
        {
            log.Info("Starting loading Report input data.");
            _report = _processor.LoadFile<GenerationReport>(inputFile) ??
                throw new Exception($"could not load GenerationReport.");
        }

        private void GenerateReportOutput(GenerationReport report)
        {
            log.Info("Generating Result Report.");
            IEnergyCalculator totalGenerationValueCalculator = new TotalGenerationValueCalculator(_reportOutput, _referenceData.Factors[0], log);
            IEmissionEnergyCalculator highestDailyEmissionsCalculator = new HighestDailyEmissionsCalculator(_reportOutput, _referenceData.Factors[1], log);
            IEmissionEnergyCalculator actualHeatRateCalculator = new ActualHeatRateCalculator(_reportOutput, log);

            foreach (var energyGenerator in report.GetEnergies())
            {
                log.InfoFormat("Generating result report data for type: {0}.", energyGenerator.GetType().Name);
                switch (energyGenerator)
                {
                    case WindGenerator wind:
                        wind.Accept(totalGenerationValueCalculator, GetValueFactor(wind));
                        break;

                    case GasGenerator gas:
                        gas.Accept(totalGenerationValueCalculator, GetValueFactor(gas));
                        gas.Accept(highestDailyEmissionsCalculator, GetEmissionFactor(gas));
                        break;

                    case CoalGenerator coal:
                        coal.Accept(totalGenerationValueCalculator, GetValueFactor(coal));
                        coal.Accept(highestDailyEmissionsCalculator, GetEmissionFactor(coal));
                        coal.Accept(actualHeatRateCalculator);
                        break;
                }
            }
        }

        private void SaveReportOutputToFile()
        {
            log.Info("Saving Result Report.");
            _processor.SaveResultReport(GetOutputFileFullName(), _reportOutput);
            log.Info("Result Report generated.");
        }

        private string GetOutputFileFullName()
        {
            return _outputFileDirectory + "\\" + Path.GetFileNameWithoutExtension(_inputFileName) + "-Result" + Path.GetExtension(_inputFileName);
        }

        private string GetValueFactor(EnergyGeneratorBase energy)
        {
            return GeneratorTypeToValueFactorMap.Where(x => x.Key.Contains(EnumHelper.GetStringValue(energy.Type))).FirstOrDefault().Value;
        }

        private string GetEmissionFactor(EnergyGeneratorBase energy)
        {
            return GeneratorTypeToEmissionFactorMap.Where(x => x.Key.Contains(EnumHelper.GetStringValue(energy.Type))).FirstOrDefault().Value;
        }

        private static void Handle(object sender, UnhandledExceptionEventArgs args)
        {
            Exception ex = (Exception)args.ExceptionObject;

            log.Fatal("Unhandled excepion caught.", ex);
        }
    }
}