
namespace LibraryAssignmentOJA
{
    public class Book
    {
        public string Title { get; set; }
        public Author Author { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public double? Rating { get; set; }

        public Book(string title, Author author, string genre, int year)
        {
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            Rating = null;
        }
        public void Update(Book updatedBook)
        {
            Title = updatedBook.Title;
            Author = updatedBook.Author;
            Genre = updatedBook.Genre;
            Year = updatedBook.Year;
            Rating = updatedBook.Rating;
        }

        public void SetRating(double rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentOutOfRangeException("Betyget måste vara mellan 1 och 5.");

            Rating = rating;
        }
    }

}
