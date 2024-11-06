

namespace LibraryAssignmentOJA
{
    public class Author
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }

        public Author(string name, DateTime birthDate, string country)
        {
            Name = name;
            BirthDate = birthDate;
            Country = country;
        }
    }
}
