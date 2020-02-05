using System.Collections.Generic;
using Dominio;
using Infraestrutura.Helper;

namespace Infraestrutura.Repositorio
{
    public static class ClienteRepositorio
    {
        public static IList<ClienteDominio> GetClientes(string[] fileLines)
        {
            return fileLines
                    .FiltroPorId("002")
                    .ConverteParaTipo<ClienteDominio>('ç');
        }
    }
}