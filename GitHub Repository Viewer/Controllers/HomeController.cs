using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitHub_Repository_Viewer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;

    public HomeController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://api.github.com/");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "GitHubRepoViewer"); // Required by GitHub API
    }

    // GET: Display the input form
    public IActionResult Index()
    {
        return View();
    }

    // POST: Fetch and display repositories
    [HttpPost]
    public async Task<IActionResult> Index(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            ViewBag.Error = "Please enter a GitHub username.";
            return View();
        }

        try
        {
            var response = await _httpClient.GetAsync($"users/{username}/repos");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var repositories = JsonConvert.DeserializeObject<List<Repository>>(json);
                return View(repositories);
            }
            else
            {
                ViewBag.Error = "User not found or API error occurred.";
                return View();
            }
        }
        catch (Exception)
        {
            ViewBag.Error = "An error occurred while fetching data.";
            return View();
        }
    }
}