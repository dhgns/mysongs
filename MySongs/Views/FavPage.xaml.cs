using MySongs.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySongs.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavPage : ContentPage
	{
		public FavPage ()
		{
			InitializeComponent();
		}

        protected override async void OnAppearing()
        {
            this.BindingContext = App.viewModel;
            App.Current.MainPage.ToolbarItems.Clear();
            App.viewModel.FavSongs = new ObservableCollection<Song>(await App.DatabaseService.GetItemsAsync());
            base.OnAppearing();
        }

        private  IEnumerable<Song> FromDAOAsync(List<SongDAO> list)
        {
            var Songs = new ObservableCollection<Song>();

            foreach (SongDAO songDAO in list)
            {
                Song Song = songDAO.fromDAO();
                //var Album = App.DatabaseService.GetAlbumNotAsync(songDAO.Artist);
                var Artist = App.DatabaseService.GetArtistNotAsync(songDAO.Album);
                Song.Artist = Artist.Result[0];
                //Song.Album = Album.Result[0];

                Songs.Add(songDAO.fromDAO());
            }

            return Songs;
        }
    }
}