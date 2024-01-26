using System.Dynamic;

class Book
{
    public string bookTitle { get; set; }
    public string bookAuthor { get; set; }
    public long _ISBN { get; set; }
    public long numberOfBookCopies { get; set; }

    public long numberOfCopiesOfBooksBorrowed { get; set; }
}