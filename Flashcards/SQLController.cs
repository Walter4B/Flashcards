using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace Flashcards
{
    internal class SQLController 
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["connectionKey"].ConnectionString;
        //FlashcardsController flashcardController = new FlashcardsController();
        OutputController outputController = new OutputController(); 
        InputController inputController = new InputController();
        TableVisualisationEngine tableVisualisationEngine = new TableVisualisationEngine();

        internal void SQLConnectionCall(Action<SqlCommand> functionToPass)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    functionToPass(command);
                }
            }

        }

        internal void CreateDatabase()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    string str = @"CREATE DATABASE IF NOT EXISTS MyDatabase ON PRIMARY " +
                  "(NAME = MyDatabase_Data, " +
                  "FILENAME = 'D:/Projects/Flashcards.mdf', " +
                  "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
                  "SIZE = 1MB, " +
                  "MAXSIZE = 5MB, " +
                  "FILEGROWTH = 10%)";

                    command.CommandText = str;
                    command.ExecuteNonQuery();
                }
            }
        }

        internal void CreateTables(SqlCommand command)
        {
            command.CommandText = @"CREATE TABLE IF NOT EXISTS SubjectKeysTable (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, ListOfStacks TEXT NOT NULL, CreationDate TEXT NOT NULL, EditDate TEXT NOT NULL)";
            command.CommandText = @"CREATE TABLE IF NOT EXISTS FlashcardsTable (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Front TEXT NOT NULL, Back TEXT NOT NULL, CreationDate TEXT NOT NULL, EditDate TEXT NOT NULL)";
            command.ExecuteNonQuery();
        }

        internal void CreateFlashCard(SqlCommand command)
        {
            FlashCard flashCard = new FlashCard();
            outputController.DisplayMessage("CreateFlashcardInputFront");
            flashCard.FlashcardFront = inputController.GetUserInputString();
            outputController.DisplayMessage("CreateFlashcardInputBack");
            flashCard.FlashcardBack = inputController.GetUserInputString();
            flashCard.DateTimeCreation = DateTime.Now.ToString();
            command.CommandText = $"INSERT INTO CodingTable (FrontOfCard, BackOfCard, CreationDate, EditDate) VALUES('{flashCard.FlashcardFront}','{flashCard.FlashcardBack}','{flashCard.DateTimeCreation}','{flashCard.DateTimeCreation}');";
            command.ExecuteNonQuery();
        }

        internal void DeleteFlashCard(SqlCommand command)
        {
            outputController.DisplayMessage("ChoseStack");
            string userInputStack = inputController.GetUserInputString();
            outputController.DisplayMessage("DeleteFlashcardInstruction");
            string userInputFlashCard = inputController.GetUserInputString();
            command.CommandText = $"DELETE FROM '{userInputStack}' WHERE ID = '{userInputFlashCard}';";
            command.ExecuteNonQuery();
        }

        internal void ShowTableBySubjects(SqlCommand command)
        {
            outputController.DisplayMessage("ChoseStack");
            string userInput = inputController.GetUserInputString();
            string CommandText = $"SELECT * FROM StackTable";
            command.CommandText = CommandText;
            using (SqlDataReader sqlDataReader = command.ExecuteReader())
            {
                var tableData = new List<List<object>> { };

                while (sqlDataReader.Read())
                {
                    tableData.Add(new List<object> { sqlDataReader.GetInt32(0), sqlDataReader.GetString(1), sqlDataReader.GetString(2), sqlDataReader.GetString(3) });
                }
                tableVisualisationEngine.DisplayStacks(tableData);
            }
        }

        internal void UpdateFlashcard(SqlCommand command)
        {
            FlashCard flashcard = new FlashCard();
            outputController.DisplayMessage("UpdateChoseFlashcard");
            string frontOfCard = inputController.GetUserInputString();
            outputController.DisplayMessage("");
            string backOfCard = inputController.GetUserInputString();
            string editDate = DateTime.Now.ToString();
            command.CommandText = $"UPDATE CodingTable SET FrontOfCard ='{frontOfCard}', BackOfCard ='{backOfCard}', EditDate ='{editDate}'";
            command.ExecuteNonQuery();
        }
    }
}
