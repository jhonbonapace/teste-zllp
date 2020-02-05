using System.Collections.Generic;
using Dominio;
using Infraestrutura.Helper;

namespace Infraestrutura.Repositorio
{
    public static class VendaRepositorio
    {
        public static IList<VendaDominio> GetVendas(string[] fileLines)
        {
            return fileLines
                    .FiltroPorId("003")
                    .ConverteParaTipo<VendaDominio>('ç');
        }
    }
}