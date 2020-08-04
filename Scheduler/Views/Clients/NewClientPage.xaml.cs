using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scheduler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class NewClientPage : ContentPage
    {
        public NewClientPage()
        {
            InitializeComponent();
        }
    }
}