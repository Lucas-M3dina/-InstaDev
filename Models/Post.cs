using System;
using System.Collections.Generic;
using System.IO;

namespace InstaDev.Models
{
    public class Post : InstadevBase
    {
         public Int32 IdImagem { get; set; }

        public string Descricao { get; set; }
        public string NomeUsuario { get; set; }
        public string ImagemUsuario { get; set; }
        
        
        
        
        public string Imagem { get; set; }
        public bool repetir { get; set; }
        


        private const string PATH = "Database/Post.csv";


        public Post(){
            CriarPastaEArquivo(PATH);
        }

        private string Preparar(Post e){
            return $"{e.NomeUsuario};{e.IdImagem};{e.Descricao};{e.Imagem};{e.ImagemUsuario}";
        }

        public void Criar(Post e)
        {
            string[] linha = {Preparar(e)};
            File.AppendAllLines(PATH, linha);
        }

        public void Deletar(string user)
        {
            List<string> linhas = LerTodasLinhasCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == user);
            ReescreverCSV(PATH, linhas);
        }

        public List<Post> LerTodas()
        {
            List<Post> postagens = new List<Post>();
            string[] linhas = File.ReadAllLines(PATH);

            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");
                Post postagem = new Post();
                
                postagem.NomeUsuario = linha[0];
                postagem.IdImagem = Int32.Parse(linha[1]);
                postagem.Descricao = linha[2];
                postagem.Imagem = linha[3];
                postagem.ImagemUsuario = linha[4];
                

                postagens.Add(postagem);
            }

            return postagens;
        }

        public bool VerificandoId(Int32 id){
            List<string> PostsCsv = LerTodasLinhasCSV("Database/Post.csv");
            var identificador = PostsCsv.Find(x => Int32.Parse(x.Split(";")[1]) == id);

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