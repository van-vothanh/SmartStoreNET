using System;
using System.Collections.Generic;
using System.IO;

namespace SmartStore.Core.Data
{
    public partial class DataSettings
    {
        public DataSettings()
        {
            RawDataSettings = new Dictionary<string, string>();
        }

        public string DataProvider { get; set; }
        public string DataConnectionString { get; set; }
        public IDictionary<string, string> RawDataSettings { get; private set; }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(this.DataProvider) && !String.IsNullOrEmpty(this.DataConnectionString);
        }

        public static string SettingsFilePath
        {
            get
            {
                return Path.Combine(CommonHelper.MapPath("~/App_Data/"), "Settings.txt");
            }
        }
    }
}
