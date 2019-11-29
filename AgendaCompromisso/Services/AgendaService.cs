using AgendaCompromissos.DataContexts;
using AgendaCompromissos.Models;
using AgendaCompromissos.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace AgendaCompromissos.Services {
    public class AgendaService {
        private readonly AgendaContext _context;

        public AgendaService(AgendaContext context) {
            _context = context;
        }

        public async Task InsertAsync(Consultas consulta) {
            _context.Add(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Consultas>> FindAllAsync() {
            return await _context.Consulta.ToListAsync();
        }

        public async Task<Consultas> FindByIdAsync(int id) {
            return await _context.Consulta.FirstOrDefaultAsync(obj => obj.ConsultaId == id);
        }

        public async Task<Consultas> FindByDateAsync(DateTime? dtInicial, DateTime? dtFim) {

            IQueryable<Consultas> listaConsultas = from obj in _context.Consulta select obj;

            return await listaConsultas
                    .Where(data => (data.DataHoraInicial <= dtInicial && data.DataHoraFinal >= dtInicial) ||
                        (data.DataHoraInicial <= dtFim && data.DataHoraFinal >= dtFim)).OrderBy(d => d.DataHoraInicial).FirstOrDefaultAsync();
        }
    }
}
