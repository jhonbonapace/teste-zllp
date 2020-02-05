using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dominio;
using Infraestrutura.Helper;
using Infraestrutura.Repositorio;
using Shared;

namespace Infraestrutura
{
    public class ArquivoRepositorio
    {
        private string _nomeArquivo;
        private string _caminhoArquivo;

        public ArquivoRepositorio(string nomeArquivo, string caminhoArquivo)
        {
            _nomeArquivo = nomeArquivo;
            _caminhoArquivo = caminhoArquivo;
        }

        public RelatorioDominio ProcessaArquivo()
        {
            string caminhoCompletoReaded = Path.Combine(Pasta.FolderDataReaded, _nomeArquivo);

            string[] fileLines = File.ReadAllLines(_caminhoArquivo);

            File.Delete(caminhoCompletoReaded);
            File.Move(_caminhoArquivo, caminhoCompletoReaded);

            return new RelatorioDominio(
                VendedorRepositorio.GetVendedores(fileLines),
                ClienteRepositorio.GetClientes(fileLines),
                VendaRepositorio.GetVendas(fileLines)
            );
        }

        public IList<RelatorioDominio> ProcessaArquivosExistentes()
        {
            IList<RelatorioDominio> relatorios = new List<RelatorioDominio>();

            foreach (string arquivo in Directory.GetFiles(Pasta.FolderDataIn))
            {
                _nomeArquivo = Path.GetFileName(arquivo);
                _caminhoArquivo = arquivo;
                relatorios.Add(ProcessaArquivo());
            }

            return relatorios;
        }
    }
}
