using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Comprension_Lectora.Models;
using Comprension_Lectora.Controllers;

namespace Comprension_Lectora.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Enter(string user, string password)
        {
            try
            {
                // using (CONSTRUCTORAEntities db= new CONSTRUCTORAEntities()) 
                using (DESARROLLO_WEBEntities db = new DESARROLLO_WEBEntities()) 
                {
                    var lst = from d in db.USER
                              where d.Email == user && d.Passwords == password && d.IdEstado ==1
                              select d;

                    if (lst.Count()>0)
                    {
                        USER oUser = lst.First();
                        Session["User"] = oUser;
                        Session["username"] = oUser.PERSONAL_ADMISTRATIVO.Nombre;
                        return Content("1","ADMINISTRADOR");
                        
                    }
                    else
                    {
                        return Content("Usuario Invalido");
                    }


                }

            }
            catch (Exception ex)
            {
                return Content("Lo siento ocurrio un Error"+ex.Message);
            }

        }
        
    }
}