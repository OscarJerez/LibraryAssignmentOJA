using System;
namespace LibraryAssignmentOJA
{
    class Program
    {
        static void Main(string[] args)
        {
            var libraryManager = new LibraryManager();
            var userInteraction = new LibraryUI(libraryManager);
            userInteraction.Start();
        }
    }
}
