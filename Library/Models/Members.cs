using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Library;

namespace Library.Models
{
  public class Member
  {
    private int _id;
    private string _member;

    public Member (string member, int id = 0)
    {
      _id = id;
      _member = member;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetMember()
    {
      return _member;
    }
    public override bool Equals(System.Object otherMember)
    {
      if (!(otherMember is Member))
      {
        return false;
      }
      else
      {
        Member newMember = (Member) otherMember;
        return this.GetId().Equals(newMember.GetId());
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO members (member) VALUES (@member);";

      MySqlParameter member = new MySqlParameter();
      member.ParameterName = "@member";
      member.Value = this._member;
      cmd.Parameters.Add(member);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Member> GetAll()
    {
      List<Member> allMembers = new List<Member> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM members;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int MemberId = rdr.GetInt32(0);
        string MemberMember = rdr.GetString(1);
        Member newMember = new Member(MemberMember, MemberId);
        allMembers.Add(newMember);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allMembers;
    }
    public static Member Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM members WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int MemberId = 0;
      string MemberName = "";

      while(rdr.Read())
      {
        MemberId = rdr.GetInt32(0);
        MemberName = rdr.GetString(1);
      }
      Member newMember = new Member(MemberName, MemberId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newMember;
    }

    public List<Book> GetBooks()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT books.* FROM members
      JOIN members_books ON (members.id = members_books.member_id)
      JOIN books ON (members_books.book_id = books.id)
      WHERE members.id = @MemberId;";

      MySqlParameter memberIdParameter = new MySqlParameter();
      memberIdParameter.ParameterName = "@MemberId";
      memberIdParameter.Value = _id;
      cmd.Parameters.Add(memberIdParameter);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Book> books = new List<Book>{};

      while(rdr.Read())
      {
        int BookId = rdr.GetInt32(0);
        string BookTitle = rdr.GetString(1);
        Book newBook = new Book(BookTitle);
        books.Add(newBook);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return books;
    }
    public void AddBook(Book newBook)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO members_books (member_id, book_id) VALUES (@MemberId, @BookId);";

      MySqlParameter member_id = new MySqlParameter();
      member_id.ParameterName = "@MemberId";
      member_id.Value = _id;
      cmd.Parameters.Add(member_id);

      MySqlParameter book_id = new MySqlParameter();
      book_id.ParameterName = "@BookId";
      book_id.Value = newBook.GetId();
      cmd.Parameters.Add(book_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
