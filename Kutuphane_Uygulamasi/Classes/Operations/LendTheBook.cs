using Kutuphane_Uygulamasi.Classes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kutuphane_Uygulamasi.Classes.Operations
{
    internal class LendTheBook
    {
        public static void LendTheBooks()
        {
            MenuOperations.Header();
            Console.WriteLine("\n  Ödünç vermek istediğiniz kitabın ISBN numarasını giriniz ");

            Console.Write("  ISBN Numarası Giriniz : ");
            string _ISBN = Console.ReadLine();

            var convertedISBN = MenuOperations.helper.ConvertTextToNumber(_ISBN);

            if (convertedISBN.state)
            {
                var result = MenuOperations.bookOperations.SearchForBooksByISBN(convertedISBN.result);

                if (result.status)
                {
                    List<Book> books = result.books;
                    Console.Write("  Kaç tane kitap ödünç vermek istiyorusunuz : ");

                    string bookCount = Console.ReadLine();
                    var convertedBookCount = MenuOperations.helper.ConvertTextToNumber(bookCount);

                    if (convertedBookCount.state)
                    {
                        Console.Write("  Teslim Edilmesi Gereken Tarihi ('GÜN/AY/YIL' => '25/01/2024') şeklinde giriniz : ");
                        string lastDate = Console.ReadLine();

                        var resultDate = MenuOperations.helper.ConvertTextToDate(lastDate);

                        if (resultDate.state)
                        {
                            BorrowedBook borrowedBook = new BorrowedBook
                            {
                                bookTitle = books[0].bookTitle,
                                bookAuthor = books[0].bookAuthor,
                                _ISBN = books[0]._ISBN,
                                numberOfBookCopies = convertedBookCount.result,
                                DateTheBookWasLent = DateTime.Now,
                                DateOfDeliveryOfTheBook = resultDate.result,
                            };

                            List<BorrowedBook> borrowedBooks = new List<BorrowedBook>();
                            borrowedBooks.Add(borrowedBook);

                            Console.WriteLine("  -------------------------");
                            foreach (var item in borrowedBooks)
                            {
                                Console.WriteLine("  Kitap Başlığı : " + item.bookTitle);
                                Console.WriteLine("  Kitap Yazarı : " + item.bookAuthor);
                                Console.WriteLine("  ISBN : " + item._ISBN);
                                Console.WriteLine("  Ödünç Verilme Tarihi : " + item.DateTheBookWasLent);
                                Console.WriteLine("  Teslim Etme Tarihi : " + resultDate.result);
                                Console.WriteLine("  SAYI : " + item.numberOfBookCopies);
                            }
                            Console.WriteLine("  -------------------------");

                            Console.WriteLine("  Yukarıdaki Bilgileri Onaylıyor Musunuz ?");
                            Console.WriteLine("  1- Onayla  2- İptal");

                            char key = Console.ReadKey().KeyChar;
                            switch (key)
                            {
                                case '1':
                                    var resultSave = MenuOperations.bookOperations.SaveTheBorrowedBook(borrowedBook);
                                    Console.WriteLine(resultSave.message);
                                    Thread.Sleep(1000);
                                    MenuOperations.MainMenu();
                                    break;
                                case '2':
                                    MenuOperations.MainMenu();
                                    break;

                                default:
                                    Console.WriteLine(" : Geçersiz Karakter");
                                    Thread.Sleep(300);
                                    LendTheBooks();
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine(" : Hatalı Tarih girişi");
                            Thread.Sleep(1000);
                            LendTheBooks();
                        }
                    }
                    else
                    {
                        Console.WriteLine("  :Lütfen Sayı giriniz");
                        LendTheBooks();
                    }
                }
                else
                {
                    Console.WriteLine(" : Bu ISBN numarasına ait kitap bulunmuyor");
                    Thread.Sleep(1000);
                    MenuOperations.MainMenu();
                }
            }
            else
            {
                Console.WriteLine(" : Hatalı ISBN girişi");
                Thread.Sleep(1000);
                MenuOperations.MainMenu();
            }
        }
    }
}
