using System;
using System.Collections.Generic;
using System.IO;
using Dominio;
using Infraestrutura;
using Shared;

namespace App
{
    class Program
    {
        private static readonly string DATA_IN = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "data", "in");
        private static readonly string DATA_OUT = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "data", "out");
        private static readonly string DATA_READED = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "data", "readed");

        static void Main(string[] args)
        {
            Pasta.GerenciarDiretorios();
            ProcessaArquivosExistentes();

            using (FileSystemWatcher watcher = new FileSystemWatcher(DATA_IN, "*.dat"))
            {
                watcher.Created += new FileSystemEventHandler(OnFileCreated);

                watcher.EnableRaisingEvents = true;

                Console.WriteLine("Pressione 'q' para sair");

                while (Console.ReadLine() != "q") ;
            }
        }

        static void ProcessaArquivosExistentes()
        {
            IList<RelatorioDominio> relatorios = new List<RelatorioDominio>();

            foreach (string arquivo in Directory.GetFiles(Pasta.FolderDataIn))
            {
                ProcessarArquivo(arquivo, Path.GetFileName(arquivo));
            }
        }

        static void OnFileCreated(object source, FileSystemEventArgs e)
        {
            ProcessarArquivo(e.FullPath, e.Name);
        }

        static void ProcessarArquivo(string caminhoArquivo, string nomeArquivo)
        {
            Console.WriteLine($"--- Efetuando leitura do arquivo {nomeArquivo}");

            ArquivoRepositorio repositorio = new ArquivoRepositorio(nomeArquivo, caminhoArquivo);
            RelatorioDominio relatorio = repositorio.ProcessaArquivo();

            Console.WriteLine($"------ Gerando relatorio");

            string caminhoRelatorio = relatorio.Exportar(nomeArquivo);

            Console.WriteLine($"------ Relatorio gerado em {caminhoRelatorio}");
        }
    }
}
