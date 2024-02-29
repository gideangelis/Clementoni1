using Clementoni1.Interfaces;
using Clementoni1.Models;
using Clementoni1.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Clementoni1.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;
        private readonly IPersonaService<PersonaItaliaService> _personaItaliaService;
        private readonly IPersonaService<PersonaFranciaService> _personaFranciaService;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, IPersonaService<PersonaItaliaService> personaItaliaService, IPersonaService<PersonaFranciaService> personaFranciaService)
        {
            _logger = logger;
            _httpClient = httpClient;
            _personaItaliaService = personaItaliaService;
            _personaFranciaService = personaFranciaService;
        }

        public async Task<IActionResult> Index()
        {
            //_logger.LogInformation("Hai aperto la home");

            //var url = "https://www.google.it";

            //var response = await _httpClient.GetAsync($"{url}");
            //_ = response.EnsureSuccessStatusCode();

            //var aggiungiNumeroItalia = _personaItaliaService.AggiungiPrefisso("3408473829");

            //_logger.LogInformation(aggiungiNumeroItalia);

            //var aggiungiNumeroFrancia = _personaFranciaService.AggiungiPrefisso("123");

            //_logger.LogInformation(aggiungiNumeroFrancia);

            
            //var listaPersone = new List<PersonaViewModel>(); { persona, persona2 }
            

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> _ListPersona()
        {
            var url = "https://localhost:7192/api/Person";

            var response = await _httpClient.GetAsync($"{url}");
            _ = response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            var persone = JsonConvert.DeserializeObject<List<PersonaViewModel>>(responseString);

            //ViewBag.Count = lista.Count;

            return PartialView(persone);
        }

        [HttpGet]
        public IActionResult _FormPersona()
        { 
            return PartialView();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersona(PersonaViewModel persona)
        {
            var url = "https://localhost:7192/api/Person";
            var personaJson = JsonConvert.SerializeObject(persona);

            var data = new StringContent(personaJson.ToString(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, data);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeletePersona(int id)
        {
            var url = $"https://localhost:7192/api/Person/{id}";
            var response = await _httpClient.DeleteAsync(url);

            return RedirectToAction("Index");
        }

      
        public async Task<IActionResult> _EditPersona(int id)
        {
            var url = $"https://localhost:7192/api/Person/{id}";
            var persona = new PersonaViewModel();
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                persona = JsonConvert.DeserializeObject<PersonaViewModel>(content);
            }

            return PartialView(persona);
        }

        [HttpPost]
        public async Task<IActionResult> EditPersona(PersonaViewModel modificata)
        {
            var url = $"https://localhost:7192/api/Person/{modificata.Id}";
            var personaJson = JsonConvert.SerializeObject(modificata);

            var data = new StringContent(personaJson.ToString(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, data);

            return RedirectToAction("Index");
        }

    }
}
