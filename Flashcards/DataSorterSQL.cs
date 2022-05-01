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

        internal void GetListsWithAvarageSuccessSQLVesrion()
        {
            outputController.DisplayMessage("ChoseYear");
            int year = inputController.GetUserInputInt();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                                            //ROUND((CAST(NumberOfQuestions AS FLOAT) / CAST(Score AS FLOAT)) * 100, 0) AS [Score] // Use to get procentige of correct answers
                    string commandText = @$"SELECT * FROM
                                            (
                                                SELECT 
                                                    Subject AS [Subject],
                                                    DATENAME(MONTH, StudyDate) AS StudyMonth,
                                                    CAST(Score as float) AS [Score]
                                                    FROM StudyTable
                                                    Where DATENAME(YEAR, StudyDate) = '{year}'
                                            ) AS Src
                                            PIVOT
                                            (
                                                AVG([Score])
                                                FOR[StudyMonth]
                                                IN([Jan],[Feb],[Mar],[Apr],[May],[Jun],[Jul],[Aug],[Sep],[Oct],[Nov],[Dec])
                                            ) AS PivotedTable";

                    command.CommandText = commandText;
                    command.ExecuteNonQuery();
                    using (SqlDataReader sqlDataReader = command.ExecuteReader())
                    {
                        var tableDataPivoted = new List<List<object>> { };

                        while (sqlDataReader.Read())
                        {
                            tableDataPivoted.Add(new List<object>
                            {
                            sqlDataReader["Subject"],
                            string.IsNullOrEmpty(sqlDataReader["Jan"].ToString()) ? "0" : sqlDataReader["Jan"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Feb"].ToString()) ? "0" : sqlDataReader["Feb"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Mar"].ToString()) ? "0" : sqlDataReader["Mar"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Apr"].ToString()) ? "0" : sqlDataReader["Apr"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["May"].ToString()) ? "0" : sqlDataReader["May"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Jun"].ToString()) ? "0" : sqlDataReader["Jun"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Jul"].ToString()) ? "0" : sqlDataReader["Jul"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Aug"].ToString()) ? "0" : sqlDataReader["Aug"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Sep"].ToString()) ? "0" : sqlDataReader["Sep"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Oct"].ToString()) ? "0" : sqlDataReader["Oct"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Nov"].ToString()) ? "0" : sqlDataReader["Nov"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Dec"].ToString()) ? "0" : sqlDataReader["Dec"].ToString()
                            });
                        }
                        tableVisualisationEngine.DisplaySessionsInMounths(tableDataPivoted);
                    }
                }
            }
        }
        internal void GetListsWithSumOfSessionsSQLVersion(List<Models.DataForReport> tableData)
        {
            outputController.DisplayMessage("ChoseYear");
            int year = inputController.GetUserInputInt();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    string commandText = @$"SELECT * FROM
                                            (
                                                SELECT 
                                                    DATENAME(MONTH, StudyDate) AS StudyMonth,
                                                    Subject AS [Subject],
                                                    CAST(Score as float) AS [Score]
                                                    FROM StudyTable
                                                    Where DATENAME(YEAR, StudyDate) = '{year}'
                                            ) AS Src
                                            PIVOT
                                            (
                                                COUNT([Score])
                                                FOR[StudyMonth]
                                                IN([Jan],[Feb],[Mar],[Apr],[May],[Jun],[Jul],[Aug],[Sep],[Oct],[Nov],[Dec])
                                            ) AS PivotedTable";

                    command.CommandText = commandText;
                    command.ExecuteNonQuery();
                    using (SqlDataReader sqlDataReader = command.ExecuteReader())
                    {
                        var tableDataPivoted = new List<List<object>> { };

                        while (sqlDataReader.Read())
                        {
                            tableDataPivoted.Add(new List<object>
                            {
                            sqlDataReader["Subject"],
                            string.IsNullOrEmpty(sqlDataReader["Jan"].ToString()) ? "0" : sqlDataReader["Jan"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Feb"].ToString()) ? "0" : sqlDataReader["Feb"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Mar"].ToString()) ? "0" : sqlDataReader["Mar"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Apr"].ToString()) ? "0" : sqlDataReader["Apr"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["May"].ToString()) ? "0" : sqlDataReader["May"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Jun"].ToString()) ? "0" : sqlDataReader["Jun"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Jul"].ToString()) ? "0" : sqlDataReader["Jul"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Aug"].ToString()) ? "0" : sqlDataReader["Aug"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Sep"].ToString()) ? "0" : sqlDataReader["Sep"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Oct"].ToString()) ? "0" : sqlDataReader["Oct"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Nov"].ToString()) ? "0" : sqlDataReader["Nov"].ToString(),
                            string.IsNullOrEmpty(sqlDataReader["Dec"].ToString()) ? "0" : sqlDataReader["Dec"].ToString()
                            });
                        }
                        tableVisualisationEngine.DisplaySessionsInMounths(tableDataPivoted);
                    }
                }
            }
        }
    }
}
