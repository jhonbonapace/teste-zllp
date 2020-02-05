using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Shared;

namespace Dominio
{
    public class RelatorioDominio
    {
        public IList<VendedorDominio> Vendedores { get; set; }
        public IList<ClienteDominio> Clientes { get; set; }
        public IList<VendaDominio> Vendas { get; set; }

        public RelatorioDominio(
            IList<VendedorDominio> vendedores,
            IList<ClienteDominio> clientes,
            IList<VendaDominio> vendas)
        {

        }

        private int TotalVendedores() => Vendedores != null ? Vendedores.Count : 0;

        private int TotalClientes() => Clientes != null ? Clientes.Count : 0;

        private string VendaMaisCara() =>
            Vendas != null && Vendas.Count > 0 ?
            Vendas.OrderByDescending(x => x.ValorTotal).First().Id
            : "Nenhuma venda para retornar";

        private string PiorVendedor() =>
            Vendas != null && Vendas.Count > 0 ?
            Vendas
                .GroupBy(x => x.NomeVendedor)
                .Select(x => new { NomeVendedor = x.Key, TotalEmVendas = x.Sum(y => y.ValorTotal) })
                .OrderBy(x => x.TotalEmVendas)
                .First()
                .NomeVendedor
            : "Nenhuma venda para avaliar o vendedor";

        public string Exportar(string nomeArquivo)
        {
            string pathDestino = Path.Combine(Pasta.FolderDataOut, nomeArquivo);
            File.WriteAllText(pathDestino, Gerar());
            return pathDestino;
        }

        private string Gerar()
        {
            StringBuilder relatorio = new StringBuilder();
            relatorio.AppendLine($"Quantidade de clientes no arquivo de entrada: {TotalClientes()}");
            relatorio.AppendLine($"Quantidade de vendedor no arquivo de entrada: {TotalVendedores()}");
            relatorio.AppendLine($"ID da venda mais cara: {VendaMaisCara()}");
            relatorio.AppendLine($"O pior vendedor: {PiorVendedor()}");
            return relatorio.ToString();
        }
    }
}