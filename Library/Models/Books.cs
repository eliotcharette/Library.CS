using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Library;

namespace Library.Models
{
  public class Book
  {
    private int _id;
    private string _book;
    public Book (string book, int id = 0)
    {

      _id = id;
      _book = book;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetBook()
    {
      return _book;
    }
    public override bool Equals(System.Object otherBook)
    {
      if (!(otherBook is Book))
      {
        return false;
      }
      else
      {
        Book newBook = (Book) otherBook;
        return this.GetId().Equals(newBook.GetId());
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO books (book) VALUES (@book);";

      MySqlParameter book = new MySqlParameter();
      book.ParameterName = "@book";
      book.Value = this._book;
      cmd.Parameters.Add(book);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Book> GetAll()
    {
      List<Book> allBooks = new List<Book> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM books;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int BookId = rdr.GetInt32(0);
        string BookBook = rdr.GetString(1);
        Book newBook = new Book(BookBook, BookId);
        allBooks.Add(newBook);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allBooks;
    }
    public static Book Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM books WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int BookId = 0;
      string BookTitle = "";

      while(rdr.Read())
      {
        BookId = rdr.GetInt32(0);
        BookTitle = rdr.GetString(1);
      }
      Book newBook = new Book(BookTitle, BookId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newBook;
    }

    public List<Author> GetAuthors()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT authors.* FROM books
                JOIN books_authors ON (books.id = books_authors.book_id)
                JOIN authors ON (books_authors.author_id = authors.id)
                WHERE books.id = @BookId;";

            MySqlParameter bookIdParameter = new MySqlParameter();
            bookIdParameter.ParameterName = "@BookId";
            bookIdParameter.Value = _id;
            cmd.Parameters.Add(bookIdParameter);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Author> authors = new List<Author>{};

            while(rdr.Read())
            {
              int AuthorId = rdr.GetInt32(0);
              string AuthorName = rdr.GetString(1);
              Author newAuthor = new Author(AuthorName, AuthorId);
              authors.Add(newAuthor);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return authors;
        }
    public void AddAuthor(Author newAuthor)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO books_authors (book_id, author_id) VALUES (@BookId, @AuthorId);";

            MySqlParameter book_id = new MySqlParameter();
            book_id.ParameterName = "@BookId";
            book_id.Value = _id;
            cmd.Parameters.Add(book_id);

            MySqlParameter author_id = new MySqlParameter();
            author_id.ParameterName = "@AuthorId";
            author_id.Value = newAuthor.GetId();
            cmd.Parameters.Add(author_id);

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

      MySqlCommand cmd = new MySqlCommand("DELETE FROM books WHERE id = @BookId; DELETE FROM authors_books WHERE book_id = @BookId;", conn);
      MySqlParameter bookIdParameter = new MySqlParameter();
      bookIdParameter.ParameterName = "@BookId";
      bookIdParameter.Value = this.GetId();

      cmd.Parameters.Add(bookIdParameter);
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
      cmd.CommandText = @"DELETE FROM books;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }




    // public void Edit(string newWine)
    //   {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"UPDATE wines SET wine = @newWine WHERE id = @searchId;";
    //   MySqlParameter searchId = new MySqlParameter();
    //   searchId.ParameterName = "@searchId";
    //   searchId.Value = _id;
    //   cmd.Parameters.Add(searchId);
    //
    //   MySqlParameter wine = new MySqlParameter();
    //   wine.ParameterName = "@newWine";
    //   wine.Value = newWine;
    //   cmd.Parameters.Add(wine);
    //
    //   cmd.ExecuteNonQuery();
    //   _wine = newWine;
    //
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    // }
  }
}
