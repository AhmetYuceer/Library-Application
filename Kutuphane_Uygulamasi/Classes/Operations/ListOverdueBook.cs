using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kutuphane_Uygulamasi.Classes.Operations
{
    internal class ListOverdueBook
    {

        public static void ListOverdueBooks()
        {
            MenuOperations.Header();
            Console.WriteLine("\n  --- |Kitap Listesi| --- \n");

            List<BorrowedBook> borrowedBooks = MenuOperations.bookOperations.GetBorrowedBooksList();

            foreach (var item in borrowedBooks)
            {
                if (DateTime.Now > item.DateOfDeliveryOfTheBook)
                {
                    Console.WriteLine("  ----------------------------");
                    Console.WriteLine("  Başlık : " + item.bookTitle);
                    Console.WriteLine("  Yazar : " + item.bookAuthor);
                    Console.WriteLine("  ISBN : " + item._ISBN);
                    Console.WriteLine("  Mevcut kopya sayısı : " + item.numberOfBookCopies);
                }
            }
            MenuOperations.Footer();

            char key = Console.ReadKey().KeyChar;

            switch (key)
            {
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
                    PageControl.GetAllBooks();
                    break;
            }
        }

    }
}
