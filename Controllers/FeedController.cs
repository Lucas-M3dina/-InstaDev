using System;
using System.IO;
using InstaDev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaDev.Controllers
{
    [Route("Feed")]
    public class FeedController : Controller
    {
        Post PostModel = new Post();
        Usuario u = new Usuario();
        Random IdAleatorio = new Random();
        bool repetir;

        public IActionResult Index(){
            ViewBag.UserName = HttpContext.Session.GetString("_UserName");
            ViewBag.Name = HttpContext.Session.GetString("_Nome");
            ViewBag.ImagemUsuario = HttpContext.Session.GetString("_FotoUsuario");
            ViewBag.Posts = PostModel.LerTodas();
            ViewBag.usuarios = u.LerTodos();
            return View();
        }

        [Route("Postar")]
        public IActionResult Postar(IFormCollection form){
            Post novoPost = new Post();
            do
            {
                Int32 idimagem = IdAleatorio.Next();
                repetir = PostModel.VerificandoId(idimagem);

            } while(repetir == true);

            novoPost.NomeUsuario = HttpContext.Session.GetString("_UserName");
            novoPost.ImagemUsuario = HttpContext.Session.GetString("_FotoUsuario");
            novoPost.IdImagem = IdAleatorio.Next();
            novoPost.Descricao = form["Descricao"];
            //novaEquipe.Imagem = form["Imagem"];

            if (form.Files.Count > 0)
            {
                //Upload inicio

                var file = form.Files[0];
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Posts");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                novoPost.Imagem = file.FileName;


            }else
            {
                novoPost.Imagem = "padrao.png";
            }
            
            //Upload final

            PostModel.Criar(novoPost);

            ViewBag.Post = PostModel.LerTodas();

            return LocalRedirect("~/Feed");
        }
    }
}