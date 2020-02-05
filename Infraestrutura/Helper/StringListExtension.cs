using System;
using System.Collections.Generic;
using System.Linq;

namespace Infraestrutura.Helper
{
    public static class StringListExtension
    {
        public static IList<string> FiltroPorId(this IList<string> lines, string id, int tamanhoId = 3)
        {
            return lines.Where(x => x.Substring(0, tamanhoId) == id).ToList();
        }

        public static IList<string> FiltroPorId(this IList<string> lines, string id, int tamanhoId, int posicaoId, char separador)
        {
            if (posicaoId == 0)
                throw new ArgumentException("PosicaoId não informado");

            if (posicaoId == 1)
                return lines.Where(x => x.Substring(0, tamanhoId) == id).ToList();
            else
            {
                if (separador == '\0')
                    throw new ArgumentException("Separador não informado");

                return lines.Where(x => x.Split(separador)[posicaoId].Substring(0, tamanhoId) == id).ToList();
            }
        }

        public static IList<T> ConverteParaTipo<T>(this IList<string> lines, char separator) where T : class
        {
            return lines.Select(x => (T)Activator.CreateInstance(typeof(T), new object[] { x.Split(separator) })).ToList();
        }
    }
}