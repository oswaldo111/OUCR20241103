using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OUCR20241103.Models;

namespace OUCR20241103.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly OUCR20241103DBContext _context;

        public ProveedorController(OUCR20241103DBContext context)
        {
            _context = context;
        }

        // GET: Proveedor
        public async Task<IActionResult> Index()
        {
                

              return _context.Proveedor != null ? 
                          View(await _context.Proveedor.ToListAsync()) :
                          Problem("Entity set 'OUCR20241103DBContext.Proveedor'  is null.");

                        
        }

        // GET: Proveedor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || _context.Proveedor == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedor
                .Include(s=> s.Direccione)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedor == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Details";
            return View(proveedor);

        }

        // GET: Proveedor/Create
        public IActionResult Create()
        {
            var proveedor = new Proveedor
            {
                Nombre = " ",
                Dui = " ",

                Direccione = new List<Direccione>()
            };
            proveedor.Direccione.Add(new Direccione
            {
                Calle = " "
            });
            ViewBag.Accion = "Create";
            return View(proveedor);
        }

        // POST: Proveedor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Dui,Direccione")] Proveedor proveedor)
        {

                if(ModelState.IsValid)
                {
                _context.Add(proveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

                }
                return View(proveedor);
          
  
        }

        [HttpPost]
        public ActionResult AgregarDetalles([Bind("Id,Nombre,Dui,Direccione")] Proveedor proveedor, string accion)
        {
            
            proveedor.Direccione.Add(new Direccione { Calle = "" });

            ViewBag.Accion = accion;
            return View(accion, proveedor);
        }

         public IActionResult EliminarDetalles([Bind("Id,Nombre,Dui,Direccione")] Proveedor proveedor,
           int index, string accion)
           
           {
        
            
              var det = proveedor.Direccione[index];
            if (accion == "Edit" && det.Id > 0)
            {
                det.Id = det.Id * -1;
            }
            else
            {
                Console.WriteLine(index+"nada");
                proveedor.Direccione.RemoveAt(index);
            }

            ViewBag.Accion = accion;
            return View(accion, proveedor);

  
        }


        // GET: Proveedor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
             if (id == null || _context.Proveedor == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedor
                .Include(s => s.Direccione)
                .FirstAsync(s => s.Id == id);
            if (proveedor == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Edit";
            return View(proveedor);
        
        }

        // POST: Proveedor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Dui,Direccione")] Proveedor proveedor)
        {
            if (id != proveedor.Id)
            {
                return NotFound();
            }

            try
            {
                // Obtener los datos de la base de datos que van a ser modificados
                var proveedorUpdate = await _context.Proveedor
                        .Include(s => s.Direccione)
                        .FirstAsync(s => s.Id == proveedor.Id);
                proveedorUpdate.Nombre = proveedor.Nombre;
                proveedorUpdate.Dui = proveedor.Dui;
                // Obtener todos los detalles que seran nuevos y agregarlos a la base de datos
                var detNew = proveedor.Direccione.Where(s => s.Id == 0);
                foreach (var d in detNew)
                {
                    proveedorUpdate.Direccione.Add(d);
                }
                // Obtener todos los detalles que seran modificados y actualizar a la base de datos
                var detUpdate = proveedor.Direccione.Where(s => s.Id > 0);
                foreach (var d in detUpdate)
                {
                    var det = proveedorUpdate.Direccione.FirstOrDefault(s => s.Id == d.Id);
                    det.Calle = d.Calle;
                    det.NumeoDeCasa = d.NumeoDeCasa;
                  
                }
                // Obtener todos los detalles que seran eliminados y actualizar a la base de datos
                var delDet = proveedor.Direccione.Where(s => s.Id < 0).ToList();
                if(delDet!=null && delDet.Count > 0)
                {
                    foreach (var d in delDet)
                    {
                        d.Id = d.Id * -1;
                        var det = proveedorUpdate.Direccione.FirstOrDefault(s => s.Id == d.Id);
                        _context.Remove(det);
                       // proveedorUpdate.Direccioness.Remove(det);
                    }
                }                
                // Aplicar esos cambios a la base de datos
                _context.Update(proveedorUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(proveedor.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        
        }

        // GET: Proveedor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Proveedor == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedor
            .Include( s => s.Direccione)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            ViewBag.Accion = "Delete";

            return View(proveedor);
        }

        // POST: Proveedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           if (_context.Proveedor == null)
            {
                return Problem("Entity set 'OUCR20241103DBContext.Proveedor'  is null.");
            }
            var proveedor = await _context.Proveedor
                .FindAsync(id);
            if (proveedor != null)
            {
                _context.Proveedor.Remove(proveedor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        
        }

        private bool ProveedorExists(int id)
        {
          return (_context.Proveedor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
