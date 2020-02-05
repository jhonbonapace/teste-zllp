using System;
using System.Text.RegularExpressions;

namespace Dominio
{
    public class VendedorDominio : DadosDominio
    {
        public string CPF { get; private set; }
        public string Nome { get; private set; }
        public decimal Salario { get; private set; }

        public VendedorDominio(string[] inputDataFromFile)
        {
            string cpf = inputDataFromFile[1];
            string nome = inputDataFromFile[2]; 
            decimal salario = Convert.ToDecimal(inputDataFromFile[3]);

            Regex regex = new Regex(@"[^\d]");
            cpf = regex.Replace(cpf, "");

            if (string.IsNullOrEmpty(cpf))
                throw new ArgumentException($"(Id: {Id}) CPF não informado");
            
            if (cpf.Length != 11)
                throw new ArgumentException($"(Id: {Id}) Tamanho do CPF diferente de 11");

            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException($"(Id: {Id}) Nome não informado");

            if (salario <= 0)
                throw new ArgumentException($"(Id: {Id}) Salário não informado");

            this.CPF = cpf;
            this.Nome = nome;
            this.Salario = salario;
        }
    }
}