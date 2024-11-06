using System.Text.Json;

namespace LibraryAssignmentOJA
{
    public class LibraryManager
    {
        private const string FilePath = "library.json";
        private List<Book> _books;

        public LibraryManager()
        {
            _books = LoadBooks();
        }

        public List<Book> GetAllBooks()
        {
            return _books;
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
            SaveBooks();
        }

        public void RemoveBook(string title)
        {
            var bookToRemove = _books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (bookToRemove != null)
            {
                _books.Remove(bookToRemove);
                SaveBooks();
            }
        }

        public void UpdateBook(string title, Book updatedBook)
        {
            var bookToUpdate = _books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (bookToUpdate != null)
            {
                bookToUpdate.Title = updatedBook.Title;
                bookToUpdate.Author = updatedBook.Author;
                bookToUpdate.Genre = updatedBook.Genre;
                bookToUpdate.Year = updatedBook.Year;
                SaveBooks();
            }
        }

        public List<Book> SearchBooks(string searchTerm)
        {
            return _books.Where(b => b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                      b.Author.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                         .ToList();
        }

        public List<Book> SortBooksByYear()
        {
            return _books.OrderBy(b => b.Year).ToList();
        }

        private void SaveBooks()
        {
            var json = JsonSerializer.Serialize(_books);
            File.WriteAllText(FilePath, json);
        }

        private List<Book> LoadBooks()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
            }
            return new List<Book>();
        }
        public void SetBookRating(string title, double rating)
        {
            var book = _books.SingleOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (book != null)
            {
                if (rating < 1 || rating > 5)
                {
                    Console.WriteLine("Betyget måste vara mellan 1 och 5.");
                    return;
                }
                book.SetRating(rating);
                SaveBooks();
                Console.WriteLine("Betyget har uppdaterats!");
            }
            else
            {
                Console.WriteLine("Boken med den angivna titeln kunde inte hittas.");
            }
        }

    }
}
