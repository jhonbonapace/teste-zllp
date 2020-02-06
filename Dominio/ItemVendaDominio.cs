using System;
using System.Text.RegularExpressions;

namespace Dominio
{
    public class ItemVendaDominio
    {
        public string Id { get; private set; }
        public int Quantidade { get; private set; }
        public decimal Preco { get; private set; }

        public ItemVendaDominio(string[] dadosEntrada)
        {
            if (dadosEntrada.Length != 3)
                throw new ArgumentException($"(Id: {Id}) Dados de entrada com informações faltantes.");

            string id = dadosEntrada[0];
            int quantidade = Convert.ToInt32(dadosEntrada[1]);
            decimal preco = Convert.ToDecimal(dadosEntrada[2]);

            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException($"Id não informado");

            if (quantidade <= 0)
                throw new ArgumentException($"(Id: {id}) Quantidade do item não informada");

            if (preco <= 0)
                throw new ArgumentException($"(Id: {id}) Preço do item não informado");

            Id = id;
            Quantidade = quantidade;
            Preco = preco;
        }
    }
}