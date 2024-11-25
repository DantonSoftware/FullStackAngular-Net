﻿using Data.Interfaces.IRepositorio;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio
{
    public class AntecedenteRepositorio : Repositorio<Antecedente>, IAntecedenteRepositorio
    {

        private readonly ApplicationDbContext _db;

        public AntecedenteRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Antecedente antecedente)
        {
            var antecedentDb = _db.Antecedentes.FirstOrDefault(e => e.Id == antecedente.Id);
            if (antecedentDb != null)
            {
                antecedentDb.FechaActualizacion = DateTime.Now;
                antecedentDb.Observacion = antecedente.Observacion;
                                
                _db.SaveChanges();
            }
        }
    }
}