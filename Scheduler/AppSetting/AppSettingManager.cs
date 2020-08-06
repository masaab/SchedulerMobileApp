using Newtonsoft.Json;
using System;
using System.IO;
using Xamarin.Essentials;

namespace Scheduler.AppSetting
{
    public class AppSettingManager : IApplicationSettingManager
    {
        private string FileName { get; set; }
        public AppSettingDetail AppSettingDetails { get; }
        public AppSettingManager()
        {
#if Debug
            {
                FileName = "appsetting.dev";
             
            }
#else
            {
               FileName = "appsetting.pro";
             }
#endif
            AppSettingDetails = ReadAppSettingFile();

        }
        private AppSettingDetail ReadAppSettingFile()
        {
            try
            {
               // var da = this.GetType().Assembly.GetManifestResourceNames();
               // var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AppSettingManager)).Assembly;
                var stream = this.GetType().Assembly.GetManifestResourceStream($"Scheduler.{FileName}");
                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    var jsonSerializer = new JsonSerializer();
                    return JsonConvert.DeserializeObject<AppSettingDetail>(json);
                }
            }
            catch (Exception ex)
            {
            }
            throw new NotImplementedException();
        }
    }
}
