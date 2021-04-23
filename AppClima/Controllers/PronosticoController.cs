using modelo;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Data;
using System.Net;

namespace AppClima.Controllers
{
    public class PronosticoController : Controller
    {
        ClimaContext _context = new ClimaContext();

        //GET: Pronostico
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> PronosticoClima()
        {
            return View(await _context.Pronostico.ToListAsync());
        }

        #region CRUD

        public ActionResult Create()
        {
            return View(new PronosticoClima());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PronosticoClima entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entidad.UsuarioModifica = entidad.UsuarioModifica is null ? "SYSADMIN" : entidad.UsuarioModifica;
                    entidad.ProgramaModifica = entidad.ProgramaModifica is null ? nameof(PronosticoController) : entidad.UsuarioModifica;
                    entidad.EquipoModifica = entidad.EquipoModifica is null ? Environment.MachineName : entidad.UsuarioModifica;
                    entidad.FechaModifica = DateTime.Now;

                    _context.Pronostico.Add(entidad);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("PronosticoClima");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "No se pueden guardar los cambios.Vuelva a intentarlo y, si el problema persiste, consulte con el administrador del sistema.");
            }
            return View(entidad);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pronostico = await _context.Pronostico.FindAsync(id);

            if (pronostico is null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View("Edit", pronostico);

        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(int? id, PronosticoClima entidad)
        {
            try
            {
                if (id != entidad.Cod_Div)
                {
                    return HttpNotFound();
                }

                entidad.UsuarioModifica = entidad.UsuarioModifica is null ? "SYSADMIN" : entidad.UsuarioModifica;
                entidad.ProgramaModifica = entidad.ProgramaModifica is null ? nameof(PronosticoController) : entidad.UsuarioModifica;
                entidad.EquipoModifica = entidad.EquipoModifica is null ? Environment.MachineName : entidad.UsuarioModifica;
                entidad.FechaModifica = DateTime.Now;

                _context.Entry(entidad).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return RedirectToAction("PronosticoClima");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "No se pueden guardar los cambios.Vuelva a intentarlo y, si el problema persiste, consulte con el administrador del sistema.");
            }

            return View(entidad);
        }

        public async Task<ActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Error al eliminar.Vuelva a intentarlo y, si el problema persiste, consulte con el administrador del sistema.";
            }
            var pronostico = await _context.Pronostico.FindAsync(id);
            if (pronostico == null)
            {
                return HttpNotFound();
            }
            return View(pronostico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var pronostico = await _context.Pronostico.FindAsync(id);
                _context.Pronostico.Remove(pronostico);
                await _context.SaveChangesAsync();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("PronosticoClima");
        }

        #endregion
    }
}