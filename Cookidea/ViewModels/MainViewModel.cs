using Acr.UserDialogs;
using Cookidea.Models;
using Cookidea.Services;
using Cookidea.Views;
using Plugin.Connectivity;
using QuickType;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;
using Xamvvm;

namespace Cookidea
{
    public class MainViewModel : BasePageModel
    {
        #region Properties
        private ObservableCollection<Song> _Songs;
        public ObservableCollection<Song> Songs
        {
            get { if (this._Songs == null) this._Songs = new ObservableCollection<Song>(); return this._Songs; }
            set { this.SetField(ref this._Songs, value); }
        }

        private ObservableCollection<Song> _favSongs;
        public ObservableCollection<Song> FavSongs
        {
            get { if (this._favSongs == null) this._favSongs = new ObservableCollection<Song>(); return this._favSongs; }
            set { this.SetField(ref this._favSongs, value); }
        }

        private string _title;
        public string Title
        {
            get { return this._title; }
            set { this.SetField(ref this._title, value); }
        }

        private string _link;
        public string Link
        {
            get { return this._link; }
            set { this.SetField(ref this._link, value); }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get { return this._imageUrl; }
            set { this.SetField(ref this._imageUrl, value); }
        }

        private string _SongUrl;
        public string SongUrl
        {
            get { return this._SongUrl; }
            set { this.SetField(ref this._SongUrl, value); }
        }

        private string _touchedSongUrl;
        public string TouchedSongUrl
        {
            get { return this._touchedSongUrl; }
            set { this.SetField(ref this._touchedSongUrl, value); }
        }

        private Song _touchedSong;
        public Song TouchedSong
        {
            get { return this._touchedSong; }
            set { this.SetField(ref this._touchedSong, value); }
        }

        private Query _query;
        public Query Query
        {
            get { return this._query; }
            set { this.SetField(ref this._query, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return this._isBusy; }
            set { this.SetField(ref this._isBusy, value); }
        }

        private bool _isFavSongsEmpty;
        public bool IsFavSongsEmpty
        {
            get { return this._isFavSongsEmpty; }
            set { this.SetField(ref this._isFavSongsEmpty, value); }
        }

        private string _entryTitleText;
        public string EntryTitleText
        {
            get { return this._entryTitleText; }
            set { this.SetField(ref this._entryTitleText, value); }
        }

        public ICommand ItemTappedCommand
        {
            get { return GetField<ICommand>(); }
            set { SetField(value); }
        }

        public ICommand CmdFavMainTapped
        {
            get { return GetField<ICommand>(); }
            set { SetField(value); }
        }

        public ICommand CmdBtnSearchClicked
        {
            get { return GetField<ICommand>(); }
            set { SetField(value); }
        }

        public object LastTappedItem
        {
            get { return GetField<object>(); }
            set { SetField(value); }
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            IsBusy = false;

            CmdBtnSearchClicked = new BaseCommand(param => BtnSearchClicked());

            ItemTappedCommand = new BaseCommand(param => ItemTapped());
        }
        #endregion

        #region Methods
        public async void SearchSongsAsync(string title)
        {
            this.IsBusy = true;
            this.Songs.Clear();

            this.Query = await Services.DownloadService.SearchSongsAsync(title);
            if(this.Query != null)
            {
                this.Songs = new ObservableCollection<Song>(this.Query.Songs);
                if (this.Songs.Count == 0)
                {
                    await App.Current.MainPage.DisplayAlert("Búsqueda finalizada", "No se han encontrado resultados", "Ok");
                }
            }
            this.IsBusy = false;
        }

        private async void ItemTapped()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Song Song = LastTappedItem as Song;
                if (Song != null)
                {
                    if (Song.isFavorite)
                        DeleteFavorite(Song);
                    else
                        SaveFavorite(Song);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error añadiendo a playlist", "Ok");
            }
        }

        private async void SaveFavorite(Song Song)
        {
            Song.isFavorite = true;

            await App.DatabaseService.SaveItemAsync(Song);

            UserDialogs.Instance.Toast(new ToastConfig("Añadiendo canción a playlist")
                                            .SetDuration(3000)
                                            .SetBackgroundColor(System.Drawing.Color.FromArgb(12, 131, 193)));

            App.Current.MainPage.ToolbarItems.Clear();
            App.Current.MainPage.ToolbarItems.Add(new ToolbarItem("", "fav_filled.png", () => DeleteFavorite(Song)));

        }

      
        private async void DeleteFavorite(Song Song)
        {
            Song.isFavorite = false;

            
            await App.DatabaseService.DeleteItemAsync(Song);

            UserDialogs.Instance.Toast(new ToastConfig("Eliminando canción de la playlist")
                                            .SetDuration(3000)
                                            .SetBackgroundColor(System.Drawing.Color.FromArgb(12, 131, 193)));
                                            

            this.FavSongs = new ObservableCollection<Song>(await App.DatabaseService.GetItemsAsync());

            App.Current.MainPage.ToolbarItems.Clear();
            App.Current.MainPage.ToolbarItems.Add(new ToolbarItem("", "fav_empty.png", () => SaveFavorite(Song)));
            
        
        }

        private async void BtnSearchClicked()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                if (!string.IsNullOrEmpty(EntryTitleText) && !string.IsNullOrWhiteSpace(EntryTitleText))
                {
                    SearchSongsAsync(EntryTitleText);

                    EntryTitleText = "";

                    new NavigationService().NavigateTo(new ResultPage());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Debe introducir un titulo válido", "Ok");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No hay conexión a Internet", "Ok");
            }
        }
        #endregion
    }
}
