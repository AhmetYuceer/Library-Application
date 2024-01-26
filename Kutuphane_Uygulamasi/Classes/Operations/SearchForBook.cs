using System;
using System.Collections.Generic;
using System.Threading;

namespace Kutuphane_Uygulamasi.Classes.Operations
{
    internal class SearchForBook
    {

        public static void SearchForBooks()
        {
            MenuOperations.Header();
            Console.WriteLine("\n  --- |Kitap Ara| --- \n");
            Console.WriteLine("  1 - Kitap Başlığına Göre Ara ");
            Console.WriteLine("  2 - Kitap Yazarına Göre Ara ");
            Console.WriteLine("  3 - ISBN Numarasına Göre Ara");
            MenuOperations.Footer();

            char key = Console.ReadKey().KeyChar;

            switch (key)
            {
                case '1': SearchForBooksByTitle(); break;
                case '2': SearchForBooksByAuthor(); break;
                case '3': SearchForBooksByISBN(); break;
                case 'z': MenuOperations.MainMenu(); break;

                case 'x':
                    Console.Clear();
                    Console.WriteLine("Kapatılıyor ...");
                    Thread.Sleep(500);
                    break;
                default:
                    Console.WriteLine(" : Geçersiz Karakter");
                    Thread.Sleep(300);
                    SearchForBooks();
                    break;
            }
        }

        private static void SearchForBooksByTitle()
        {
            Console.Clear();
            Console.Write("Kitap Başlığını Giriniz : ");
            string bookTitle = Console.ReadLine();

            var resultTitle = MenuOperations.bookOperations.SearchForBooksByTitle(bookTitle);

            if (resultTitle.status)
            {
                foreach (var book in resultTitle.books)
                {
                    MenuOperations.Header();
                    Console.WriteLine(" ----------------------------");
                    Console.WriteLine("  Başlık : " + book.bookTitle);
                    Console.WriteLine("  Yazar : " + book.bookAuthor);
                    Console.WriteLine("  ISBN : " + book._ISBN);
                    Console.WriteLine("  Mevcut kopya sayısı : " + book.numberOfBookCopies);
                    Console.WriteLine("  Ödünç verilen kopya sayısı : " + book.numberOfCopiesOfBooksBorrowed);
                }
                Console.WriteLine(" ----------------------------");
                Console.ReadKey();
                SearchForBooks();
            }
            else
            {
                Console.WriteLine(" Kayıt Bulunamadı.");
                Thread.Sleep(1000);
                SearchForBooks();
            }
        }
        private static void SearchForBooksByAuthor()
        {
            Console.Clear();
            Console.Write("Yazar Adını Giriniz : ");
            string authorName = Console.ReadLine();

            var resultAuthor = MenuOperations.bookOperations.SearchForBooksByAuthor(authorName);

            if (resultAuthor.state)
            {
                foreach (var book in resultAuthor.books)
                {
                    MenuOperations.Header();
                    Console.WriteLine(" ----------------------------");
                    Console.WriteLine("  Başlık : " + book.bookTitle);
                    Console.WriteLine("  Yazar : " + book.bookAuthor);
                    Console.WriteLine("  ISBN : " + book._ISBN);
                    Console.WriteLine("  Mevcut kopya sayısı : " + book.numberOfBookCopies);
                    Console.WriteLine("  Ödünç verilen kopya sayısı : " + book.numberOfCopiesOfBooksBorrowed);
                }
                Console.WriteLine(" ----------------------------");
                Console.ReadKey();
                SearchForBooks();
            }
            else
            {
                Console.WriteLine(" Kayıt Bulunamadı.");
                Thread.Sleep(1000);
                SearchForBooks();
            }
        }

        private static void SearchForBooksByISBN()
        {
            Console.Clear();
            Console.Write("ISBN Bilgisini Giriniz : ");
            string _ISBN = Console.ReadLine();

            var result = MenuOperations.helper.ConvertTextToNumber(_ISBN);

            if (result.state)
            {
                var resultISBN = MenuOperations.bookOperations.SearchForBooksByISBN(result.result);

                if (result.state)
                {
                    foreach (var book in resultISBN.books)
                    {
                        MenuOperations.Header();
                        Console.WriteLine(" ----------------------------");
                        Console.WriteLine("  Başlık : " + book.bookTitle);
                        Console.WriteLine("  Yazar : " + book.bookAuthor);
                        Console.WriteLine("  ISBN : " + book._ISBN);
                        Console.WriteLine("  Mevcut kopya sayısı : " + book.numberOfBookCopies);
                        Console.WriteLine("  Ödünç verilen kopya sayısı : " + book.numberOfCopiesOfBooksBorrowed);
                    }
                    Console.WriteLine(" ----------------------------");
                    Console.ReadKey();
                    SearchForBooks();
                }
                else
                {
                    Console.WriteLine(" Kayıt Bulunamadı.");
                    Thread.Sleep(1000);
                    SearchForBooks();
                }
            }
            else
            {
                Console.WriteLine(" : Lütfen sayısal değer giriniz");
                Thread.Sleep(1000);
                SearchForBooks();
            }
        }
    }
}