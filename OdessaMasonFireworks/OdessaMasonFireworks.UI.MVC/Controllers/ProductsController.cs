using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OdessaMasonFireworks.DATA.EF.Models;
using System.Drawing;//added for image-related classes
using OdessaMasonFireworks.UI.MVC.Utilities;

namespace OdessaMasonFireworks.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly OdessaMasonFireworksContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(OdessaMasonFireworksContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var odessaMasonFireworksContext = _context.Products.Include(p => p.Brand).Include(p => p.Type);
            return View(await odessaMasonFireworksContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Type)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName");
            ViewData["TypeId"] = new SelectList(_context.ProductTypes1, "TypeId", "TypeName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Description,TypeId,BrandId,UnitsInStock,UnitsOrdered,UnitType,UnitsPerBox,BoxesPerCase,CostPerUnit,PricePerUnit,ProductImage,Image")] Product product)
        {
            if (ModelState.IsValid)
            {
                #region File Upload-Create
                //ONLY BOTHER IF MODEL IS VALID check to see if file was upoloaded
                if (product.Image != null)
                {
                    //there is a file uploaded
                    //check the file type
                    string ext = Path.GetExtension(product.Image.FileName);

                    //create a list of acceptable to check against
                    string[] validExts = { ".jpeg", ".jpg", ".gif", ".png", ".webp", ".jfif" };

                    //verify the uploaded file has a matching extension
                    //AND verify the file size will work with our .NET app
                    if (validExts.Contains(ext.ToLower()) && product.Image.Length < 4_194_303)
                    {
                        //generate unique filename GUID
                        product.ProductImage = Guid.NewGuid() + ext;
                        //to save this file to the right folder on the webserver
                        //to access the web root use the _webHostEnviroment
                        string webRootPath = _webHostEnvironment.WebRootPath;
                        string fullImagePath = webRootPath + "/images/";//gets us to wwwroot/images

                        //create a MemoryStream to read the image into server memory
                        using (var memoryStream = new MemoryStream())
                        {
                            await product.Image.CopyToAsync(memoryStream);//transfer file from request to server memorty

                            using (var img = Image.FromStream(memoryStream))//setting up an image not a file
                            {
                                //now send the image to ImageUtility to resize for base file and also thumbnail
                                int maxImageSize = 500;//in pixles
                                int maxThumbSize = 100;

                                ImageUtility.ResizeImage(fullImagePath, product.ProductImage, img, maxImageSize, maxThumbSize);//resixed and saved
                            }
                        }
                    }
                }
                else
                {
                    //in this case no file was uploaded, so assign a default file name
                    //IF YOU HAVEN'T ALREADY, GET THE DEFAULT FILE
                    product.ProductImage = "NoImage.png";
                }
                #endregion

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", product.BrandId);
            ViewData["TypeId"] = new SelectList(_context.ProductTypes1, "TypeId", "TypeName", product.TypeId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", product.BrandId);
            ViewData["TypeId"] = new SelectList(_context.ProductTypes1, "TypeId", "TypeName", product.TypeId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Description,TypeId,BrandId,UnitsInStock,UnitsOrdered,UnitType,UnitsPerBox,BoxesPerCase,CostPerUnit,PricePerUnit,ProductImage,Image")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                #region EDIT File Upload
                //retain old image file name so we can delete if a new file was uploaded
                string oldImageName = product.ProductImage;

                //Check if the user uploaded a file
                if (product.Image != null)
                {
                    //get the file's extension
                    string ext = Path.GetExtension(product.Image.FileName);

                    //list valid extensions
                    string[] validExts = { ".jpeg", ".jpg", ".png", ".gif", ".webp", ".jfif" };

                    //check the file's extension against the list of valid extensions
                    if (validExts.Contains(ext.ToLower()) && product.Image.Length < 4_194_303)
                    {
                        //generate a unique file name
                        product.ProductImage = Guid.NewGuid() + ext;
                        //build our file path to save the image
                        string webRootPath = _webHostEnvironment.WebRootPath;
                        string fullPath = webRootPath + "/images/";

                        //Delete the old image
                        if (oldImageName != "NoImage.png")
                        {
                            ImageUtility.Delete(fullPath, oldImageName);
                        }

                        //Save the new image to webroot
                        using (var memoryStream = new MemoryStream())
                        {
                            await product.Image.CopyToAsync(memoryStream);
                            using (var img = Image.FromStream(memoryStream))
                            {
                                int maxImageSize = 500;
                                int maxThumbSize = 100;
                                ImageUtility.ResizeImage(fullPath, product.ProductImage, img, maxImageSize, maxThumbSize);
                            }
                        }

                    }
                }
                #endregion

                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", product.BrandId);
            ViewData["TypeId"] = new SelectList(_context.ProductTypes1, "TypeId", "TypeName", product.TypeId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Type)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public JsonResult AjaxDelete(int id)
        {
            Product product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();

            string confirmMessage = $"Deleted {product.ProductName} from the database.";
            return Json(new { id = id, message = confirmMessage });
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'OdessaMasonFireworksContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
