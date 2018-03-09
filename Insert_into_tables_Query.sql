INSERT INTO Authors (first_name, last_name, DOB, info) VALUES
('Sir Arthur', 'Conan Doyle', '1859-5-22', 'A British writer best known for his detective fiction featuring the character Sherlock Holmes.'),
('John', 'Tolkien', '1892-1-3', 'An English writer, poet, philologist, and university professor who is best known as the author of the classic high-fantasy works The Hobbit, The Lord of the Rings, and The Silmarillion.'),
('Andrzej', 'Sapkowski', '1948-6-21', 'A Polish fantasy writer. He is best known for his book series, The Witcher. His books have been translated so far into nineteen languages.')
GO

INSERT INTO Books(book_name, author_id, decription, available_count, price) VALUES
('A Study in Scarlet', 1, 'It is an 1887 detective novel by British author Arthur Conan Doyle. Written in 1886, the story marks the first appearance of Sherlock Holmes and Dr. Watson, who would become two of the most famous characters in popular fiction. The book`s title derives from a speech given by Holmes, an amateur detective, to his friend and chronicler Watson on the nature of his work', 10, 20.5),
('The Sign of the Four', 1, 'It is the second novel featuring Sherlock Holmes written by Sir Arthur Conan Doyle.', 5, 18.25),
('The Fellowship of the Ring', 2, 'It is the first of three volumes of the epic novel The Lord of the Rings by the English author J. R. R. Tolkien. It is followed by The Two Towers and The Return of the King. It takes place in the fictional universe of Middle-earth.', 34, 36),
('The Two Towers', 2, 'It is the second volume of J. R. R. Tolkien`s high fantasy novel The Lord of the Rings. It is preceded by The Fellowship of the Ring and followed by The Return of the King.', 20, 33.7),
('The Return of the King', 2, 'It is the third and final volume of J. R. R. Tolkien`s The Lord of the Rings, following The Fellowship of the Ring and The Two Towers. The story begins in the kingdom of Gondor, which is soon to be attacked by the Dark Lord Sauron.', 0, 35),
('The Lady of the Lake', 3, 'It is the fifth novel in the Witcher Saga written by Polish fantasy writer Andrzej Sapkowski, first published in Poland in 1999. It is a sequel to the fourth Witcher novel The Swallow`s Tower.', 7, 10.6)
GO

INSERT INTO Sales (book_id, sale_date, books_count, price) VALUES
(5, '2018-01-15', 2, 70),
(2, '2018-01-23', 1, 18.25),
(3, '2018-02-10', 1, 36),
(4, '2018-02-10', 1, 33.7),
(5, '2018-02-10', 1, 35),
(6, '2018-02-23', 10, 106)
GO