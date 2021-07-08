
using System.Collections.Generic;
using System.IO;
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
        Post DeletePost = new Post();
        // Random IdAleatorio = new Random();
        bool repetir;


        [Route("Listar")]
        public IActionResult Index(){
            ViewBag.UserName = HttpContext.Session.GetString("_UserName");
            ViewBag.Name = HttpContext.Session.GetString("_Nome");
            ViewBag.ImagemUsuario = HttpContext.Session.GetString("_FotoUsuario");
            // ViewBag.UsuarioModel = Usuario.LerTodos();
            ViewBag.Usuario = UsuarioModel.LerTodos();
            return View();
        }


       [Route("Modificar")]
        public IActionResult Modificar(IFormCollection form){
            int idUser = int.Parse(HttpContext.Session.GetString("_IdUser"));
            Usuario NovoUsuario = new Usuario();
            Usuario AntigoUsuario = new Usuario();
            string nick = HttpContext.Session.GetString("_UserName");

            List<Usuario> usuarios = new List<Usuario>();
            usuarios = Usuario.LerTodos();
            AntigoUsuario = usuarios.Find(x => x.NomeUsuario == nick);


            if (form.Files.Count > 0)
            {
                //Upload inicio

                var file = form.Files[0];
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                NovoUsuario.FotoPerfil = file.FileName;


            }else
            {
                NovoUsuario.FotoPerfil = "padrao.png";
            }
            
            
        
            NovoUsuario.Senha = AntigoUsuario.Senha;
            NovoUsuario.IdUsuario = AntigoUsuario.IdUsuario;
            NovoUsuario.Nome = form["Nome"];
            NovoUsuario.NomeUsuario = form["Username"];
            NovoUsuario.Email = form["Email"];

            HttpContext.Session.SetString("_UserName", form["Username"]);
            HttpContext.Session.SetString("_Nome", form["Nome"]);
            HttpContext.Session.SetString("_FotoUsuario", NovoUsuario.FotoPerfil);

            UsuarioModel.Deletar(idUser);

            UsuarioModel.Criar(NovoUsuario);
            ViewBag.Usuario = UsuarioModel.LerTodos();

            return LocalRedirect("~/Feed");
        }
        
        [Route("Excluir")]
        public IActionResult Deletar()
        {
            int IdDel = int.Parse(HttpContext.Session.GetString("_IdUser"));
            string user = HttpContext.Session.GetString("_UserName");
            UsuarioModel.Deletar(IdDel);
            DeletePost.Deletar(user);
            ViewBag.Usuario = UsuarioModel.LerTodos();
            return LocalRedirect("~/");
        }

        
    }
    

}