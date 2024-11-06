namespace LibraryAssignmentOJA
{
    using System;

    public class LibraryUI
    {
        private readonly LibraryManager _libraryManager;

        public LibraryUI(LibraryManager libraryManager)
        {
            _libraryManager = libraryManager;
        }

        public void Start()
        {
            while (true)
            {
                ShowMenu();
                var choice = Console.ReadLine();
                HandleUserChoice(choice!);
            }
        }
        private void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Välkommen till bibliotekshanteraren!");
            Console.WriteLine("1. Visa alla böcker");
            Console.WriteLine("2. Lägg till bok");
            Console.WriteLine("3. Uppdatera bok");
            Console.WriteLine("4. Ta bort bok");
            Console.WriteLine("5. Sök böcker");
            Console.WriteLine("6. Sortera böcker efter år");
            Console.WriteLine("7. Sätt betyg på bok");
            Console.WriteLine("0. Avsluta");
            Console.Write("Välj ett alternativ: ");
        }
        private void HandleUserChoice(string choice)
        {
            switch (choice)
            {
                case "1": ShowBooks(); break;
                case "2": AddBook(); break;
                case "3": UpdateBook(); break;
                case "4": RemoveBook(); break;
                case "5": SearchBooks(); break;
                case "6": SortBooks(); break;
                case "7": SetBookRating(); break;
                case "0": Environment.Exit(0); break;
                default:
                        Console.WriteLine("Ogiltigt val, försök igen. \nTryck Enter för att komma tillbaks till menyn!"); 
                    break;
            }
        }
        private void ShowBooks()
        {
            var books = _libraryManager.GetAllBooks();
            if (books.Count == 0) Console.WriteLine("Inga böcker finns.\nTryck Enter för att komma tillbaks till menyn!");
            else 
            books.ForEach(b => Console.WriteLine($"\nTitel: {b.Title}, \nFörfattare: {b.Author.Name}, \nGenre: {b.Genre}, \nPubliceringsår: {b.Year}, \nBetyg: {(b.Rating.HasValue ? b.Rating.Value.ToString("F1") : "Ej betygsatt")}" +
                $"\n\nTryck på ENTER för att komma tillbaks till menyn!"));
            Console.ReadKey();
        }
        private void AddBook()
        {
            Console.Write("Titel: ");
            var title = Console.ReadLine();
            Console.Write("Författare: ");
            var authorName = Console.ReadLine();
            Console.Write("Författarens födelsedatum (yyyy-mm-dd): ");
            DateTime birthDate = DateTime.Parse(Console.ReadLine()!);
            Console.Write("Ange förtattarens Land:");
            var country = Console.ReadLine();
            var author = new Author(authorName!, birthDate, country!);

            Console.Write("Genre: ");
            var genre = Console.ReadLine();
            Console.Write("År: ");
            var year = int.Parse(Console.ReadLine()!);

            var book = new Book(title!, author, genre!, year);
            _libraryManager.AddBook(book);
            Console.WriteLine("Boken har lagts till!\nTryck Enter för att komma tillbaks till menyn!");
            Console.ReadKey();
        }
        private void UpdateBook()
        {
            Console.Write("Ange titel på bok som ska uppdateras: ");
            var title = Console.ReadLine();

            var existingBook = _libraryManager.GetAllBooks().SingleOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (existingBook == null)
            {
                Console.WriteLine("Boken hittades inte.\nTryck Enter för att komma tillbaks till menyn!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Nuvarande titel: {existingBook.Title}");
            Console.Write("Ny titel (lämna tomt för att behålla titel): ");
            var newTitle = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newTitle)) existingBook.Title = newTitle;

            Console.WriteLine($"Nuvarande författare: {existingBook.Author.Name}");
            Console.Write("Ny författare (lämna tomt för att behålla författare): ");
            var newAuthorName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newAuthorName)) existingBook.Author.Name = newAuthorName;

            Console.WriteLine($"Nuvarande födelsedatum: {existingBook.Author.BirthDate:yyyy-MM-dd}");
            Console.Write("Ny födelsedatum (yyyy-mm-dd, lämna tomt för att behålla födelsedatum): ");
            var newBirthDateInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newBirthDateInput) && DateTime.TryParse(newBirthDateInput, out var newBirthDate))
            {
                existingBook.Author.BirthDate = newBirthDate;
            }

            Console.WriteLine($"Nuvarande genre: {existingBook.Genre}");
            Console.Write("Ny genre (lämna tomt för att behålla genre): ");
            var newGenre = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newGenre)) existingBook.Genre = newGenre;

            Console.WriteLine($"Nuvarande år: {existingBook.Year}");
            Console.Write("Ny år (lämna tomt för att behålla publiceringsår): ");
            var newYearInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newYearInput) && int.TryParse(newYearInput, out var newYear))
            {
                existingBook.Year = newYear;
            }

            _libraryManager.UpdateBook(existingBook.Title, existingBook);
            Console.WriteLine("Boken har uppdaterats!\nTryck Enter för att komma tillbaks till menyn!");
            Console.ReadKey();
        }


        private void RemoveBook()
        {
            Console.Write("Ange titel på bok som ska tas bort: ");
            var title = Console.ReadLine();
            _libraryManager.RemoveBook(title!);
            Console.WriteLine("Boken har tagits bort!\nTryck Enter för att komma tillbaks till menyn!");
            Console.ReadKey();
        }
        private void SearchBooks()
        {
            Console.Write("Sökterm: ");
            var searchTerm = Console.ReadLine();
            var books = _libraryManager.SearchBooks(searchTerm!);
            if (books.Count == 0) Console.WriteLine("Inga böcker hittades.\nTryck Enter för att komma tillbaks till menyn!");
            else books.ForEach(b => Console.WriteLine($"Titel: {b.Title}, Författare: {b.Author.Name}, Genre: {b.Genre}, År: {b.Year}"));
            Console.ReadKey();
        }
        private void SortBooks()
        {
            var books = _libraryManager.SortBooksByYear();
            books.ForEach(b => Console.WriteLine($"Titel: {b.Title}, Författare: {b.Author.Name}, Genre: {b.Genre}, År: {b.Year}"));
            Console.ReadKey();
        }
        private void SetBookRating()
        {
            Console.Write("Ange titel på bok som ska betygsättas: ");
            var title = Console.ReadLine();

            Console.Write("Ange betyg (1-5): ");
            if (double.TryParse(Console.ReadLine(), out double rating) && rating >= 1 && rating <= 5)
            {
                _libraryManager.SetBookRating(title!, rating);
            }
            else
            {
                Console.WriteLine("Ogiltigt betyg. Betyget måste vara mellan 1 och 5.");
            }
            Console.ReadKey();
        }
    }
}
