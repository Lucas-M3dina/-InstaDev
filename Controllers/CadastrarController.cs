using System;
using InstaDev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaDev.Controllers
{
    [Route("Cadastrar")]
    public class CadastrarController : Controller
    {
        Usuario UsuarioModel = new Usuario();
        Random numeroRandom = new Random();

        int IdAleatorio;
        bool repetir;

        public IActionResult Index(){
            
            return View();
        }

        [Route("Cadastrar_Usuario")]
        public IActionResult Cadastrar(IFormCollection form){
            Usuario novoUsuario = new Usuario();

            IdAleatorio = numeroRandom.Next();
            do
            {
                repetir = UsuarioModel.VerificandoId(IdAleatorio);

            } while(repetir == true);

            novoUsuario.IdUsuario = IdAleatorio;
            novoUsuario.Nome = form["Nome"];
            novoUsuario.NomeUsuario = form["NomeUsuario"];
            novoUsuario.Email = form["Email"];
            novoUsuario.Senha = form["Senha"];

            UsuarioModel.Criar(novoUsuario);

            return LocalRedirect("~/");
        }
    }
}