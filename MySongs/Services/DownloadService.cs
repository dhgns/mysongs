using QuickType;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MySongs.Services
{
    static class DownloadService
    {
        /// <summary>
        /// Change apiKey with your API key obtained from food2fork.com
        /// </summary>
        public async static Task<Query> SearchSongsAsync(string title)
        {
            string url = "https://api.deezer.com/search?q=track:" + title;
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetStringAsync(url);
                var parsedJSON = Query.FromJson(response);
                return parsedJSON;
            }
            catch(Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error de conexión con el servidor","Ok");
                return null;
            }
            
        }
    }
}
