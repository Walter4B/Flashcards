using System.Data.SqlClient;
using System.Configuration;

namespace Flashcards
{
    internal class DatabaseManager
    {
        internal void CreateDatabase()
        {

            string connectionString = "Server=(LocalDb)\\LocalDBDemo; Integrated Security=true;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();

                        string str = $@"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'FlashcardDatabase')
                                        BEGIN
                                            CREATE DATABASE FlashcardDatabase;
                                        END;
                                     ";

                        command.CommandText = str;
                        command.ExecuteNonQuery();
                    }
                }
                CreateTables();
            }

            catch (Exception ex)
            {
                Console.WriteLine("table already existst");
            }
        }

        internal void CreateTables()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["connectionKeyServer"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText =
                       $@" IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'StackTable')
                        CREATE TABLE StackTable (
	                      Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	                      Name varchar(30) NOT NULL UNIQUE,
                          CreationDate varchar(30) NOT NULL,
                          EditDate varchar(30) NOT NULL,
                         );
                      ";

                    command.ExecuteNonQuery();

                    command.CommandText =
                        $@" IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'FlashcardTable')
                        CREATE TABLE FlashcardTable (
                          Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
                          FlashcardFront varchar(30) NOT NULL,
                          FlashcardBack varchar(30) NOT NULL,
                          DateTimeCreation varchar(30) NOT NULL,
                          DateTimeEdit varchar(30) NOT NULL,
                          StackId int NOT NULL
                            FOREIGN KEY 
                            REFERENCES StackTable(Id) 
                            ON DELETE CASCADE 
                            ON UPDATE CASCADE
                         );
                      ";

                    command.ExecuteNonQuery();

                    command.CommandText =
                      $@" IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'StudyTable')
                        CREATE TABLE StudyTable (
	                      Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
                          Subject varchar(30) NOT NULL,
	                      NumberOfQuestions int NOT NULL,
                          Score int NOT NULL,
                          StudyDate varchar(30) NOT NULL,
                          Mounth varchar(3) NOT NULL,
                          Year int NOT NULL,
                         );
                      ";

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
