using System.Diagnostics;
using System.Text;
using System.Text.Json;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class RestService : IRestService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        public List<TodoItem> Items { get; private set; }

        public RestService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<TodoItem>> RefreshDataAsync()
        {
            Items = new List<TodoItem>();

            UriBuilder builder = new(Constants.BaseUrl) { Path = Constants.Endpoint };
            try
            {
                HttpResponseMessage response = await _client.GetAsync(builder.Uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonSerializer.Deserialize<List<TodoItem>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task SaveTodoItemAsync(TodoItem item, bool isNewItem = false)
        {
            try
            {
                string json = JsonSerializer.Serialize<TodoItem>(item, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    UriBuilder builder = new(Constants.BaseUrl) { Path = Constants.Endpoint };
                    response = await _client.PostAsync(builder.Uri, content);
                }
                else
                {
                    UriBuilder builder = new(Constants.BaseUrl) { Path = $"{Constants.Endpoint}/{item.Id}" };
                    response = await _client.PutAsync(builder.Uri, content);
                }

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"\tTodoItem successfully saved.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task DeleteTodoItemAsync(int id)
        {
            UriBuilder builder = new(Constants.BaseUrl) { Path = $"{Constants.Endpoint}/{id}" };

            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(builder.Uri);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"\tTodoItem successfully deleted.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
