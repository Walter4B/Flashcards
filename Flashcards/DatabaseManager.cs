using System.Data.SqlClient;
using System.Configuration;

namespace Flashcards
{
    internal class DatabaseManager
    {
        internal void CreateDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionKeyServer"].ConnectionString;


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();

                        string str = $@"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'quizDb')
                                        BEGIN
                                            CREATE DATABASE quizDb;
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
                Console.WriteLine(ex);
            }
        }

        internal void CreateTables()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionKeyDatabase"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText =
                       $@" IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'stack')
                        CREATE TABLE stack (
	                      Id int IDENTITY(1,1) NOT NULL,
	                      Name varchar(100) NOT NULL UNIQUE,
                          CreationDate varchar(30) NOT NULL,
                          EditDate varchar(30) NOT NULL,
	                      PRIMARY KEY (Id)
                         );
                      ";

                    command.ExecuteNonQuery();

                    command.CommandText =
                        $@" IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'flashcard')
                        CREATE TABLE flashcard (
                          Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
                          Question varchar(30) NOT NULL,
                          Answer varchar(30) NOT NULL,
                          CreationDate varchar(30) NOT NULL,
                          EditDate varchar(30) NOT NULL,
                          StackId int NOT NULL 
                            FOREIGN KEY 
                            REFERENCES stack(Id) 
                            ON DELETE CASCADE 
                            ON UPDATE CASCADE
                         );
                      ";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
