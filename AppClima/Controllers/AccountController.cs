using modelo;
using Repo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppClima.Controllers
{
    public class AccountController : Controller
    {
        ClimaContext _context = new ClimaContext();
        // GET: Account
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = await _context.Usuario
                                            .Where(w => w.Email == login.Email.ToUpper() &&
                                                   w.Password == login.Password.ToUpper())
                                            .FirstOrDefaultAsync();
                    if (usuario is null)
                    {
                        throw new DataException("Email and Password no validos");
                    }

                    Session["Usuario"] = usuario.IdUsuario;
                    return RedirectToAction("PronosticoClima","Pronostico");
                }
            }
            catch (DataException e)
            {
                ModelState.AddModelError("", e.Message);
            }
            
            return View(new LoginModel());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Registrar", new Usuario());
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registrar(Usuario entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entidad.UsuarioModifica = entidad.UsuarioModifica is null ? "SYSADMIN" : entidad.UsuarioModifica;
                    entidad.ProgramaModifica = entidad.ProgramaModifica is null ? nameof(PronosticoController) : entidad.UsuarioModifica;
                    entidad.EquipoModifica = entidad.EquipoModifica is null ? Environment.MachineName : entidad.UsuarioModifica;
                    entidad.FechaModifica = DateTime.Now;

                    _context.Usuario.Add(entidad);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "No se pueden guardar los cambios.Vuelva a intentarlo y, si el problema persiste, consulte con el administrador del sistema.");
            }
            return View("Registrar", entidad);
        }

    }
}