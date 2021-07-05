using InstaDev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaDev.Controllers
{
        [Route("EditPerfil")]
    public class EditLoginController : Controller
    {
        Usuario Usuario = new Usuario();
        Usuario UsuarioModel = new Usuario();
        // Random IdAleatorio = new Random();
        bool repetir;


        [Route("Listar")]
        public IActionResult Index(){
            
            // ViewBag.UsuarioModel = Usuario.LerTodas();
            ViewBag.UsuarioModel = Usuario.LerTodos();
            return View();
        }


       [Route("Modificar")]
    
        public IActionResult Modificar(IFormCollection form){
            
            Usuario NovoUsuario = new Usuario();

            NovoUsuario.Nome = form["Nome"];
            NovoUsuario.NomeUsuario = form["NomeUsuario"];
            NovoUsuario.Email = form["Email"];

            UsuarioModel.Criar(NovoUsuario);
            ViewBag.Usuario = UsuarioModel.LerTodos();

            return LocalRedirect("~/EditPerfil/Modificar");
        }

        // public IActionResult Modificar(Usuario user)
        // {
        //     List<string> linhas = LerTodasLinhasCSV(PATH);
        //     linhas.RemoveAll(x => x.Split(";")[0] == user.IdUsuario.ToString());
        //     linhas.Add(PrepararLinha(user));
        //     ReescreverCSV(PATH, linhas);
        // }
    }
    

}