using System;
using System.Text.RegularExpressions;

namespace Dominio
{
    public class ClienteDominio : DadosDominio
    {
        public string CNPJ { get; private set; }
        public string Nome { get; private set; }
        public string AreaNegocio { get; private set; }

        public ClienteDominio(string[] inputDataFromFile)
        {
            string cnpj = inputDataFromFile[1]; 
            string nome = inputDataFromFile[2];
            string areaNegocio = inputDataFromFile[3];

            Regex regex = new Regex(@"[^\d]");
            cnpj = regex.Replace(cnpj, "");

            if (string.IsNullOrWhiteSpace(cnpj))
                throw new ArgumentException($"(Id: {Id}) CNPJ não informado");
            
            if (cnpj.Length != 14)
                throw new ArgumentException($"(Id: {Id}) Tamanho do CNPJ diferente de 14");

            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException($"(Id: {Id}) Nome não informado");

            if (string.IsNullOrWhiteSpace(areaNegocio))
                throw new ArgumentException($"(Id: {Id}) Área de Negócio não informada");

            this.CNPJ = cnpj;
            this.Nome = nome;
            this.AreaNegocio = areaNegocio;
        }
    }
}