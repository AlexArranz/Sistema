using AplicacionWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacionWeb.Data
{
    public class DdInitilizer
    {
        public static void Initilize(AplicacionWebContext context)
        {
            //Crear base de datos
            context.Database.EnsureCreated();

            //Buscar si existen registros en la tabla categoria
            if (context.Categoria.Any())
            {
                return;
            }
            var categorias = new Categoria[]
            {
                new Categoria{Nombre="Programación", Descripcion="Cursos de programación", Estado =true },
                new Categoria{Nombre="Diseño gráfico", Descripcion="Cursos de diseño gráfico",Estado=true}
            };

            //Recorrer el vector categorias y añadir cada objeto a la base de datos
            foreach (Categoria c in categorias)
            {
                context.Categoria.Add(c);
            }
            context.SaveChanges();
        }
    }
}
