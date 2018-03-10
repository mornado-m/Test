CREATE DATABASE Bookshop_DB

USE Bookshop_DB
GO

CREATE TABLE Authors
(author_id INT IDENTITY(1, 1),
first_name NVARCHAR(50) NOT NULL,
last_name NVARCHAR(50) NOT NULL,
DOB DATE,
info NVARCHAR(MAX) NULL,
CONSTRAINT PK_AuthorsID PRIMARY KEY (author_id)
)
GO

CREATE TABLE Books
(book_id INT IDENTITY(1, 1) NOT NULL,
book_name NVARCHAR(256) NOT NULL,
author_id INT NOT NULL,
[description] NVARCHAR(MAX) NULL,
available_count INT NOT NULL,
price FLOAT NOT NULL,
CONSTRAINT PK_BooksID PRIMARY KEY (book_id),
CONSTRAINT FK_book_author FOREIGN KEY (author_id) REFERENCES Authors(author_id)
)
GO

CREATE TABLE Sales
(sale_id INT IDENTITY(1, 1) NOT NULL,
book_id INT NOT NULL,
sale_date DATE NOT NULL,
books_count INT NOT NULL,
price FLOAT NOT NULL,
CONSTRAINT PK_SalesID PRIMARY KEY (sale_id),
CONSTRAINT FK_sale_book FOREIGN KEY (book_id) REFERENCES Books(book_id)
)
GO