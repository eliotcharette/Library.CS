using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Library.Models;

namespace Library.Controllers
{
  public class LibrarianController : Controller
  {
    [HttpGet("/librarians")]
    public ActionResult Index()
    {
      return View();
    }
    [HttpGet("/catalog")]
    public ActionResult Catalog()
    {
    List<Book> allBooks = Book.GetAll();
    return View(allBooks);
    }

    [HttpGet("/addbook")]
    public ActionResult AddBookForm()
    {
      return View(Author.GetAll());
    }
    [HttpPost("/catalog")]
    public ActionResult PostToCatalog()
    {
      Book newBook = new Book(Request.Form["title"]);
      newBook.Save();
      return RedirectToAction("Catalog", Book.GetAll());
    }

    [HttpGet("/addauthor")]
    public ActionResult AddAuthorForm()
    {
      return View();
    }
    [HttpGet("/authors")]
    public ActionResult AuthorPage()
    {
    List<Author> allAuthors = Author.GetAll();
      return View(allAuthors);
    }
    [HttpPost("/authors")]
    public ActionResult AuthorPost()
    {
      Author newAuthor = new Author(Request.Form["name"]);
      newAuthor.Save();
      return RedirectToAction("AuthorPage", Author.GetAll());
    }



    [HttpPost("/book/delete")]
    public ActionResult DeleteOneBook(int bookId)
    {
      Book.Find(bookId).Delete();
      return RedirectToAction("Catalog");
    }

  }

}
