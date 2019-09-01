using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;

namespace MySongs.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage, IBasePage<MainViewModel>
    {
        public ResultPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            this.BindingContext = App.viewModel;
            App.Current.MainPage.ToolbarItems.Clear();
            base.OnAppearing();
        }
    }
}
