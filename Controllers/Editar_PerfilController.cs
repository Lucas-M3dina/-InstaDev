using InstaDev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaDev.Controllers
{
        [Route("EditPerfil")]
    public class Editar_PerfilController : Controller
    {
        Usuario Usuario = new Usuario();
        Usuario UsuarioModel = new Usuario();
        // Random IdAleatorio = new Random();
        bool repetir;


        [Route("Index")]
        public IActionResult Index(){
            
            // ViewBag.UsuarioModel = Usuario.LerTodas();
            ViewBag.Usuario = UsuarioModel.LerTodos();
            return View();
        }


       [Route("Modificar")]
    
        public IActionResult Modificar(IFormCollection form){
            
            Usuario NovoUsuario = new Usuario();

            NovoUsuario.Nome = form[""];
            NovoUsuario.NomeUsuario = form[""];
            NovoUsuario.Email = form[""];

            UsuarioModel.Criar(NovoUsuario);
            ViewBag.Usuario = UsuarioModel.LerTodos();

            return LocalRedirect("~/Feed");
        }
        // [Route("Deletar/{id}")]
        // public IActionResult Deletar(int id)
        // {
        //     UsuarioModel.Deletar(id);
        //     ViewBag.Usuario = UsuarioModel.LerTodos();
        //     return LocalRedirect("~/Login");
        // }

        
    }
    

}