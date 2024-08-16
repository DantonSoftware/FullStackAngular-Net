﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.IRepositorio
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<IEnumerable<T>> ObtenerTodos(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null,
            string incluirPropiedades = null // Include
            );

        Task<T> ObtenerPrimero(
            Expression<Func<T, bool>> ffiltro = null,
            string incluirPropiedades = null
            );

        Task Agregar(T entidad);
        void Remover(T entidad);
    }
}