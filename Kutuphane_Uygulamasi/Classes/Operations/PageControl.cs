using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kutuphane_Uygulamasi.Classes.Operations
{
    internal class PageControl
    {
        // -Page Control- \\
        private static int startIndex = 0, maxIndex = 10, amount = 10;
        private static bool nextPage = false, previousPage = false;

        public static void GetAllBooks()
        {
            MenuOperations.Header();
            Console.WriteLine("\n  --- |Kitap Listesi| --- \n");

            List<Book> books = MenuOperations.bookOperations.GetBooksList();

            WriteBooksOnTheConsole(books);

            MenuOperations.Footer();

            char key = Console.ReadKey().KeyChar;
            switch (key)
            {
                case '1': NextPage(books); break;
                case '2': PreviousPage(books); break;
                case 'z':
                    startIndex = 0;
                    maxIndex = amount;
                    MenuOperations.MainMenu();
                    break;
                case 'x':
                    Console.Clear();
                    Console.WriteLine("Kapatılıyor ...");
                    Thread.Sleep(500);
                    break;

                default:
                    Console.WriteLine(" : Geçersiz Karakter");
                    Thread.Sleep(300);
                    GetAllBooks();
                    break;
            }
        }

        private static void WriteBooksOnTheConsole(List<Book> books)
        {
            Console.WriteLine("\n  Toplam Kayıt Sayısı : " + books.Count);
            while ((startIndex < books.Count) && (startIndex < maxIndex))
            {
                Console.WriteLine("  ----------------------------");
                Console.WriteLine($" ({startIndex + 1}) ");
                Console.WriteLine("  Başlık : " + books[startIndex].bookTitle);
                Console.WriteLine("  Yazar : " + books[startIndex].bookAuthor);
                Console.WriteLine("  ISBN : " + books[startIndex]._ISBN);
                Console.WriteLine("  Mevcut kopya sayısı : " + books[startIndex].numberOfBookCopies);
                Console.WriteLine("  Ödünç verilen kopya sayısı : " + books[startIndex].numberOfCopiesOfBooksBorrowed);
                startIndex++;
            }

            if (books.Count % startIndex != 0 || books.Count > startIndex + amount)
            {
                nextPage = true;
                Console.WriteLine("  ----------------------------");
                Console.Write("\n  1- Sonraki Sayfa  ");
            }
            else
            {
                nextPage = false;
                Console.WriteLine("  ----------------------------\n");

            }

            if (startIndex - amount > 0)
            {
                previousPage = true;
                Console.WriteLine("  2- Önceki Sayfa");
            }
            else
            {
                previousPage = false;
            }
        }

        private static void PreviousPage(List<Book> books)
        {
            if (previousPage)
            {
                if (startIndex % amount != 0)
                {
                    maxIndex -= amount;
                    startIndex -= (books.Count - maxIndex) + amount;
                    GetAllBooks();
                    return;
                }
                startIndex -= amount * 2;
                maxIndex -= amount;
            }
            else
            {
                startIndex = 0;
            }
            GetAllBooks();
        }

        private static void NextPage(List<Book> books)
        {
            if (nextPage)
                maxIndex += amount;
            else
            {
                startIndex -= (books.Count - maxIndex) + amount;
            }
            GetAllBooks();
        }
    }
}