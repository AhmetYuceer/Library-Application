using System;
using System.Collections.Generic;
using System.IO; 
using Newtonsoft.Json;
using Kutuphane_Uygulamasi.Classes.Helpers;

namespace Kutuphane_Uygulamasi.Classes.Operations
{
    internal class BookOperations
    {
        Helper helper = new Helper();

        private string mainPath = AppDomain.CurrentDomain.BaseDirectory;
        private  string allBooks = "All Books.json";
        private  string borrowedBooks = "Borrowed Books.json";
        
        public string SetBookAttribute(string currentValue, string description)
        {
            string newValue = currentValue;
            Console.Clear();
            Console.Write(description);

            newValue = Console.ReadLine();

            Console.WriteLine(" 1 - Onayla \n 0 - İptal Et");

            while (true)
            {
                char key = Console.ReadKey().KeyChar;
                switch (key)
                {
                    case '1':
                        return newValue.Trim();
                    case '0':
                        return currentValue.Trim();
                    default:
                        Console.WriteLine(": Geçersiz Karakter");
                        break;
                }
 
            }
        }

        public (bool status, string message) ReturningTheBook(long borrowedBookISBN , long numberOfBookReturns)
        {
            try
            {
                List<BorrowedBook> borrowedBook = GetBorrowedBooksList();
                List<Book> books = GetBooksList();

                string borrowedBookPath = Path.Combine(mainPath, borrowedBooks);
                string booksPath = Path.Combine(mainPath, allBooks);

                foreach (var item in borrowedBook)
                {
                    if (item._ISBN == borrowedBookISBN)
                    {
                        item.numberOfBookCopies -= numberOfBookReturns;

                        if (item.numberOfBookCopies < 0)
                        {
                            item.numberOfBookCopies += numberOfBookReturns;
                            return (false, "Yeterli Kitap Bulunmuyor");
                        }
                      
                        foreach (var book in books)
                        {
                            if (book._ISBN == item._ISBN)
                            {
                                book.numberOfBookCopies += numberOfBookReturns;
                                book.numberOfCopiesOfBooksBorrowed -= numberOfBookReturns;
                                
                                if (item.numberOfBookCopies == 0)
                                {
                                    borrowedBook.Remove(item);
                                }
                                
                                string borrowedBookJson = JsonConvert.SerializeObject(borrowedBook, Formatting.Indented);
                                File.WriteAllText(borrowedBookPath, borrowedBookJson);

                                string bookJson = JsonConvert.SerializeObject(books, Formatting.Indented);
                                File.WriteAllText(booksPath, bookJson);
                                return (true, "Başarılı Bir Şekilde İade Edildi");
                            }
                        }
                    }
                }
                return (false, "ISBN Numarası Geçersiz");
            }
            catch (Exception)
            {
                return (false, "İade sırasında bir hata oluştu tekrar deneyiniz");
            }

         
        }

        #region is the book registration process
        public (bool status, string message) SaveTheBorrowedBook(BorrowedBook borrowedBook)
        {
            try
            {
                string borrowedBookPath = Path.Combine(mainPath, borrowedBooks);
                string booksPath = Path.Combine(mainPath, allBooks);

                List<Book> books = GetBooksList();

                foreach (var item in books)
                {
                    if (item._ISBN == borrowedBook._ISBN)
                    {
                        item.numberOfBookCopies -= borrowedBook.numberOfBookCopies;
                        if (item.numberOfBookCopies < 0)
                        {
                            item.numberOfBookCopies += borrowedBook.numberOfBookCopies;
                            return (false, "Yeterli Kitap Bulunmuyor");
                        }

                        item.numberOfCopiesOfBooksBorrowed += borrowedBook.numberOfBookCopies;

                        List<BorrowedBook> BorrowedBookList = GetBorrowedBooksList();
                        BorrowedBookList.Add(borrowedBook);

                        string borrowedBookJson = JsonConvert.SerializeObject(BorrowedBookList, Formatting.Indented);
                        File.WriteAllText(borrowedBookPath, borrowedBookJson);

                        string bookJson = JsonConvert.SerializeObject(books, Formatting.Indented);
                        File.WriteAllText(booksPath, bookJson);

                        return (true, "Kitap Ödünç Verildi");
                    }
                }
                return (false, " :Aradığınız kitap bulunamadı.");
            }
            catch (Exception)
            {
                return (false, " :Kayıt sırasında hata oluştu tekrar deneyiniz");
            }
        }
        public (bool status, string message) SaveTheBook(Book book)
        {
            try
            {
                string newPath = Path.Combine(mainPath, allBooks);
                List<Book> books = GetBooksList();
                var result = helper.BookAttributesControl(book, books);

                if (result.state)
                {
                    books.Add(book);
                    string bookJson = JsonConvert.SerializeObject(books, Formatting.Indented);
                    File.WriteAllText(newPath, bookJson);
                    return (true, result.message);
                }
                return (false, result.message);
            }
            catch (Exception)
            {
                return (false, " :Kayıt sırasında hata oluştu tekrar deneyiniz");
            }
        }
        #endregion

        #region Get Books
        public List<Book> GetBooksList()
        {
            if (!File.Exists(allBooks))
            {
                return new List<Book>();
            }
            string jsonFile = File.ReadAllText(allBooks);
            List<Book> books = JsonConvert.DeserializeObject<List<Book>>(jsonFile);

            return books;
        }
        public List<BorrowedBook> GetBorrowedBooksList()
        {
            if (!File.Exists(borrowedBooks))
            {
                return new List<BorrowedBook>();
            }
            string jsonFile = File.ReadAllText(borrowedBooks);
            List<BorrowedBook> books = JsonConvert.DeserializeObject<List<BorrowedBook>>(jsonFile);

            return books;
        }
        #endregion
        
        #region Search For Books

        public (bool status, List<Book> books) SearchForBooksByTitle(string bookTitle)
        {
            List<Book> books = GetBooksList();
            List<Book> result = new List<Book>();

            foreach (var book in books)
            {
                if (book.bookTitle == bookTitle)
                    result.Add(book);                               
            }

            if (result.Count <= 0)
                return (false, result);

            return (true, result);
        }

        public (bool state, List<Book> books) SearchForBooksByAuthor(string bookAuthorName)
        {
            List<Book> books = GetBooksList();
            List<Book> result = new List<Book>();

            foreach (var book in books)
            {
                if (book.bookAuthor == bookAuthorName)
                    result.Add(book);
            }

            if (result.Count <= 0)
                return (false, result);

            return (true, result);
        }  

        public (bool status, List<Book> books) SearchForBooksByISBN(long ISBN)
        {
            List<Book> books = GetBooksList();
            List<Book> result = new List<Book>();

            foreach (var book in books)
            {
                if (book._ISBN == ISBN)
                    result.Add(book);
            }

            if (result.Count <= 0)
                return (false, result);

            return (true, result);
        }
        #endregion
    }
}