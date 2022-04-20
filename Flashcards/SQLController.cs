using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace Flashcards
{
    internal class SQLController 
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionKeyServer"].ConnectionString;
        OutputController outputController = new OutputController(); 
        InputController inputController = new InputController();
        TableVisualisationEngine tableVisualisationEngine = new TableVisualisationEngine();

        internal void CreateFlashCard()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    Models.FlashCard flashCard = new Models.FlashCard();
                    outputController.DisplayMessage("ChoseStack");
                    string stackReference = inputController.GetUserInputString();
                    outputController.DisplayMessage("CreateFlashcardInputFront");
                    flashCard.FlashcardFront = inputController.GetUserInputString();
                    outputController.DisplayMessage("CreateFlashcardInputBack");
                    flashCard.FlashcardBack = inputController.GetUserInputString();
                    flashCard.DateTimeCreation = DateTime.Now.ToString();
                    command.CommandText = $@"INSERT INTO FlashcardTable (FlashcardFront, FlashcardBack, DateTimeCreation, DateTimeEdit, StackReference) VALUES('{flashCard.FlashcardFront}','{flashCard.FlashcardBack}','{flashCard.DateTimeCreation}','{flashCard.DateTimeCreation}','{flashCard.StackReference}')";
                    command.ExecuteNonQuery();
                }
            }
        }

        internal void DeleteFlashCard()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    outputController.DisplayMessage("DeleteFlashcardInstruction");
                    int userInputFlashCard = inputController.GetUserInputInt();
                    command.CommandText = $"DELETE FROM FlashcardTable WHERE ID = '{userInputFlashCard}';";
                    command.ExecuteNonQuery();
                }
            }
        }

        internal void UpdateFlashcard()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    Models.FlashCard flashcard = new Models.FlashCard();
                    outputController.DisplayMessage("UpdateChoseFlashcard");
                    string frontOfCard = inputController.GetUserInputString();
                    outputController.DisplayMessage("");
                    string backOfCard = inputController.GetUserInputString();
                    string editDate = DateTime.Now.ToString();
                    command.CommandText = $"UPDATE FlashcardTable SET FrontOfCard ='{frontOfCard}', BackOfCard ='{backOfCard}', EditDate ='{editDate}'";
                    command.ExecuteNonQuery();
                }
            }
        }

        internal void ShowFlashcards()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    string CommandText = $"SELECT * FROM FlashcardTable";
                    command.CommandText = CommandText;
                    using (SqlDataReader sqlDataReader = command.ExecuteReader())
                    {
                        var tableData = new List<List<object>> { };

                        while (sqlDataReader.Read())
                        {
                            tableData.Add(new List<object> { sqlDataReader.GetInt32(0), sqlDataReader.GetString(1), sqlDataReader.GetString(2), sqlDataReader.GetString(3), sqlDataReader.GetString(4) });
                        }
                        tableVisualisationEngine.DisplayStacks(tableData);
                    }
                }
            }
        }

        internal void CreateStack()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    Models.Stack stack = new Models.Stack();
                    outputController.DisplayMessage("CreateStackInputName");
                    stack.StackName = inputController.GetUserInputString();
                    stack.DateTimeCreation = DateTime.Now.ToString();
                    command.CommandText = $"INSERT INTO StackTable (Name, CreationDate, EditDate) VALUES('{stack.StackName}','{stack.DateTimeCreation}','{stack.DateTimeCreation}')";
                    command.ExecuteNonQuery();
                }
            }
            
        }

        internal void UpdateStack() 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    Models.Stack flashcard = new Models.Stack();
                    outputController.DisplayMessage("UpdateChoseStack");
                    int stackId = inputController.GetUserInputInt();
                    outputController.DisplayMessage("UpdateNewStackName");
                    string newName = inputController.GetUserInputString();
                    string editDate = DateTime.Now.ToString();
                    command.CommandText = $"UPDATE StackTable SET Name ='{newName}', EditDate ='{editDate}' Where Id = {stackId}";
                    command.ExecuteNonQuery();
                }
            }
        }

        internal void DeleteStack()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    outputController.DisplayMessage("DeleteStackInstruction");
                    int userInputFlashCard = inputController.GetUserInputInt();
                    command.CommandText = $"DELETE FROM StackTable WHERE ID = '{userInputFlashCard}';";
                    command.ExecuteNonQuery();
                }
            }
        }

        internal void ShowStack()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    outputController.DisplayMessage("ChoseStack");
                    string userInput = inputController.GetUserInputString();
                    string CommandText = $"SELECT * FROM FlashcardTable where StackReference = '{userInput}'";
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
            }
        }

        internal void GetStudySessions() 
        {
            Console.WriteLine();
        }

    }
}
