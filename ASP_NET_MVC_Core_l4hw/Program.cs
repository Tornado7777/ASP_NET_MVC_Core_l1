using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ASP_NET_MVC_Core_l4hw
{
    /*
     * Придумайте небольшое приложение консольного типа, который берет различные Json
     * структуры (предположительно из разных веб сервисов), олицетворяюющие товар в магазинах.
     * Структуры не похожи друг на друга, но вам нужно их учесть, сделать универсально. Структуры
     * на ваше усмотрение.
     */
    internal class Program
    {
        static readonly HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com"),
        };

        static void Main(string[] args)
        {
            var saveNodeAdapter = new SaveNodeAdapter();
            GoClient("todos/3", saveNodeAdapter).Wait();
            GoClient("posts/3", saveNodeAdapter).Wait();
        }

        static async Task GoClient(string address, SaveNodeAdapter saveNodeAdapter )
        {           

            using HttpResponseMessage response1 = await client.GetAsync(address);

            if (response1.IsSuccessStatusCode)
            {
                var jsonResponse = await response1.Content.ReadAsStringAsync();
                saveNodeAdapter.SaveNode(jsonResponse);
            }
        }
    }



    #region Начальные классы
    class Todo
    {
        int UserId;
        int Id;
        string Title;
        bool Completed;
        public Todo(int userId = 0,
        int id = 0,
        string title = null,
        bool completed = false)
        {
            UserId = userId;
            Id = id;
            Title = title;
            Completed = completed;
        }

    }
    class Post
    {
        int UserId;
        int Id;
        string Title;
        string Body;
        public Post(int userId = 0,
        int id = 0,
        string title = null,
        string body = null)
        {
            UserId = userId;
            Id = id;
            Title = title;
            Body = body;
        }
    }

   
    #endregion

    #region Целевой интерфейс

    public interface ISaveNodeAdapter
    {
        void SaveNode(string jsonString);
    }

    #endregion

    #region Адаптер для классов
    public class SaveNodeAdapter : ISaveNodeAdapter
    {
        public void SaveNode(string jsonString)
        {
            Console.WriteLine($"{jsonString}\n");
            try
            {
                Todo todo = JsonConvert.DeserializeObject<Todo>(jsonString);
                //TODO:....
            }
            catch
            {
                Post post = JsonConvert.DeserializeObject<Post>(jsonString);
                //TODO:....
            }
            Console.ReadKey(true);
        }
    }
    #endregion
}
