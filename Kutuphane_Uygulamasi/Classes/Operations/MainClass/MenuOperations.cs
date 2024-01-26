using Kutuphane_Uygulamasi.Classes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kutuphane_Uygulamasi.Classes.Operations
{
    internal class MenuOperations
    {
        public static readonly Helper helper = new Helper();
        public static readonly BookOperations bookOperations = new BookOperations();

        public static void Header()
        {
            Console.Clear();
            Console.WriteLine("\n -------------------------");
            Console.WriteLine("  Kütüphane Uygulaması ");
            Console.WriteLine(" -------------------------");
        }
        public static void Footer()
        {
            Console.WriteLine("\n ------------------------- \n");
            Console.WriteLine("  z - Geri");
            Console.WriteLine("  x - Programı Kapat");
            Console.WriteLine(" -------------------------");
        }
        public static void MainMenu()
        {
            Header();
            Console.WriteLine("  1 - Yeni Kitap Ekle ");
            Console.WriteLine("  2 - Tüm Kitapları Listele ");
            Console.WriteLine("  3 - Kitap Ara ");
            Console.WriteLine("  4 - Kitap Ödünç Ver ");
            Console.WriteLine("  5 - Kitap İade Edin ");
            Console.WriteLine("  6 - Süresi Geçmiş Kitapları Listele ");
            Footer();

            char key = Console.ReadKey().KeyChar;

            switch (key)
            {
                case '1': AddBooks.AddNewBook(); break;
                case '2': PageControl.GetAllBooks(); break;
                case '3': SearchForBook.SearchForBooks(); break;
                case '4': LendTheBook.LendTheBooks(); break;
                case '5': ReturningTheBook.ReturningTheBooks(); break;
                case '6': ListOverdueBook.ListOverdueBooks(); break;

                case 'z':
                    MainMenu();
                    break;
                case 'x':
                    Console.Clear();
                    Console.WriteLine("Kapatılıyor ...");
                    Thread.Sleep(500);
                    break;

                default:
                    Console.WriteLine(" : Geçersiz Karakter");
                    Thread.Sleep(300);
                    MainMenu();
                    break;
            }
        }
    }
}