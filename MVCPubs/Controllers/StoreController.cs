using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPubs.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MVCPubs.Controllers
{
    public class StoreController : Controller
    {
        private readonly pubsContext _context;
        public StoreController (pubsContext context) { _context = context; }
        public IActionResult Index()
        {
            return View(_context.Stores.ToList());
        }

        public IActionResult Create()
        {
            Store store= new Store();
            return View("Create", store);
        }

        [HttpPost]
        public IActionResult Create (Store store)
        {
            if(ModelState.IsValid) 
            { 
                _context.Stores.Add(store);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create", store);
        }

        public IActionResult Delete(string id)
        {
            Store store = _context.Stores.Find(id);
            if (store != null)
            {
                return View("Delete", store);
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName("Delete")]

        public IActionResult DeleteConfirmed(string id)
        {
            Store store = _context.Stores.Find(id);
            if (store != null) 
            {
                _context.Stores.Remove(store); 
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Delete", store);
        }

        public IActionResult Edit (string id)
        {
            Store store = _context.Stores.Find(id);
            if (store != null)
            {
                return View("Edit", store);
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult Edit([Bind(include: "StorId, StorName, StorAddress, City, State, Zip, Sales")] Store store)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(store).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", store);
        }

        public IActionResult Details(string id)
        {
            Store store = _context.Stores.Find(id);
            if (store != null)
            {
                return View("Details", store);
            }
            return NotFound();
        }

        public IActionResult TraerPorPaisCiudad (string City)
        {
            IEnumerable<Store> ListaStores = BuscarPorPaisCiudad(City);
            return View("Index", ListaStores);
        }

        #region metodos nonAction

        [NonAction]
        public IEnumerable<Store> BuscarPorPaisCiudad (string ciudad)
        {
            IEnumerable<Store> ListaStores = (from s in _context.Stores where s.City.ToLower() == ciudad.ToLower() select s).ToList();

            return ListaStores;
        }
        #endregion
    }
}
