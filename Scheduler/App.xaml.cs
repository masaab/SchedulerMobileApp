using Xamarin.Essentials;
using Xamarin.Forms;
using Scheduler.Bootstrap;
using System.Threading.Tasks;
using Scheduler.Managers.Abstraction;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Scheduler
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address

        //Azure url: https://schedulerapi.azurewebsites.net
        public static string AzureBackendUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
       //
       // public static bool UseMockDataStore = false;

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjkzOTMxQDMxMzgyZTMyMmUzMGlreXVYNTY2S3QwaGtsalBCY3V3ZDJ5RWhrdjBGZEVNN3NEdU5MT1A3ZDA9");
            InitializeComponent();
            InitializeApp();
            InitializeNavigation().ConfigureAwait(false);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start("android=c9843a95-617f-4cba-ac00-a6f0804184ab;", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void InitializeApp()
        {
            AppContainer.RegisterDependencies();
        }

        private async Task InitializeNavigation()
        {
            var navigationManager = AppContainer.Resolve<INavigationManager>();
            await navigationManager.InitializeAsync();
        }
    }
}
