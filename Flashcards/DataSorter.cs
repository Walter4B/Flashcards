using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class DataSorter
    {
        internal List<List<object>> GetListsWithAverages(List<Models.DataForReport> inputedList)
        {
            List<List<object>> sortedList = new List<List<object>>();
            List<string> listOfSubjects = new List<string>();
          
                foreach (var property in inputedList)
                {
                    listOfSubjects.Add(property.name);
                }
                listOfSubjects = listOfSubjects.Distinct().ToList();

            foreach (var subject in listOfSubjects)
            {
                sortedList.Add(new List<object> { subject, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0});
            }

            foreach (var property in inputedList)
            {
                for (int i = 0; i < listOfSubjects.Count; i++)
                {
                    if (property.name == sortedList[i][0].ToString())
                    {
                        string tempstring = property.sessionDate;
                        string tempSubstring = $"{tempstring[3]}" + $"{tempstring[4]}";
                        int temp2 = Convert.ToInt32(tempSubstring);
                        int temp = Convert.ToInt32(sortedList[i][temp2]) + Convert.ToInt32(property.score);
                        sortedList[i][temp2] = temp;
                    }
                }
            }
            return sortedList;
        }

        internal List<List<object>> GetListsWithSumOfSessions(List<Models.DataForReport> inputList)
        {
            List<Models.MonthlyValues> sortedList;
            return null;
        }
    }
}
