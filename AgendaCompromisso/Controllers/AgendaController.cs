using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaCompromissos.Models;
using AgendaCompromissos.Models.ViewModels;
using AgendaCompromissos.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgendaCompromissos.Controllers {
    public class AgendaController : Controller {

        private readonly AgendaService _agendaService;

        public AgendaController(AgendaService agendaService) {
            _agendaService = agendaService;
        }

        public async Task<ActionResult> Index() {
            List<Consultas> lista = await _agendaService.FindAllAsync();
            return View(lista);
        }

        public async Task<ActionResult> Details(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Código da consulta inválido ou nulo." });
            }

            var obj = await _agendaService.FindByIdAsync(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Código da consulta não localizado." });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Consultas consulta) {
            if (!ModelState.IsValid) {
                var viewModel = new ConsultaViewModel { Consultas = consulta };
                return View(viewModel);
            }

            //A data final não pode ser menor que a data inicial
            if (consulta.DataHoraFinal <= consulta.DataHoraInicial) {
                return RedirectToAction(nameof(Error), new { message = "Dia e hora final da consulta não permitido" });
            }

            //O sistema não deve permitir o agendamento de duas ou mais consultas no mesmo range de datas
            var obj = await _agendaService.FindByDateAsync(consulta.DataHoraInicial, consulta.DataHoraFinal);

            if (obj == null) {
                await _agendaService.InsertAsync(consulta);
                return RedirectToAction(nameof(Index));
            }
            else {
                return RedirectToAction(nameof(Error), new { message = "Data e hora já agendada para este médico" });
            }
        }

        private object Error() {
            throw new NotImplementedException();
        }

        public ActionResult Create() {
            var viewModel = new ConsultaViewModel() ;

            return View(viewModel);
        }

        public ActionResult Edit(int id) {
            return View();
        }

        public ActionResult Delete(int id) {
            return View();
        }
    }
}