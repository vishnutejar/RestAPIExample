using Newtonsoft.Json;
using RestAPIExample.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

        private void AddPost(object sender, EventArgs e)
        {
            HttpClient client;
            JsonSerializerOptions serializerOptions;

            Uri uri = new Uri(string.Format("https://jsonplaceholder.typicode.com/posts", string.Empty));
            client = new HttpClient();
            Root postData = new Root() {
                body = "post1",
                title="7/26/2021",
                userId=12321
            };

            string json = JsonSerializer.Serialize<Root>(postData);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = client.PostAsync(uri, content).Result;


            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"\tTodoItem successfully saved.");
                string postdata =  response.Content.ReadAsStringAsync().Result;
                GetData();

            }

        }

        private void PutData(object sender, EventArgs e)
        {
            HttpClient client;
            JsonSerializerOptions serializerOptions;

            Uri uri = new Uri(string.Format("https://jsonplaceholder.typicode.com/posts/1", string.Empty));
            client = new HttpClient();
            Root postData = new Root()
            {
                body = "post1",
                title = "7/26/2021",
                userId = 1,
                id=1
            };


            string json = JsonSerializer.Serialize<Root>(postData);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = client.PutAsync(uri, content).Result;


            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"\tTodoItem successfully saved.");
                string postdata = response.Content.ReadAsStringAsync().Result;

            }

        }
        private void DeleteData(object sender, EventArgs e)
        {
            HttpClient client;
            JsonSerializerOptions serializerOptions;
            Root postData = new Root()
            {
                body = "post1",
                title = "7/26/2021",
                userId = 1,
                id = 1
            };
            Uri uri = new Uri(string.Format("https://jsonplaceholder.typicode.com/posts/{0}", postData.id));
            client = new HttpClient();
            


            string json = JsonSerializer.Serialize<Root>(postData);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = client.DeleteAsync(uri).Result;


            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"\tTodoItem successfully saved.");
                string postdata = response.Content.ReadAsStringAsync().Result;

            }
            else { 
            

            }

        }

    }
}
