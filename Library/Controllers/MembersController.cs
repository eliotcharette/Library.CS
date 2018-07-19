using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Library.Models;

namespace Library.Controllers
{
  public class MemberController : Controller
  {
    [HttpGet("/members")]
    public ActionResult Index()
    {
      return View();
    }
    [HttpGet("/member/catalog")]
    public ActionResult Members()
    {
      List<Member> allMembers = Member.GetAll();
      return View(allMembers);
    }
    [HttpGet("/checkout/{id}")]
public ActionResult Checkout(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Member newMember = Member.Find(id);
      List<Book> books = newMember.GetBooks();
      List<Book> allBooks = Book.GetAll();
      model.Add("newMember", newMember);
      model.Add("books", books);
      model.Add("allBooks", allBooks);
      return View(model);
    }
    [HttpPost("/checkout/{memberId}/book")]
    public ActionResult CheckoutBook(int memberId)
    {
      Member member = Member.Find(memberId);
      Book book = Book.Find(int.Parse(Request.Form["book-id"]));
      book.AddMember(member);
      return RedirectToAction("Checkout", new { id = memberId});
    }
    [HttpGet("/member/form")]
    public ActionResult CreateMember()
    {
      List<Member> allMembers = Member.GetAll();
        return View(allMembers);
    }
    [HttpPost("/member/form")]
    public ActionResult PostMember()
    {
      Member newMember = new Member(Request.Form["name"]);
      newMember.Save();
      return RedirectToAction("CreateMember", Member.GetAll());
    }
  }
}
