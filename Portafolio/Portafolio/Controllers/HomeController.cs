using Microsoft.AspNetCore.Mvc;
using Portafolio.Models;
using Portafolio.Services;
using System.Diagnostics;

namespace Portafolio.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositorioProyectos repositorioProyectos;
        private readonly IServicioCorreo servicioCorreo;
        private readonly IConfiguration configuration;

        public HomeController (ILogger<HomeController> logger, IRepositorioProyectos repositorioProyectos, IServicioCorreo servicioCorreo, IConfiguration configuration) {
            _logger = logger;
            this.repositorioProyectos = repositorioProyectos;
            this.servicioCorreo = servicioCorreo;
            this.configuration = configuration;
        }

        /*Estos metodos se conocen como ACCIONES, estas acciones se 
        ejecutan cuando realizamos una peticion HTTP a una ruta especifica
        de nuestra aplicacion*/
        public IActionResult Index() {
            // Configuramos una propiedad para el viewbag
            ViewBag.Nombre = "Cesar Villalobos Olmos";
            ViewBag.Edad = 20;
            ViewBag.Mensaje = "Soy un programador autodidacta enfocado en tecnologias web, " +
                              "especialmente con JavaScript y .Net";

            var persona = new Persona() {
                Nombre = "Cesar Villalobos Olmos",
                Edad = 20,
                Mensaje = 
                    "Soy un programador autodidacta enfocado en tecnologias web, " + 
                    "especialmente con JavaScript y .Net"
            };

            // Le podemos pasar el nombre de la vista a mostrar - Primer parametro
            // El segundo parametro parametro corresponde a un modelo fuertemente tipado
            // return View("Index", persona);

            /*Mostramos un mensaje de log en consola, existen: 
            Critical, Error, Warning, Information, Debug y Trace*/
            _logger.LogCritical("Este es un mensaje de critical");
            _logger.LogError("Este es un mensaje de error");
            _logger.LogWarning("Este es un mensaje de warning");
            _logger.LogInformation("Este es un mensaje de information");
            _logger.LogDebug("Este es un mensaje de debug");
            _logger.LogTrace("Este es un mensaje de trace");

            /*Obtenemos una variable desde nuestro archivo de configuracion json*/
            var nombre = configuration.GetValue<string>("Nombre");
            _logger.LogInformation($"El nombre es: {nombre}");

            var proyectos = repositorioProyectos.ObtenerProyectos().Take(3).ToList();
            
            var modelo = new HomeIndexViewModel() { 
                Proyectos = proyectos
            };
            
            return View(modelo);
        }

        public IActionResult Proyectos() {
            var proyectos = repositorioProyectos.ObtenerProyectos().ToList();
            return View(proyectos);
        }

        public IActionResult Contacto() {
            return View();
        }

        [HttpPost]
        public IActionResult Contacto(ContactoViewModel contactoViewModel) {
            string mensaje = servicioCorreo.EnviarMensaje(contactoViewModel);
            _logger.LogInformation(mensaje);

            return RedirectToAction("Gracias");
        }

        public IActionResult Gracias() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}