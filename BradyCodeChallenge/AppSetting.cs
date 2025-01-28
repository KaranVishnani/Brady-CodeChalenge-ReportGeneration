using System.Configuration;

namespace BradyCodeChallenge
{
    public static class AppSetting
    {
        public const string InputFilePath = "input";
        public const string outputFilePath = "output";
        public const string InputFileFilter = "inputFileRegexPattern";
        public const string ReferenceDataFile = "referenceData";

        public static string GetAppSettingValue(string key)
        {
            string? value = string.Empty;
            if (string.IsNullOrEmpty(key))
            {
                throw new KeyNotFoundException($"A key:{key} not present in appsettings config.");
            }

            value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception($"Empty value for key:{key} in appsettings config.");
            }

            return Environment.ExpandEnvironmentVariables(value);
        }
    }
}
