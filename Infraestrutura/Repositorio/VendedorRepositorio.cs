using System.Collections.Generic;
using Dominio;
using Infraestrutura.Helper;

namespace Infraestrutura.Repositorio
{
    public static class VendedorRepositorio
    {
        public static IList<VendedorDominio> GetVendedores(string[] fileLines)
        {
            return fileLines
                    .FiltroPorId("001")
                    .ConverteParaTipo<VendedorDominio>('ç');
        }
    }
}