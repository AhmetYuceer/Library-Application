using Kutuphane_Uygulamasi.Classes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kutuphane_Uygulamasi.Classes.Operations
{
    internal class ReturningTheBook
    {
        public static void ReturningTheBooks()
        {
            MenuOperations.Header();
            Console.WriteLine("\n  İade etmek istediğiniz kitabın ISBN numarasını giriniz ");
            Console.Write("  ISBN Numarası Giriniz : ");

            string _ısbn = Console.ReadLine();
            var convertedISBN = MenuOperations.helper.ConvertTextToNumber(_ısbn);

            if (convertedISBN.state)
            {
                Console.Write("  Kaç tane kitap iade etmek istiyorusunuz : ");

                string bookCount = Console.ReadLine();
                var convertedBookCount = MenuOperations.helper.ConvertTextToNumber(bookCount);

                if (convertedBookCount.state)
                {
                    var result = MenuOperations.bookOperations.ReturningTheBook(convertedISBN.result, convertedBookCount.result);

                    Console.WriteLine(result.message);
                    Thread.Sleep(1000);
                    MenuOperations.MainMenu();
                }
                else
                {
                    Console.WriteLine("  :Lütfen Sayı giriniz");
                    Thread.Sleep(1000);
                    ReturningTheBooks();
                }
            }
            else
            {
                Console.WriteLine(" : Bu ISBN numarasına ait kitap bulunmuyor");
                Thread.Sleep(1000);
                MenuOperations.MainMenu();
            }
        }
    }
}