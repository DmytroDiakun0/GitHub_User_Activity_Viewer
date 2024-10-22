//using GitHub_User_Activity_Viewer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GitHub_User_Activity_Viewer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DisplayUser(string userName)
        {
            var client = _httpClientFactory.CreateClient();

            var url = $"https://api.github.com/users/{userName}";

            client.DefaultRequestHeaders.Add("User-Agent", "GitHub_User_Activity_Viewer");

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(data);

                if (dictionary != null)
                {
                    Dictionary<string, string> result = new Dictionary<string, string>();

                    //DateTime created;
                    //DateTime updated;
                    DateTime.TryParse(dictionary.GetValueOrDefault("created_at").ToString(), out DateTime created);
                    DateTime.TryParse(dictionary.GetValueOrDefault("updated_at").ToString(), out DateTime updated);

                    result.Add("login", dictionary.GetValueOrDefault("login")?.ToString() ?? "[null]");
                    result.Add("id", dictionary.GetValueOrDefault("id")?.ToString() ?? "[null]");
                    result.Add("avatar_url", dictionary.GetValueOrDefault("avatar_url")?.ToString() ?? "[null]");
                    result.Add("url", dictionary.GetValueOrDefault("url")?.ToString() ?? "[null]");
                    result.Add("html_url", dictionary.GetValueOrDefault("html_url")?.ToString() ?? "[null]");
                    result.Add("followers_url", dictionary.GetValueOrDefault("followers_url")?.ToString() ?? "[null]");
                    result.Add("following_url", dictionary.GetValueOrDefault("following_url")?.ToString() ?? "[null]");
                    result.Add("repos_url", dictionary.GetValueOrDefault("repos_url")?.ToString() ?? "[null]");
                    result.Add("events_url", dictionary.GetValueOrDefault("events_url")?.ToString() ?? "[null]");
                    result.Add("name", dictionary.GetValueOrDefault("name")?.ToString() ?? "[null]");
                    result.Add("company", dictionary.GetValueOrDefault("company")?.ToString() ?? "[null]");
                    result.Add("location", dictionary.GetValueOrDefault("location")?.ToString() ?? "[null]");
                    result.Add("bio", dictionary.GetValueOrDefault("bio")?.ToString() ?? "[No Bio]");
                    result.Add("email", dictionary.GetValueOrDefault("email")?.ToString() ?? "[null]");
                    result.Add("public_repos", dictionary.GetValueOrDefault("public_repos")?.ToString() ?? "[null]");
                    result.Add("followers", dictionary.GetValueOrDefault("followers")?.ToString() ?? "[null]");
                    result.Add("following", dictionary.GetValueOrDefault("following")?.ToString() ?? "[null]");
                    result.Add("created_at", created.ToString("yyyy-MM-dd") ?? "[null]");
                    result.Add("updated_at", updated.ToString("yyyy-MM-dd") ?? "[null]");

                    ViewData["UserData"] = result;

                    return View();
                }
            }

            ViewData["IndexErrorMessage"] = $"User \"{userName}\" Not Found";
            return View("Index");
        }
    }
}
