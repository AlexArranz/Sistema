using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AplicacionWeb
{
    public class Paginacion<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalR { get; private set; }

        public Paginacion(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            TotalR = count;
            this.AddRange(items);

        }
        
       /// <summary>
       /// Pagina previa
       /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        /// <summary>
        /// Siguiente pagina
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">Consulta</param>
        /// <param name="pageIndex">Numero de pagina</param>
        /// <param name="pageSize">Tamaño de pagina</param>
        /// <returns>Devuelve la pagina solicitada</returns>
        public static async Task<Paginacion<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new Paginacion<T>(items, count, pageIndex, pageSize);
        }
    }
}
