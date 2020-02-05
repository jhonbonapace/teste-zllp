using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Dominio;

namespace App
{
    class Program
    {
        private static readonly string DATA_IN = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "data", "in");
        private static readonly string DATA_OUT = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "data", "out");
        private static readonly string DATA_READED = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "data", "readed");

        static void Main(string[] args)
        {
            ManageDirectories();

            using(FileSystemWatcher watcher = new FileSystemWatcher(DATA_IN, "*.dat"))
            {
                watcher.Created += new FileSystemEventHandler(OnFileCreated);
                
                foreach (string arquivo in Directory.GetFiles(DATA_IN))
                    ProcessaArquivo(arquivo, Path.GetFileName(arquivo));

                watcher.EnableRaisingEvents = true;

                Console.WriteLine("Pressione 'q' para sair");
                
                while(Console.ReadLine() != "q");
            }
        }

        static void ManageDirectories()
        {
            if (!Directory.Exists(DATA_IN)) Directory.CreateDirectory(DATA_IN);
            if (!Directory.Exists(DATA_OUT)) Directory.CreateDirectory(DATA_OUT);
            if (!Directory.Exists(DATA_READED)) Directory.CreateDirectory(DATA_READED);
        }

        static void OnFileCreated(object source, FileSystemEventArgs e)
        {
            ProcessaArquivo(e.FullPath, e.Name);
        }

        static void ProcessaArquivo(string caminhoArquivo, string nomeArquivo)
        {
            Console.WriteLine($"--- Efetuando leitura do arquivo {nomeArquivo}");
            string[] fileLines = File.ReadAllLines(caminhoArquivo);
            
            Console.WriteLine($"------ Arquivo lido e movido para a pasta DataReaded");
            File.Move(caminhoArquivo, Path.Combine(DATA_READED, nomeArquivo), true);

            Console.WriteLine($"------ Iniciando extracao dos vendedores");
            IList<VendedorDominio> vendedores = fileLines
                .Where(x => x.Substring(0, 3) == "001")
                .Select(x => new VendedorDominio(x.Split('ç')))
                .ToList();
            Console.WriteLine($"------ Finalizada extracao dos vendedores");

            Console.WriteLine($"------ Iniciando extracao dos clientes");
            IList<ClienteDominio> clientes = fileLines
                .Where(x => x.Substring(0, 3) == "002")
                .Select(x => new ClienteDominio(x.Split('ç')))
                .ToList();
            Console.WriteLine($"------ Finalizada extracao dos clientes");

            Console.WriteLine($"------ Iniciando extracao dos vendas");
            IList<VendaDominio> vendas = fileLines
                .Where(x => x.Substring(0, 3) == "003")
                .Select(x => new VendaDominio(x.Split('ç')))
                .ToList();
            Console.WriteLine($"------ Finalizada extracao das vendas");

            Console.WriteLine($"------ Gerando relatorio");
            StringBuilder relatorio = new StringBuilder();
            relatorio.AppendLine($"Quantidade de clientes no arquivo de entrada: {clientes.Count}");
            relatorio.AppendLine($"Quantidade de vendedor no arquivo de entrada: {vendedores.Count}");
            relatorio.AppendLine($"ID da venda mais cara: {vendas.OrderByDescending(x => x.ValorTotal).First().ValorTotal}");
            relatorio.AppendLine($"O pior vendedor: {"NULL"}");

            string pathOut = Path.Combine(DATA_OUT, nomeArquivo.Replace(".dat", ".done.dat"));
            File.WriteAllText(pathOut, relatorio.ToString());

            Console.WriteLine($"------ Relatorio gerado em {pathOut}");
        }
    }
}
