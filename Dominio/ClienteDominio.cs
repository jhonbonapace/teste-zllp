using System;
using System.Text.RegularExpressions;

namespace Dominio
{
    public class ClienteDominio : DadosDominio
    {
        public string CNPJ { get; private set; }
        public string Nome { get; private set; }
        public string AreaNegocio { get; private set; }

        public ClienteDominio(string[] dadosEntrada)
        {
            if (dadosEntrada.Length != 4)
                throw new ArgumentException($"(Id: {Id}) Dados de entrada com informações faltantes.");

            string id = dadosEntrada[0];
            string cnpj = dadosEntrada[1];
            string nome = dadosEntrada[2];
            string areaNegocio = dadosEntrada[3];

            Regex regex = new Regex(@"[^\d]");
            cnpj = regex.Replace(cnpj, "");

            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Id não informado");

            if (string.IsNullOrWhiteSpace(cnpj))
                throw new ArgumentException($"(Id: {Id}) CNPJ não informado");

            if (cnpj.Length != 14)
                throw new ArgumentException($"(Id: {Id}) Tamanho do CNPJ diferente de 14");

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException($"(Id: {Id}) Nome não informado");

            if (string.IsNullOrWhiteSpace(areaNegocio))
                throw new ArgumentException($"(Id: {Id}) Área de Negócio não informada");

            this.CNPJ = cnpj;
            this.Nome = nome;
            this.AreaNegocio = areaNegocio;
        }
    }
}