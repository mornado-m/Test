using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ASP_Test.Models;
using WebGrease.Css.Extensions;

namespace ASP_Test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var books = new List<Book>();
            try
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    dbContext.Books.ForEach(book => books.Add(new Book
                    {
                        Author = new Author {first_name = book.Author.first_name, last_name = book.Author.last_name},
                        book_id = book.book_id,
                        book_name = book.book_name,
                        available_count = book.available_count,
                        price = book.price
                    }));
                }
            }
            catch (SqlException e)
            {
                ViewBag.ErrorMessage = e.Message;
                ViewBag.Books = new List<Book>();
                return View();
            }

            ViewBag.ErrorMessage = "";
            ViewBag.Books = books;
            return View();
        }

        public ActionResult Sales()
        {
            var sales = new List<Sale>();
            try
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    dbContext.Sales.ForEach(sale => sales.Add(new Sale
                    {
                        Book = new Book
                        {
                            book_name = sale.Book.book_name,
                            Author = new Author
                            {
                                first_name = sale.Book.Author.first_name,
                                last_name = sale.Book.Author.last_name
                            }
                        },
                        sale_date = sale.sale_date,
                        books_count = sale.books_count,
                        price = sale.price
                    }));
                }
            }
            catch (SqlException e)
            {
                ViewBag.ErrorMessage = e.Message;
                ViewBag.Sales = new List<Sale>();
                return View();
            }

            ViewBag.ErrorMessage = "";
            ViewBag.Sales = sales;
            return View();
        }

        public ActionResult Authors()
        {
            var authors = new List<Author>();
            try
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    dbContext.Authors.ForEach(author => authors.Add(new Author
                    {
                        author_id = author.author_id,
                        first_name = author.first_name,
                        last_name = author.last_name,
                        DOB = author.DOB,
                        info = author.info
                    }));
                }
            }
            catch (SqlException e)
            {
                ViewBag.ErrorMessage = e.Message;
                ViewBag.Authors = new List<Author>();
                return View();
            }

            ViewBag.ErrorMessage = "";
            ViewBag.Authors = authors;
            return View();
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }

        [HttpPost]
        public ActionResult Buy(Sale sale)
        {
            sale.sale_date = DateTime.Now;

            try
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    var bk = dbContext.Books.First(book => book.book_id == sale.book_id);
                    sale.price = bk.price * sale.books_count;

                    ViewBag.BookName = bk.book_name;

                    if (bk.available_count < sale.books_count || sale.books_count < 1)
                    {
                        ViewBag.ErrorMessage = "Sorry, we haven't enough books. Please, try later.";
                        return View("ConfirmBuy");
                    }
                }
            }
            catch (SqlException e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View("ConfirmBuy");
            }
            catch (NullReferenceException e)
            {
                ViewBag.ErrorMessage = "Error. Can't find book";
                return View("ConfirmBuy");
            }

            ViewBag.Sale = sale;
            ViewBag.ErrorMessage = "";
            return View("ConfirmBuy");
        }


        [HttpPost]
        public ActionResult Confirm(Sale sale)
        {
            if (Request.Form["ok"] != null)
            {
                try
                {
                    using (var dbContext = new Bookshop_DBContext())
                    {
                        var bk = dbContext.Books.First(book => book.book_id == sale.book_id);
                        bk.available_count -= sale.books_count;
                        dbContext.Sales.Add(sale);
                        dbContext.SaveChanges();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return Redirect("Index");
        }


        public ActionResult EditBook(int id)
        {
            ViewBag.BookId = id;
            try
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    var book = dbContext.Books.First(b => b.book_id == id);
                    ViewBag.BookName = book.book_name;
                    ViewBag.Author = book.Author.first_name + " " + book.Author.last_name;
                    ViewBag.Count = book.available_count;
                    ViewBag.Price = book.price;
                    ViewBag.Description = book.description;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return View();
        }

        [HttpPost]
        public ActionResult EditBook(Book bk)
        {
            if (Request.Form["ok"] != null)
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    var book = dbContext.Books.First(b => b.book_id == bk.book_id);
                    book.available_count = bk.available_count;
                    book.price = bk.price;
                    book.description = bk.description;
                    dbContext.SaveChanges();
                }
            }

            return Redirect("Index");
        }


        public ActionResult EditAuthor(int id)
        {
            ViewBag.AuthorId = id;
            try
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    var author = dbContext.Authors.First(a => a.author_id == id);
                    ViewBag.AuthorName = author.first_name + " " + author.last_name;
                    ViewBag.DOB = author.DOB;
                    ViewBag.Info = author.info;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return View();
        }

        [HttpPost]
        public ActionResult EditAuthor(Author auth)
        {
            if (Request.Form["ok"] != null)
            {
                try
                {
                    using (var dbContext = new Bookshop_DBContext())
                    {
                        var author = dbContext.Authors.First(a => a.author_id == auth.author_id);
                        author.info = auth.info;
                        dbContext.SaveChanges();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return Redirect("Authors");
        }

        [HttpGet]
        public ActionResult DeleteBook(int id)
        {
            ViewBag.BookId = id;
            try
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    var book = dbContext.Books.First(b => b.book_id == id);
                    ViewBag.BookName = book.book_name;
                }
            }
            catch (SqlException e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View("ConfirmDeleteBook");
            }

            ViewBag.ErrorMessage = "";
            return View("ConfirmDeleteBook");
        }

        [HttpPost]
        public ActionResult ConfirmDeleteBook(int id)
        {
            if (Request.Form["ok"] != null)
            {
                try
                {
                    using (var dbContext = new Bookshop_DBContext())
                    {
                        var sales = dbContext.Sales.Where(sale => sale.book_id == id);
                        foreach (var sale in sales)
                            dbContext.Sales.Remove(sale);

                        var book = dbContext.Books.First(b => b.book_id == id);
                        dbContext.Books.Remove(book);

                        dbContext.SaveChanges();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return Redirect("Index");
        }


        [HttpGet]
        public ActionResult DeleteAuthor(int id)
        {
            ViewBag.AuthorId = id;
            try
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    var author = dbContext.Authors.First(a => a.author_id == id);
                    ViewBag.AuthorName = author.first_name + " " + author.last_name;
                }
            }
            catch (SqlException e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View("ConfirmDeleteAuthor");
            }

            ViewBag.ErrorMessage = "";
            return View("ConfirmDeleteAuthor");
        }

        [HttpPost]
        public ActionResult ConfirmDeleteAuthor(int id)
        {
            if (Request.Form["ok"] != null)
            {
                try
                {
                    using (var dbContext = new Bookshop_DBContext())
                    {
                        var sales = dbContext.Sales.Where(sale => sale.Book.author_id == id);
                        foreach (var sale in sales)
                            dbContext.Sales.Remove(sale);

                        var books = dbContext.Books.Where(b => b.author_id == id);
                        foreach (var book in books)
                            dbContext.Books.Remove(book);

                        var author = dbContext.Authors.First(a => a.author_id == id);
                        dbContext.Authors.Remove(author);

                        dbContext.SaveChanges();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return Redirect("Authors");
        }


        public ActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAuthor(Author author)
        {
            if (Request.Form["ok"] != null)
            {
                try
                {
                    using (var dbContext = new Bookshop_DBContext())
                    {
                        dbContext.Authors.Add(author);
                        dbContext.SaveChanges();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return Redirect("Authors");
        }


        public ActionResult AddBook()
        {
            try
            {
                var authors = new List<SelectListItem>();
                using (var dbContext = new Bookshop_DBContext())
                {
                    dbContext.Authors.ForEach(author => authors.Add(new SelectListItem
                    {
                        Text = author.first_name + " " + author.last_name,
                        Value = author.first_name + " " + author.last_name
                    }));
                }
                ViewBag.Authors = authors;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddBook(NewBookModel newBook)
        {
            if (Request.Form["ok"] != null)
            {
                var book = new Book
                {
                    book_name = newBook.book_name,
                    available_count = newBook.available_count,
                    price = newBook.price,
                    description = newBook.description
                };
                try
                {
                    using (var dbContext = new Bookshop_DBContext())
                    {
                        book.author_id = dbContext.Authors
                            .First(a => newBook.author_name.Equals(a.first_name + " " + a.last_name)).author_id;

                        dbContext.Books.Add(book);
                        dbContext.SaveChanges();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return Redirect("Index");
        }
    }
}