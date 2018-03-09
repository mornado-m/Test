using System;
using System.Collections.Generic;
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
            using (var dbContext = new Bookshop_DBContext())
            {
                dbContext.Books.ForEach(book => books.Add(new Book{
                    Author = new Author{ first_name = book.Author.first_name, last_name = book.Author.last_name },
                    book_id = book.book_id,
                    book_name = book.book_name,
                    available_count = book.available_count,
                    price = book.price
                }));
            }

            ViewBag.Books = books;
            return View();
        }

        public ActionResult Sales()
        {
            var sales = new List<Sale>();
            using (var dbContext = new Bookshop_DBContext())
            {
                dbContext.Sales.ForEach(sale => sales.Add(new Sale
                {
                    Book = new Book { book_name = sale.Book.book_name, Author = new Author
                    {
                        first_name = sale.Book.Author.first_name,
                        last_name = sale.Book.Author.last_name
                    }},
                    sale_date = sale.sale_date,
                    books_count = sale.books_count,
                    price = sale.price
                }));
            }

            ViewBag.Sales = sales;
            return View();
        }

        public ActionResult Authors()
        {
            var authors = new List<Author>();
            using (var dbContext = new Bookshop_DBContext())
            {
                dbContext.Authors.ForEach(author => authors.Add(new Author
                {
                    first_name = author.first_name,
                    last_name = author.last_name,
                    DOB = author.DOB,
                    info = author.info
                }));
            }

            ViewBag.Authors = authors;
            return View();
        }
    }
}