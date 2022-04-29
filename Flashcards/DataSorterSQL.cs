using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Flashcards
{
    internal class DataSorterSQL
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionKeyServer"].ConnectionString;

        TableVisualisationEngine tableVisualisationEngine = new TableVisualisationEngine();
        OutputController outputController = new OutputController(); 
        InputController inputController = new InputController();

        internal void GetListsWithAveragesSQLVesrion(List<Models.DataForReport> tableData)
        {
            outputController.DisplayMessage("ChoseYear");
            int year = inputController.GetUserInputInt();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    string commandText = @$"SELECT * FROM StudyTable
                                            PIVOT (AVG(Score)
                                                FOR Mounth in ([Jan],[Feb],[Mar],[Apr],[May],[Jun],[Jul],[Aug],[Sep],[Oct],[Nov],[Dec])) PivotedTable";

                    command.CommandText = commandText;
                    command.ExecuteNonQuery();
                    commandText = "SELECT * FROM PivotedTable";
                    command.CommandText = commandText;
                    command.ExecuteNonQuery();

                    using (SqlDataReader sqlDataReader = command.ExecuteReader())
                    {
                        var tableDataPivoted = new List<List<object>> { };

                        while (sqlDataReader.Read())
                        {
                            tableDataPivoted.Add(new List<object> { 
                                sqlDataReader.GetString(0), sqlDataReader.GetInt32(1), 
                                sqlDataReader.GetInt32(2), sqlDataReader.GetInt32(3), 
                                sqlDataReader.GetInt32(4), sqlDataReader.GetInt32(5), 
                                sqlDataReader.GetInt32(6), sqlDataReader.GetInt32(7), 
                                sqlDataReader.GetInt32(8), sqlDataReader.GetInt32(9), 
                                sqlDataReader.GetInt32(10), sqlDataReader.GetInt32(11), 
                                sqlDataReader.GetInt32(12) });
                        }
                        tableVisualisationEngine.DisplayStudySessions(tableDataPivoted);
                    }
                    connection.Close();
                }
            }
        } 

        internal void GetListsWithSumOfSessionsSQLVersion(List<Models.DataForReport> tableData)
        {
            string commandText = $"SELECT * FROM StudyTable (SELECT ";
        }
    }
}/*
Subject varchar(30) NOT NULL,
                          NumberOfQuestions int NOT NULL,
                          Score int NOT NULL,
                          StudyDate*/