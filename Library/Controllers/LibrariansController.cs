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
    [HttpGet("/booksearch")]
    public ActionResult BookSearch()
    {
      return View(Book.GetAll());
    }
    [HttpPost("/booksearch")]
    public ActionResult BookSearchStart(string start)
    {
      Book newBook = new Book(Request.Form["start"]);
      return View("BookSearch", Book.SearchStart(start));
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
    [HttpGet("/authorsearch")]
    public ActionResult AuthorSearch()
    {
      return View(Author.GetAll());
    }
    [HttpPost("/authors/search")]
    public ActionResult AuthorSearchStart(string start)
    {
      Author newAuthor = new Author(Request.Form["start"]);
      return View("AuthorSearch", Author.SearchStart(start));
    }
    [HttpGet("/books/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Book books = Book.Find(id);
      List<Author> authors = books.GetAuthors();
      List<Author> allAuthors = Author.GetAll();
      model.Add("books", books);
      model.Add("authors", authors);
      model.Add("allAuthors", allAuthors);
      return View(model);
    }
    [HttpPost("/books/{bookId}/books/new")]
    public ActionResult AddAuthor(int bookId)
    {
      Book book = Book.Find(bookId);
      Author author = Author.Find(int.Parse(Request.Form["author-id"]));
      book.AddAuthor(author);
      return RedirectToAction("Details", new { id = bookId});
    }
    [HttpPost("/book/delete")]
    public ActionResult DeleteOneBook(int bookId)
    {
      Book.Find(bookId).Delete();
      return RedirectToAction("Catalog");
    }

  }

}
