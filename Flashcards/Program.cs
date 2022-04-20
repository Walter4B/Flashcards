using Flashcards;
using System.Configuration;

class FlashcardsMain
{
    public static void Main()
    {
        FlashcardsController flashcardsController = new FlashcardsController();
        SQLController sqlController = new SQLController();
        DatabaseManager dbManager = new DatabaseManager();

        dbManager.CreateDatabase();
        flashcardsController.MainMenu();
    }
}