using Newtonsoft.Json;
using RestAPIExample.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RestAPIExample
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Root> Post { get; set; }
        public MainPage()
        {

            InitializeComponent();
            GetData();
        }
        public async Task GetData()
        {
            HttpClient client;

            Uri uri = new Uri(string.Format("https://jsonplaceholder.typicode.com/posts", string.Empty));
            client = new HttpClient();

            //string json = JsonSerializer.Serialize<TodoItem>(item, serializerOptions);
            // StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
             response = await client.GetAsync(uri);
            

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"\tTodoItem successfully saved.");
                string content = await response.Content.ReadAsStringAsync();
               var Items = JsonConvert.DeserializeObject<List<Root>>(content);
                Post = new ObservableCollection<Root>(Items);
                lstPost.ItemsSource = Post;
            }
        }
    }
}
