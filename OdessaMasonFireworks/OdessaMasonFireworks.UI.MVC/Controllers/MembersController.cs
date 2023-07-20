using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OdessaMasonFireworks.DATA.EF.Models;
using OdessaMasonFireworks.UI.MVC.Utilities;

namespace OdessaMasonFireworks.UI.MVC.Controllers
{

    public class MembersController : Controller
    {
        private readonly OdessaMasonFireworksContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public MembersController(OdessaMasonFireworksContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnviroment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin")]
        // GET: Members
        public async Task<IActionResult> Index()
        {
            return _context.Members != null ?
                        View(await _context.Members.ToListAsync()) :
                        Problem("Entity set 'OdessaMasonFireworksContext.Members'  is null.");
        }

        public IActionResult Rent()
        {
            return View();
        }

        public async Task<IActionResult> TiledMembers()
        {
            return _context.Members != null ?
                         View(await _context.Members.ToListAsync()) :
                         Problem("Entity set 'OdessaMasonFireworksContext.Members'  is null.");
        }

        [Authorize(Roles = "Admin")]
        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        [Authorize(Roles = "Admin")]
        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,FirstName,LastName,Position,JoinDate,Address,City,State,PostalCode,Email,Phone,MemberImage,Image")] Member member)
        {
            if (ModelState.IsValid)
            {
                if (member.Image != null)
                {
                    string ext = Path.GetExtension(member.Image.FileName);
                    string[] validExts = { ".jpeg", ".jpg", ".gif", ".png", ".webp", ".jfif" };

                    if (validExts.Contains(ext.ToLower()) && member.Image.Length < 4_194_303)
                    {
                        member.MemberImage = Guid.NewGuid() + ext;
                        string webRootPath = _webHostEnviroment.WebRootPath;
                        string fullImagePath = webRootPath + "/images/";
                        using (var memoryStream = new MemoryStream())
                        {
                            await member.Image.CopyToAsync(memoryStream);

                            using (var img = Image.FromStream(memoryStream))
                            {
                                int maxImageSize = 500;
                                int maxThumbSize = 100;

                                ImageUtility.ResizeImage(fullImagePath, member.MemberImage, img, maxImageSize, maxThumbSize);
                            }
                        }
                    }
                }
                else
                {
                    member.MemberImage = "NoImage.png";
                }
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        [Authorize(Roles = "Admin")]
        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,FirstName,LastName,Position,JoinDate,Address,City,State,PostalCode,Email,Phone,MemberImage,Image")] Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string oldImageName = member.MemberImage;

                if (member.Image != null)
                {
                    string ext = Path.GetExtension(member.Image.FileName);

                    string[] validExts = { ".jpeg", ".jpg", ".png", ".gif", ".webp", ".jfif" };

                    if (validExts.Contains(ext.ToLower()) && member.Image.Length < 4_194_303)
                    {
                        member.MemberImage = Guid.NewGuid() + ext;
                        string webRootPath = _webHostEnviroment.WebRootPath;
                        string fullPath = webRootPath + "/images/";

                        if (oldImageName != "NoImage.png")
                        {
                            ImageUtility.Delete(fullPath, oldImageName);
                        }

                        using (var memoryStream = new MemoryStream())
                        {
                            await member.Image.CopyToAsync(memoryStream);
                            using (var img = Image.FromStream(memoryStream))
                            {
                                int maxImageSize = 500;
                                int maxThumbSize = 100;
                                ImageUtility.ResizeImage(fullPath, member.MemberImage, img, maxImageSize, maxThumbSize);
                            }
                        }

                    }
                }
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberId))
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
            return View(member);
        }

        [Authorize(Roles = "Admin")]
        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        [Authorize(Roles = "Admin")]
        public JsonResult AjaxDelete(int id)
        {
            Member member = _context.Members.Find(id);
            _context.Members.Remove(member);
            _context.SaveChanges();

            string confirmMessage = $"Deleted {member.FirstName + " " + member.LastName} from the database.";
            return Json(new { id = id, message = confirmMessage });
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Members == null)
            {
                return Problem("Entity set 'OdessaMasonFireworksContext.Members'  is null.");
            }
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
    }
}
