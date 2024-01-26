using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Kutuphane_Uygulamasi.Classes.Helpers
{
    internal class Helper
    {

        public (bool state, DateTime result) ConvertTextToDate(string lastDate)
        {
            try
            {
                string[] dates = lastDate.Split('/'); //  DD/MM/YYYY
                
                if (dates.Length == 3)
                {
                    int day = Convert.ToInt32(dates[0]);
                    int month = Convert.ToInt32(dates[1]);
                    int year = Convert.ToInt32(dates[2]);
                    DateTime date = new DateTime(year,month,day,DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    return (true, date);
                }
                return (false, DateTime.Now);
            }
            catch (Exception)
            {
                return (false, DateTime.Now);
            }
        }

        public (bool state, long result) ConvertTextToNumber(string value)
        {
            try
            {
                long result = long.Parse(value);
                if (result <= 0)
                {
                    return (false, -1);
                }
                return (true,result);
            }
            catch (Exception)
            {
                return (false,-1);
            }   
        }

        public (bool state, string message) BookAttributesControl(Book book ,List<Book> books)
        {
      
            if (book.bookTitle != "")
            {
                if (book.bookAuthor != "")
                {
                    if (book._ISBN > 0)
                    {
                    
                        foreach (var item in books)
                        {
                            if (item._ISBN == book._ISBN)
                            {
                                return (false, " :Bu kitaba ait ISBN numarası zaten kayıtlı");
                            }
                        }

                        if (book.numberOfBookCopies > 0)
                        {
                            return (true, " :Kayıt Başarılı");
                        }
                        else
                        {
                            return (false, " :Kopya sayısı 0 dan büyük olmalıdır.");
                        }
                    }
                    else
                    {
                        return (false, " :ISBN numarası 0 dan büyük olmalıdır.");
                    }
                }
                else
                {
                    return (false, " :Yazar adı boş olamaz");
                }
            }
            else
            {
                return (false, " :Kitap başlığı boş olamaz");
            }
        }

        






    }
}