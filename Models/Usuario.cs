using System;
using System.Collections.Generic;
using System.IO;

namespace InstaDev.Models
{
    public class Usuario: InstadevBase
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string FotoPerfil { get; set; } = "user_padrao.jpg";
        public bool repetir;
        
        

        private const string PATH = "Database/Usuario.csv";
        
        public Usuario(){
            CriarPastaEArquivo(PATH);
        }

        private string PrepararLinha(Usuario user){
            return $"{user.IdUsuario};{user.Email};{user.Senha};{user.Nome};{user.NomeUsuario};{user.FotoPerfil}";
        }

        public void Criar(Usuario user)
        {
            string[] linha = {PrepararLinha(user)};
            File.AppendAllLines(PATH, linha);

        }

        public List<Usuario> LerTodos()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string[] linhas = File.ReadAllLines(PATH);

            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");
                Usuario usuario = new Usuario();

                usuario.IdUsuario = Int32.Parse(linha[0]);
                usuario.Email = linha[1];
                usuario.Senha = linha[2];
                usuario.Nome = linha[3];
                usuario.NomeUsuario = linha[4];
                usuario.FotoPerfil = linha[5];

                usuarios.Add(usuario);
            }

            return usuarios;
        }

        public void Alterar(Usuario user)
        {
            List<string> linhas = LerTodasLinhasCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == user.IdUsuario.ToString());
            linhas.Add(PrepararLinha(user));
            ReescreverCSV(PATH, linhas);
        }

        public void Deletar(int id)
        {
            List<string> linhas = LerTodasLinhasCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());
            ReescreverCSV(PATH, linhas);
        }

        public bool VerificandoId(Int32 id){
            List<string> UsuariosCsv = LerTodasLinhasCSV("Database/Usuario.csv");
            var identificador = UsuariosCsv.Find(x => Int32.Parse(x.Split(";")[0]) == id);

            if (identificador != null)
            {
                repetir = true;
            }
            else if (identificador == null)
            {
                repetir = false;
            }

            return repetir;
        }
    }
}