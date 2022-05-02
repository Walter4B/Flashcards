using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;


namespace Flashcards
{
    internal class SQLController
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionKeyServer"].ConnectionString;
        OutputController outputController = new OutputController();
        InputController inputController = new InputController();
        DataSorter sorter = new DataSorter();
        DataSorterSQL sorterSQL = new DataSorterSQL();
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
                        flashCard.DateTimeCreation = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
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
                    string editDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
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
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        Models.Stack stack = new Models.Stack();
                        outputController.DisplayMessage("CreateStackInputName");
                        stack.StackName = inputController.GetUserInputString();
                        stack.DateTimeCreation = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        command.CommandText = $"INSERT INTO StackTable (Name, CreationDate, EditDate) VALUES('{stack.StackName}','{stack.DateTimeCreation}','{stack.DateTimeCreation}')";
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {
                outputController.DisplayMessage("CreateStackNameExists");
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
                    string editDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
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
                    if (reader.HasRows)
                        return true;
                    else
                        return false;
                }
            }
        }

        internal void GetStudySessions()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    string CommandText = $"SELECT * FROM StudyTable";
                    command.CommandText = CommandText;
                    using (SqlDataReader sqlDataReader = command.ExecuteReader())
                    {
                        var tableData = new List<List<object>> { };

                        while (sqlDataReader.Read())
                        {
                            tableData.Add(new List<object> { sqlDataReader.GetString(1), sqlDataReader.GetInt32(2), sqlDataReader.GetInt32(3), sqlDataReader.GetString(4) });
                        }
                        tableVisualisationEngine.DisplayStudySessions(tableData);
                    }
                    connection.Close();
                }
            }
        }

        internal void StartStudySessions()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    SqlDataReader sqlDataReader;
                    Models.StudySession studySession = new Models.StudySession();
                    outputController.DisplayMessage("ChoseStack");
                    studySession.StackId = inputController.GetUserInputInt();
                    string CommandText = $"SELECT * FROM FlashcardTable WHERE StackId = '{studySession.StackId}'";
                    command.CommandText = CommandText;
                    connection.Open();
                    var frontData = new List<List<object>> { };
                    var backData = new List<List<object>> { };
                    using (sqlDataReader = command.ExecuteReader())
                    {
                        string currentAnswer;
                        studySession.Score = 0;
                        while (sqlDataReader.Read())
                        {
                            frontData.Add(new List<object> { sqlDataReader.GetString(1) });
                            tableVisualisationEngine.DisplayFlashcardStudy(frontData);
                            backData.Add(new List<object> { sqlDataReader.GetString(2) });
                            outputController.DisplayMessage("StudyInputAnswer");
                            currentAnswer = inputController.GetUserInputString();
                            if (currentAnswer == backData.Last().Last().ToString())
                            {
                                studySession.Score++;
                            }
                            frontData.Clear();
                        }
                    }
                    connection.Close();
                    int numOfQuestions = backData.Count;
                    string stackName = GetSingleStackName(studySession.StackId);
                    studySession.StudyDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    CommandText = $"INSERT INTO StudyTable (Subject, NumberOfQuestions, Score, StudyDate) " +
                        $"VALUES ('{stackName}','{numOfQuestions}','{studySession.Score}','{studySession.StudyDate}')";
                    command.CommandText = CommandText;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        internal string GetSingleStackName(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    Models.StudySession studySession = new Models.StudySession();
                    string stackName = "null";
                    SqlDataReader sqlDataReader;
                    connection.Open();
                    string CommandText = $"SELECT * FROM StackTable WHERE Id = '{id}'";
                    command.CommandText = CommandText;
                    using (sqlDataReader = command.ExecuteReader())
                    {
                        sqlDataReader.Read();
                        stackName = sqlDataReader.GetString(1).ToString();
                        sqlDataReader.Close();
                    }
                    connection.Close();
                    return stackName;
                }
            }
        }

        internal void DisplayNumberOfMounthlySessions()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    string CommandText = "SELECT * FROM StudyTable";
                    command.CommandText = CommandText;
                    List<Models.DataForReport> tableData = new List<Models.DataForReport>();
                    using (SqlDataReader sqlDataReader = command.ExecuteReader())
                    {

                        while (sqlDataReader.Read())
                        {
                            tableData.Add(new Models.DataForReport { name = sqlDataReader.GetString(1), score = sqlDataReader.GetInt32(2), sessionDate = sqlDataReader.GetString(4) });
                        }
                    }
                    //sorter.GetListsWithSumOfSessions(tableData); //C# solution
                    sorterSQL.GetListsWithSumOfSessionsSQLVersion(tableData);//SQL solution
                }
            }
        }

        internal void DisplayMounthlyAvarageScore()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    string CommandText = "SELECT * FROM StudyTable";
                    command.CommandText = CommandText;
                    List<Models.DataForReport> tableData = new List<Models.DataForReport>();

                    using (SqlDataReader sqlDataReader = command.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            tableData.Add(new Models.DataForReport { name = sqlDataReader.GetString(1), score = sqlDataReader.GetInt32(2), sessionDate = sqlDataReader.GetString(4) });
                        }
                    }
                    //sorter.GetListsWithAverages(tableData); //C# solution
                    sorterSQL.GetListsWithAvarageSuccessSQLVesrion();//SQL solution
                }
            }
        }
    }
}


