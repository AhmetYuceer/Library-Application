using Kutuphane_Uygulamasi.Classes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kutuphane_Uygulamasi.Classes.Operations
{
    internal class AddBooks
    {
        // -Book Attributes- \\
        private static string title = "", bookAuthor = "";
        private static long ISBN = 0, numberOfBookCopies = 0;

        public static void AddNewBook()
        {
            MenuOperations.Header();
            Console.WriteLine("\n  --- |Kitap Ekle| --- \n");
            Console.WriteLine("\n ---Kitap Bilgileri-------\n");
            Console.WriteLine("  Kitap Başlığı : " + title.Trim());
            Console.WriteLine("  Yazar : " + bookAuthor.Trim());
            Console.WriteLine("  ISBN : " + ISBN.ToString().Trim());
            Console.WriteLine("  Kopya Sayısı : " + numberOfBookCopies.ToString().Trim());

            Console.WriteLine("\n -------------------------");
            Console.WriteLine("  1 - Kitap Başlığı Gir");
            Console.WriteLine("  2 - Yazar Adı Gir ");
            Console.WriteLine("  3 - ISBN Numarası Gir ");
            Console.WriteLine("  4 - Mevcut Kopya Sayısı Gir ");
            Console.WriteLine("  5 - Kitap Bilgileri Kayıt Et ");
            MenuOperations.Footer();

            char key = Console.ReadKey().KeyChar;

            switch (key)
            {
                case '1':
                    title = MenuOperations.bookOperations.SetBookAttribute(title, "Kitap Başlığını Giriniz : ");
                    AddNewBook();
                    break;
                case '2':
                    bookAuthor = MenuOperations.bookOperations.SetBookAttribute(bookAuthor, "Kitap Yazarının Adını Giriniz : ");
                    AddNewBook();
                    break;
                case '3':
                    string _ISBN = MenuOperations.bookOperations.SetBookAttribute(ISBN.ToString().Trim(), "ISBN Numarası Giriniz : ");
                    SetISBN(_ISBN);
                    break;

                case '4':
                    string _numberOfBookCopies = MenuOperations.bookOperations.SetBookAttribute(numberOfBookCopies.ToString().Trim(), " Mevcut Kopya Sayısı Giriniz ");
                    SetNumberOfBookCopies(_numberOfBookCopies);
                    break;
                case '5':
                    SaveTheBook();
                    break;
                case 'z':
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
                    AddNewBook();
                    break;
            }
        }


        #region SaveTheBook
        private static void SaveTheBook()
        {
            Book book = new Book
            {
                bookTitle = title.Trim(),
                bookAuthor = bookAuthor.Trim(),
                _ISBN = ISBN,
                numberOfBookCopies = numberOfBookCopies,
            };
            var result = MenuOperations.bookOperations.SaveTheBook(book);
            if (result.status)
            {
                title = "";
                bookAuthor = "";
                ISBN = 0;
                numberOfBookCopies = 0;
            }
            Console.WriteLine(result.message);
            Thread.Sleep(1000);
            AddNewBook();
        }

        private static void SetNumberOfBookCopies(string _numberOfBookCopies)
        {

            var result_Number = MenuOperations.helper.ConvertTextToNumber(_numberOfBookCopies);
            if (result_Number.state)
            {
                numberOfBookCopies = result_Number.result;
                AddNewBook();
                return;
            }
            Console.WriteLine(" : Lütfen sayısal değer giriniz");
            Thread.Sleep(500);
            AddNewBook();
        }

        private static void SetISBN(string _ISBN)
        {
            var result_ISBN = MenuOperations.helper.ConvertTextToNumber(_ISBN);
            if (result_ISBN.state)
            {
                ISBN = result_ISBN.result;
                AddNewBook();
                return;
            }
            Console.WriteLine(" : Lütfen sayısal değer giriniz");
            Thread.Sleep(500);
            AddNewBook();
        }
        #endregion

    }
}
