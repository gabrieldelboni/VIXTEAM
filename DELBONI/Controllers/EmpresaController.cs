using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DELBONI.Models;

namespace DELBONI.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly ILogger<EmpresaController> _logger;

        public EmpresaController(ILogger<EmpresaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Codigo,Nome,NomeFantasia,CNPJ")] Empresa Empresa)
        {
            return View("Index");
        }
    }
}