using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace Flashcards
{
    internal class SQLController 
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["connectionKeyMainDB"].ConnectionString;
        FlashcardsController flashcardController = new FlashcardsController();
        OutputController outputController = new OutputController(); 
        InputController inputController = new InputController();
        TableVisualisationEngine tableVisualisationEngine = new TableVisualisationEngine();

        internal void SQLConnectionCall(Action<SqlCommand> functionToPass)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    //functionToPass(command);
                    connection.Close();
                }
            }

        }

        internal void CreateTableOfDB(SqlCommand command)
        {
            command.CommandText = @"CREATE TABLE IF NOT EXISTS StackTable (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, ListOfStacks TEXT NOT NULL, CreationDate TEXT NOT NULL, EditDate TEXT NOT NULL)";
            command.ExecuteNonQuery();
        }

        internal void CreateFlashcardStack(SqlCommand command)
        {
            outputController.DisplayMessage("CreateStackInputName");
            string userInput = inputController.GetUserInputString();
            command.CommandText = $"CREATE TABLE IF NOT EXISTS '{userInput}' (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, FrontOfCard TEXT NOT NULL, BackOfCard TEXT NOT NULL, CreationDate TEXT NOT NULL, EditDate TEXT NOT NULL)";
            command.ExecuteNonQuery();
        }

        internal void DeleteFlashcardStack(SqlCommand command)
        {
            outputController.DisplayMessage("DeleteStackInstruction");
            string userInput = inputController.GetUserInputString();
            command.CommandText = $"DELETE TABLE IF EXISTS '{userInput}'";
            command.CommandText = $"DELETE FROM StackTable WHERE ListOfStacks = '{userInput}';";
            command.ExecuteNonQuery();
        }

        internal void CreateFlashCard(SqlCommand command)
        {
            FlashCard flashCard = new FlashCard();
            outputController.DisplayMessage("CreateFlashcardInputFront");
            flashCard.FlashcardFront = inputController.GetUserInputString();
            outputController.DisplayMessage("CreateFlashcardInputBack");
            flashCard.FlashcardBack = inputController.GetUserInputString();
            flashCard.DateTimeCreation = flashcardController.GetCurrentDateTime();
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

        internal void ShowStacks(SqlCommand command)
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
                tableVisualisationEngine.DisplayFlashcardsInStack(tableData);
            }
        }

        internal void ShowFlashcardsInStack(SqlCommand command)
        {
            outputController.DisplayMessage("ChoseStack");
            string userInput = inputController.GetUserInputString();
            string CommandText = $"SELECT * FROM '{userInput}'";
            command.CommandText = CommandText;
            using (SqlDataReader sqlDataReader = command.ExecuteReader())
            {
                var tableData = new List<List<object>> { };

                while (sqlDataReader.Read())
                {
                    tableData.Add(new List<object> { sqlDataReader.GetInt32(0), sqlDataReader.GetString(1), sqlDataReader.GetString(2), sqlDataReader.GetString(3) });
                }
                tableVisualisationEngine.DisplayFlashcardsInStack(tableData);
            }
        }

        internal void UpdateStack(SqlCommand command)
        { }

        internal void UpdateFlashcardInStack(SqlCommand command)
        {
            FlashCard flashcard = new FlashCard();
            outputController.DisplayMessage("UpdateChoseFlashcard");
            string frontOfCard = inputController.GetUserInputString();
            outputController.DisplayMessage("");
            string backOfCard = inputController.GetUserInputString();
            string editDate = flashcardController.GetCurrentDateTime();
            command.CommandText = $"UPDATE CodingTable SET FrontOfCard ='{frontOfCard}', BackOfCard ='{backOfCard}', EditDate ='{editDate}'";
            command.ExecuteNonQuery();
        }

        internal void StudySession()
        {
            int score = flashcardController.TestSkill(); //TODO
        }
    }
}
