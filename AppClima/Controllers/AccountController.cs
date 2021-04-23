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
    }
}