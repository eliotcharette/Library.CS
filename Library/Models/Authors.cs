using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Library;

namespace Library.Models
{
  public class Author
  {
    private int _id;
    private string _author;

    public Author (string author, int id = 0)
    {
      _id = id;
      _author = author;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetAuthor()
    {
      return _author;
    }
    public override bool Equals(System.Object otherAuthor)
    {
      if (!(otherAuthor is Author))
      {
        return false;
      }
      else
      {
        Author newAuthor = (Author) otherAuthor;
        return this.GetId().Equals(newAuthor.GetId());
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO authors (author) VALUES (@author);";

      MySqlParameter author = new MySqlParameter();
      author.ParameterName = "@author";
      author.Value = this._author;
      cmd.Parameters.Add(author);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Author> SearchStart(string start)
    {
      List<Author> authorSearch = new List<Author> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM authors WHERE author LIKE @Name;";
      MySqlParameter searchName = new MySqlParameter();
      searchName.ParameterName = "@Name";
      searchName.Value = start + "%";
      cmd.Parameters.Add(searchName);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int AuthorId = rdr.GetInt32(0);
        string Author = rdr.GetString(1);
        Author newAuthor = new Author(Author, AuthorId);
        authorSearch.Add(newAuthor);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return authorSearch;
    }
    public static List<Author> GetAll()
    {
      List<Author> allAuthors = new List<Author> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM authors;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int AuthorId = rdr.GetInt32(0);
        string AuthorAuthor = rdr.GetString(1);
        Author newAuthor = new Author(AuthorAuthor, AuthorId);
        allAuthors.Add(newAuthor);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allAuthors;
    }
    public static Author Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM authors WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int AuthorId = 0;
      string AuthorName = "";

      while(rdr.Read())
      {
        AuthorId = rdr.GetInt32(0);
        AuthorName = rdr.GetString(1);
      }
      Author newAuthor = new Author(AuthorName, AuthorId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newAuthor;
    }

    public List<Book> GetBooks()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT books.* FROM authors
      JOIN authors_books ON (authors.id = authors_books.author_id)
      JOIN books ON (authors_books.book_id = books.id)
      WHERE authors.id = @AuthorId;";

      MySqlParameter authorIdParameter = new MySqlParameter();
      authorIdParameter.ParameterName = "@AuthorId";
      authorIdParameter.Value = _id;
      cmd.Parameters.Add(authorIdParameter);

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
      cmd.CommandText = @"INSERT INTO authors_books (author_id, book_id) VALUES (@AuthorId, @BookId);";

      MySqlParameter author_id = new MySqlParameter();
      author_id.ParameterName = "@AuthorId";
      author_id.Value = _id;
      cmd.Parameters.Add(author_id);

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

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = new MySqlCommand("DELETE FROM authors WHERE id = @AuthorId; DELETE FROM authors_books WHERE author_id = @AuthorId;", conn);
      MySqlParameter authorIdParameter = new MySqlParameter();
      authorIdParameter.ParameterName = "@AuthorId";
      authorIdParameter.Value = this.GetId();

      cmd.Parameters.Add(authorIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM authors;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
