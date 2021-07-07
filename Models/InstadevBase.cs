using System.Collections.Generic;
using System.IO;

namespace InstaDev.Models
{
    public abstract class InstadevBase
    {
        public void CriarPastaEArquivo(string _PATH){
            string pasta = _PATH.Split("/")[0];
            string arquivo = _PATH.Split("/")[1];

            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }

            if (!File.Exists(_PATH))
            {
                File.Create(_PATH).Close();
            }
        }

        public List<string> LerTodasLinhasCSV(string _PATH){
            
            List<string> linhas = new List<string>();

            using (StreamReader file = new StreamReader(_PATH))
            {
                string linha;
                while ( (linha = file.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }

            return linhas;
        }

        public void ReescreverCSV(string _PATH, List<string> linhas){

            using (StreamWriter output = new StreamWriter(_PATH))
            {
                foreach (var item in linhas)
                {
                    output.Write(item + "\n");
                }
            }
        }
    }
}