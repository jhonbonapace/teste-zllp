using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dominio
{
    public class VendaDominio
    {
        public string Id { get; private set; }
        public string NomeVendedor { get; private set; }
        public IList<ItemVendaDominio> Vendas { get; private set; }
        public readonly decimal ValorTotal;

        public VendaDominio(string[] inputDataFromFile)
        {
            string id = inputDataFromFile[1];
            string vendasStr = inputDataFromFile[2].Replace("[", "").Replace("]", "");
            string nomeVendedor = inputDataFromFile[3];
            
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Id não informado");
            
            if (string.IsNullOrEmpty(nomeVendedor))
                throw new ArgumentException($"(Id: {Id}) Vendedor não informado");

            Vendas = vendasStr
                        .Split(',')
                        .Select(x => new ItemVendaDominio(x.Split('-')))
                        .ToList();

            ValorTotal = Vendas.Sum(x => x.Preco * x.Quantidade);

            this.Id = id;
            this.NomeVendedor = nomeVendedor;
        }
    }
}