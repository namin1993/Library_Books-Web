using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;
public class BookController : Controller
{
    private readonly ApplicationDbContext _db;

    public BookController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        IEnumerable<Book> objBookList = _db.Books;
        return View(objBookList);
    }

    //GET
    public IActionResult Create()
    {
        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Book obj)
    {
        if (obj.Title == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _db.Books.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Book created successfully";
            return RedirectToAction("Index");
        }
        return View(obj);   
    }

    //GET
    public IActionResult Edit(int? id)
    {
        if(id==null || id == 0)
        {
            return NotFound();
        }
        var BookFromDb = _db.Books.Find(id);
        //var BookFromDbFirst = _db.Books.FirstOrDefault(u=>u.Id==id);
        //var BookFromDbSingle = _db.Books.SingleOrDefault(u => u.Id == id);

        if (BookFromDb == null)
        {
            return NotFound();
        }

        return View(BookFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Book obj)
    {
        if (obj.Title == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _db.Books.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Book updated successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var BookFromDb = _db.Books.Find(id);
        //var BookFromDbFirst = _db.Books.FirstOrDefault(u=>u.Id==id);
        //var BookFromDbSingle = _db.Books.SingleOrDefault(u => u.Id == id);

        if (BookFromDb == null)
        {
            return NotFound();
        }

        return View(BookFromDb);
    }

    //POST
    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _db.Books.Find(id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.Books.Remove(obj);
            _db.SaveChanges();
        TempData["success"] = "Book deleted successfully";
        return RedirectToAction("Index");
        
    }
}
