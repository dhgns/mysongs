using Xamarin.Forms;

namespace MySongs.Services
{
    public interface INavigationService
    {
        void NavigateTo(Page nextPage);
        void NavigateBack();
    }
}
