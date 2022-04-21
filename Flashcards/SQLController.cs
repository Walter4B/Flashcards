using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace Flashcards
{
    internal class SQLController
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionKeyServer"].ConnectionString;
        OutputController outputController = new OutputController();
        InputController inputController = new InputController();
        TableVisualisationEngine tableVisualisationEngine = new TableVisualisationEngine();

        internal void CreateFlashCard()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    outputController.DisplayMessage("ChoseStack");
                    int stackRef = inputController.GetUserInputInt();
                    while (!CheckIfStackExists(stackRef))
                    {
                        outputController.DisplayMessage("StackNotExist");
                        stackRef = inputController.GetUserInputInt();
                    }
                    bool running = true;
                    while (running)
                    {
                        Models.FlashCard flashCard = new Models.FlashCard();
                        flashCard.StackReference = stackRef;
                        outputController.DisplayMessage("CreateFlashcardInputFront");
                        flashCard.FlashcardFront = inputController.GetUserInputString();
                        outputController.DisplayMessage("CreateFlashcardInputBack");
                        flashCard.FlashcardBack = inputController.GetUserInputString();
                        flashCard.DateTimeCreation = DateTime.Now.ToString();
                        command.CommandText = $@"INSERT INTO FlashcardTable (FlashcardFront, FlashcardBack, DateTimeCreation, DateTimeEdit, StackId) VALUES('{flashCard.FlashcardFront}','{flashCard.FlashcardBack}','{flashCard.DateTimeCreation}','{flashCard.DateTimeCreation}','{flashCard.StackReference}')";
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        outputController.DisplayMessage("ContinuInputing?");
                        running = inputController.GetUserInputBool();
                    }
                }
            }
        }

        internal void DeleteFlashCard()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    bool running = true;
                    while (running)
                    {
                        connection.Open();
                        outputController.DisplayMessage("DeleteFlashcardInstruction");
                        int userInputFlashCard = inputController.GetUserInputInt();
                        command.CommandText = $"DELETE FROM FlashcardTable WHERE ID = '{userInputFlashCard}';";
                        command.ExecuteNonQuery();
                        connection.Close();
                        outputController.DisplayMessage("ContinueDeleteing?");
                        running = inputController.GetUserInputBool();
                    }
                }
            }
        }

        internal void UpdateFlashcard() 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    Models.FlashCard flashcard = new Models.FlashCard();
                    outputController.DisplayMessage("UpdateChoseFlashcard");
                    string id = inputController.GetUserInputString();
                    outputController.DisplayMessage("CreateFlashcardInputFront");
                    string frontOfCard = inputController.GetUserInputString();
                    outputController.DisplayMessage("CreateFlashcardInputBack");
                    string backOfCard = inputController.GetUserInputString();
                    string editDate = DateTime.Now.ToString();
                    command.CommandText = $"UPDATE FlashcardTable SET FlashcardFront ='{frontOfCard}', FlashcardBack ='{backOfCard}', DateTimeEdit ='{editDate}' WHERE ID = '{id}'";
                    command.ExecuteNonQuery();
                    connection.Close();
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
                    Models.FlashCard flashCard = new Models.FlashCard();
                    outputController.DisplayMessage("ChoseStack");
                    flashCard.StackReference = inputController.GetUserInputInt();
                    string CommandText = $"SELECT * FROM FlashcardTable WHERE StackId = '{flashCard.StackReference}'";
                    command.CommandText = CommandText;
                    using (SqlDataReader sqlDataReader = command.ExecuteReader())
                    {
                        var tableData = new List<List<object>> { };

                        while (sqlDataReader.Read())
                        {
                            tableData.Add(new List<object> { sqlDataReader.GetInt32(0), sqlDataReader.GetString(1), sqlDataReader.GetString(2), sqlDataReader.GetInt32(5), sqlDataReader.GetString(3), sqlDataReader.GetString(4) });
                        }
                        tableVisualisationEngine.DisplayFlashcardsInStack(tableData);
                    }
                    connection.Close();
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
                    connection.Close();
                }
            }
        }

        internal void UpdateStack()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    Models.Stack flashcard = new Models.Stack();
                    outputController.DisplayMessage("UpdateChoseStack");
                    int stackId = inputController.GetUserInputInt();
                    outputController.DisplayMessage("UpdateNewStackName");
                    string newName = inputController.GetUserInputString();
                    string editDate = DateTime.Now.ToString();
                    command.CommandText = $"UPDATE StackTable SET Name ='{newName}', EditDate ='{editDate}' Where Id = {stackId}";
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        internal void DeleteStack()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    outputController.DisplayMessage("DeleteStackInstruction");
                    int userInputFlashCard = inputController.GetUserInputInt();
                    command.CommandText = $"DELETE FROM StackTable WHERE ID = '{userInputFlashCard}';";
                    command.CommandText = $"DELETE FROM FlashcardTable WHERE StackId = '{userInputFlashCard}';";
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        internal void ShowStacks()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
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
                    connection.Close();
                }
            }
        }

        internal static bool CheckIfStackExists(int id)
        {
            string CommandText = "SELECT * FROM StackTable WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            { 
                using (SqlCommand command = new SqlCommand(CommandText, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    if(reader.HasRows)
                        return true;
                    else
                        return false;
                }
            }
        }

        internal void GetStudySessions()
        {

            Console.WriteLine("work in progress");

        }

    }
}
