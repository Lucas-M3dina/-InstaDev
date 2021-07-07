using System.Collections.Generic;
using InstaDev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaDev.Controllers
{
    [Route("Home")]
    public class LoginController: Controller
    {
        [TempData]
        public string Mensagem { get; set; }
        Usuario UsuarioModel = new Usuario();

        public IActionResult Index(){

            return View();
        }

        [Route("Logar")]
        public IActionResult Logar(IFormCollection form){
            List<string> UsuariosCsv = UsuarioModel.LerTodasLinhasCSV("Database/Usuario.csv");
            var logado = UsuariosCsv.Find(x => x.Split(";")[1] == form["Email"] && x.Split(";")[2] == form["Senha"] );
            
            if (logado != null)
            {
                HttpContext.Session.SetString("_UserName", logado.Split(";")[4]);
                HttpContext.Session.SetString("_IdUser", logado.Split(";")[0]);
                return LocalRedirect("~/Feed");
            }
            
            Mensagem = "Dados incorretos, tente novamente...";
            return LocalRedirect("~/");
        }

        [Route("Logout")]
        public IActionResult Logout(){
            HttpContext.Session.Remove("_UserName");
            HttpContext.Session.Remove("_IdUser");
            return LocalRedirect("~/");
        }
    }
}