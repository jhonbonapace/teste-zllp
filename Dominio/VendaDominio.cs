using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dominio
{
    public class VendaDominio : DadosDominio
    {
        public string IdVenda { get; private set; }
        public string NomeVendedor { get; private set; }
        public IList<ItemVendaDominio> Vendas { get; private set; }
        public readonly decimal ValorTotal;

        public VendaDominio(string[] dadosEntrada)
        {
            if (dadosEntrada.Length != 4)
                throw new ArgumentException($"(Id: {Id}) Dados de entrada com informações faltantes.");

            string id = dadosEntrada[0];
            string idVenda = dadosEntrada[1];
            string vendasStr = dadosEntrada[2].Replace("[", "").Replace("]", "");
            string nomeVendedor = dadosEntrada[3];

            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Id não informado");

            if (string.IsNullOrWhiteSpace(idVenda))
                throw new ArgumentException($"(Id: {Id}) Id da Venda não informado");

            if (string.IsNullOrWhiteSpace(nomeVendedor))
                throw new ArgumentException($"(Id: {Id}) Vendedor não informado");

            Vendas = vendasStr
                        .Split(',')
                        .Select(x => new ItemVendaDominio(x.Split('-')))
                        .ToList();

            ValorTotal = Vendas.Sum(x => x.Preco * x.Quantidade);

            this.Id = id;
            this.IdVenda = idVenda;
            this.NomeVendedor = nomeVendedor;
        }
    }
}