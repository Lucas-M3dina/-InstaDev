using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InstaDev.Models;

namespace InstaDev_feed.Controllers
{
    [Route("Perfil")]
    public class PerfilController: Controller
    {
        Post PostModel = new Post();
        Usuario u = new Usuario();
        

        public IActionResult Index(){
            ViewBag.UserName = HttpContext.Session.GetString("_UserName");
            ViewBag.Name = HttpContext.Session.GetString("_Nome");
            ViewBag.ImagemUsuario = HttpContext.Session.GetString("_FotoUsuario");
            ViewBag.Posts = PostModel.LerTodas();
            ViewBag.usuarios = u.LerTodos();
            ViewBag.cont = ContarPost();
            return View();
        }

        [Route("Contar")]
        public int ContarPost(){
            string nick = HttpContext.Session.GetString("_UserName");
            int cont = 0;
            foreach (var item in PostModel.LerTodas())
            {
                if (item.NomeUsuario == nick)
                {
                    cont++;
                }
            }
            return cont;
            
            
        
        }
    }
}