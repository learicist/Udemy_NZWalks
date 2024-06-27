using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory clientFactory;

        public RegionsController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<RegionDTO> response = new List<RegionDTO>();

            try
            {
                // Get all regions from Web API
                var client = clientFactory.CreateClient();

                var httpResponse = await client.GetAsync("https://localhost:7267/api/regions");

                httpResponse.EnsureSuccessStatusCode();

                response.AddRange(await httpResponse.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());

                ViewBag.Response = response;
            }
            catch (Exception e)
            {
                // Log the exception
                // throw;
            }

            return View(response);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = clientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7267/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var responseMessage = await client.SendAsync(httpRequestMessage);

            responseMessage.EnsureSuccessStatusCode();

            var response = await responseMessage.Content.ReadFromJsonAsync<RegionDTO>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = clientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<RegionDTO>($"https://localhost:7267/api/regions/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }
                        
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDTO request)
        {
            var client = clientFactory.CreateClient();

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7267/api/regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var responseMessage = await client.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode();

            var response = await responseMessage.Content.ReadFromJsonAsync<RegionDTO>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDTO request)
        {
            try
            {
                var client = clientFactory.CreateClient();

                var response = await client.DeleteAsync($"https://localhost:7267/api/regions/{request.Id}");

                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception e)
            {
                // Log the error
                // throw;
            }

            return View("Edit");
        }
    }
}
