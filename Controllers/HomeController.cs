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

                    result.Add("login", dictionary.GetValueOrDefault("login")?.ToString() ?? "");
                    result.Add("id", dictionary.GetValueOrDefault("id")?.ToString() ?? "");
                    result.Add("avatar_url", dictionary.GetValueOrDefault("avatar_url")?.ToString() ?? "");
                    result.Add("url", dictionary.GetValueOrDefault("url")?.ToString() ?? "");
                    result.Add("html_url", dictionary.GetValueOrDefault("html_url")?.ToString() ?? "");
                    result.Add("followers_url", dictionary.GetValueOrDefault("followers_url")?.ToString() ?? "");
                    result.Add("following_url", dictionary.GetValueOrDefault("following_url")?.ToString() ?? "");
                    result.Add("repos_url", dictionary.GetValueOrDefault("repos_url")?.ToString() ?? "");
                    result.Add("events_url", dictionary.GetValueOrDefault("events_url")?.ToString() ?? "");
                    result.Add("name", dictionary.GetValueOrDefault("name")?.ToString() ?? "");
                    result.Add("company", dictionary.GetValueOrDefault("company")?.ToString() ?? "");
                    result.Add("location", dictionary.GetValueOrDefault("location")?.ToString() ?? "");
                    result.Add("bio", dictionary.GetValueOrDefault("bio")?.ToString() ?? "");
                    result.Add("email", dictionary.GetValueOrDefault("email")?.ToString() ?? "");
                    result.Add("public_repos", dictionary.GetValueOrDefault("public_repos")?.ToString() ?? "");
                    result.Add("followers", dictionary.GetValueOrDefault("followers")?.ToString() ?? "");
                    result.Add("following", dictionary.GetValueOrDefault("following")?.ToString() ?? "");
                    result.Add("created_at", dictionary.GetValueOrDefault("created_at")?.ToString() ?? "");
                    result.Add("updated_at", dictionary.GetValueOrDefault("updated_at")?.ToString() ?? "");

                    ViewData["UserData"] = result;

                    return View();
                }
            }

            ViewData["IndexErrorMessage"] = $"User \"{userName}\" Not Found";
            return View("Index");
        }
    }
}
