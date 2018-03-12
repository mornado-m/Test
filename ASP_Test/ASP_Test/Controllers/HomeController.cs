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
            var model = new IndexViewModel();
            try
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    dbContext.Books.ForEach(book => model.Books.Add(new Book
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
                model.ErrorMessage = e.Message;
                return View(model);
            }
            
            return View(model);
        }

        public ActionResult Sales()
        {
            var model = new SalesViewModel();
            try
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    dbContext.Sales.ForEach(sale => model.Sales.Add(new Sale
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
                model.ErrorMessage = e.Message;
                return View(model);
            }
            
            return View(model);
        }

        public ActionResult Authors()
        {
            var model = new AuthorsViewModel();
            try
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    dbContext.Authors.ForEach(author => model.Authors.Add(new Author
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
                model.ErrorMessage = e.Message;
                return View(model);
            }
            
            return View(model);
        }
        
        [HttpGet]
        public ActionResult Buy(int id)
        {
            return View(new BuyViewModel {BookId = id});
        }

        [HttpPost]
        public ActionResult Buy(Sale sale)
        {
            var model = new ConfirmBuyViewModel();
            sale.sale_date = DateTime.Now;

            try
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    var bk = dbContext.Books.First(book => book.book_id == sale.book_id);
                    sale.price = bk.price * sale.books_count;

                    model.BookName = bk.book_name;

                    if (bk.available_count < sale.books_count || sale.books_count < 1)
                    {
                        model.ErrorMessage = "Sorry, we haven't enough books. Please, try later.";
                        return View("ConfirmBuy", model);
                    }
                }
            }
            catch (SqlException e)
            {
                model.ErrorMessage = e.Message;
                return View("ConfirmBuy", model);
            }

            model.Sale = sale;
            model.ErrorMessage = "";
            return View("ConfirmBuy", model);
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
            var model = new EditBookViewModel();
            try
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    model.Book = dbContext.Books.First(b => b.book_id == id).Clone();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return View(model);
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
            var model = new EditAuthorViewModel();
            try
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    model.Author = dbContext.Authors.First(a => a.author_id == id).Clone();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return View(model);
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
            var model = new ConfirmDeleteBookViewModel {BookId = id};
            try
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    var book = dbContext.Books.First(b => b.book_id == id);
                    model.BookName = book.book_name;
                }
            }
            catch (SqlException e)
            {
                model.ErrorMessage = e.Message;
                return View("ConfirmDeleteBook", model);
            }
            
            return View("ConfirmDeleteBook", model);
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
            var model = new ConfirmDeleteAuthorViewModel {AuthorId = id};
            try
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    var author = dbContext.Authors.First(a => a.author_id == id);
                    model.AuthorName = author.first_name + " " + author.last_name;
                }
            }
            catch (SqlException e)
            {
                model.ErrorMessage = e.Message;
                return View("ConfirmDeleteAuthor", model);
            }
            
            return View("ConfirmDeleteAuthor", model);
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
            var model = new AddBookViewModel();
            try
            {
                using (var dbContext = new Bookshop_DBContext())
                {
                    dbContext.Authors.ForEach(author => model.Authors.Add(new SelectListItem
                    {
                        Text = author.first_name + " " + author.last_name,
                        Value = author.first_name + " " + author.last_name
                    }));
                }
            }
            catch (SqlException e)
            {
                model.ErrorMessage = e.Message;
                return View(model);
            }

            return View(model);
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