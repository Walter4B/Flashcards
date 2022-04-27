using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class DataSorter
    {
        internal List<List<object>> GetListsWithAverages(List<Models.DataForReport> inputList)
        {
            List<List<object>> sortedList = new List<List<object>>();
            List<string> listOfSubjects = new List<string>();
          
                foreach (var element in inputList)
                {
                    listOfSubjects.Add(element.name);
                }
                listOfSubjects = listOfSubjects.Distinct().ToList();

            foreach (var element in listOfSubjects)
            {
                sortedList.Add(new List<object> { element, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0});
            }

            foreach (var element in inputList)
            {
                for (int i = 0; i < listOfSubjects.Count; i++)
                {
                    if (element.name == sortedList[i][0].ToString())
                    {
                        string tempstring = element.sessionDate;
                        string tempSubstring = $"{tempstring[3]}" + $"{tempstring[4]}";
                        int temp2 = Convert.ToInt32(tempSubstring);
                        int temp = Convert.ToInt32(sortedList[i][temp2]) + Convert.ToInt32(element.score);
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
