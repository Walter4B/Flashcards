using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class DataSorter
    {
        InputController inputController = new InputController();
        OutputController outputController = new OutputController();
        TableVisualisationEngine tableVisualisationEngine = new TableVisualisationEngine();

        internal void GetListsWithAverages(List<Models.DataForReport> inputList)
        {
            List<Models.MonthlyValues> propertyList = AdjustData(inputList);
            if (propertyList == null)
            {
                CheckIfYearExists(null);
            }
            else
            {
                List<List<object>> outputList = new List<List<object>>();
                foreach (var property in propertyList)
                {
                    outputList.Add(new List<object> {
                    property.name,
                    property.Jan.Sum() / (property.Jan.Count == 0 ? 1 : property.Jan.Count),
                    property.Feb.Sum() / (property.Feb.Count == 0 ? 1 : property.Feb.Count),
                    property.Mar.Sum() / (property.Mar.Count == 0 ? 1 : property.Mar.Count),
                    property.Apr.Sum() / (property.Apr.Count == 0 ? 1 : property.Apr.Count),
                    property.May.Sum() / (property.May.Count == 0 ? 1 : property.May.Count),
                    property.Jun.Sum() / (property.Jun.Count == 0 ? 1 : property.Jun.Count),
                    property.Jul.Sum() / (property.Jul.Count == 0 ? 1 : property.Jul.Count),
                    property.Aug.Sum() / (property.Aug.Count == 0 ? 1 : property.Aug.Count),
                    property.Sep.Sum() / (property.Sep.Count == 0 ? 1 : property.Sep.Count),
                    property.Oct.Sum() / (property.Oct.Count == 0 ? 1 : property.Oct.Count),
                    property.Nov.Sum() / (property.Nov.Count == 0 ? 1 : property.Nov.Count),
                    property.Dec.Sum() / (property.Dec.Count == 0 ? 1 : property.Dec.Count),

                }); ;
                }
                CheckIfYearExists(outputList);
            }
        }

        internal void GetListsWithSumOfSessions(List<Models.DataForReport> inputList)
        {
            List<Models.MonthlyValues> propertyList = AdjustData(inputList);
            if (propertyList == null)
            {
                CheckIfYearExists(null);
            }
            else
            {
                List<List<object>> outputList = new List<List<object>>();
                foreach (var property in propertyList)
                {
                    outputList.Add(new List<object> {
                    property.name,
                    property.Jan.Count,
                    property.Feb.Count,
                    property.Mar.Count,
                    property.Apr.Count,
                    property.May.Count,
                    property.Jun.Count,
                    property.Jul.Count,
                    property.Aug.Count,
                    property.Sep.Count,
                    property.Oct.Count,
                    property.Nov.Count,
                    property.Dec.Count

                }); ;
                }
                CheckIfYearExists(outputList);
            }
        }

        internal List<Models.MonthlyValues> AdjustData(List<Models.DataForReport> inputedList)
        {
            outputController.DisplayMessage("ChoseYear");
            int year = inputController.GetUserInputInt();
            
            List<Models.DataForReport> adjustedInputedList = new List<Models.DataForReport>();
            List<Models.MonthlyValues> propertyList = new List<Models.MonthlyValues>();
            List<string> listOfSubjects = new List<string>();

            foreach (var property in inputedList)
            {
                if (year.ToString() == GetYear(property.sessionDate))
                {
                    adjustedInputedList.Add(property);
                }
            }

            foreach (var property in adjustedInputedList)
            {
                listOfSubjects.Add(property.name);
            }

            if (!listOfSubjects.Any())
            {
                return null;
            }
            listOfSubjects = listOfSubjects.Distinct().ToList();

            foreach (var subject in listOfSubjects)
            {
                propertyList.Add(new Models.MonthlyValues
                {
                    name = subject,
                    Jan = new List<float>(),
                    Feb = new List<float>(),
                    Mar = new List<float>(),
                    Apr = new List<float>(),
                    May = new List<float>(),
                    Jun = new List<float>(),
                    Jul = new List<float>(),
                    Aug = new List<float>(),
                    Sep = new List<float>(),
                    Oct = new List<float>(),
                    Nov = new List<float>(),
                    Dec = new List<float>()
                });
            }

            foreach (var property in adjustedInputedList)
            {
                for (int i = 0; i < listOfSubjects.Count; i++)
                {
                    if (property.name == propertyList[i].name)
                    {
                        int mounthValue = Convert.ToInt32(GetMounth(property.sessionDate));
                        int tempScore = Convert.ToInt32(property.score);
                        switch (mounthValue)
                        {
                            case 1:
                                propertyList[i].Jan.Add(tempScore);
                                break;
                            case 2:
                                propertyList[i].Feb.Add(tempScore);
                                break;
                            case 3:
                                propertyList[i].Mar.Add(tempScore);
                                break;
                            case 4:
                                propertyList[i].Apr.Add(tempScore);
                                break;
                            case 5:
                                propertyList[i].May.Add(tempScore);
                                break;
                            case 6:
                                propertyList[i].Jun.Add(tempScore);
                                break;
                            case 7:
                                propertyList[i].Jul.Add(tempScore);
                                break;
                            case 8:
                                propertyList[i].Aug.Add(tempScore);
                                break;
                            case 9:
                                propertyList[i].Sep.Add(tempScore);
                                break;
                            case 10:
                                propertyList[i].Oct.Add(tempScore);
                                break;
                            case 11:
                                propertyList[i].Nov.Add(tempScore);
                                break;
                            case 12:
                                propertyList[i].Dec.Add(tempScore);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return propertyList;
        }

        internal string GetYear(string dateString)
        {
            string year = $"{dateString[6]}" + $"{dateString[7]}" + $"{dateString[8]}" + $"{dateString[9]}";
            return year;
        }

        internal string GetMounth(string dateString)
        {
            string mounth = $"{dateString[3]}" + $"{dateString[4]}";
            return mounth;
        }

        internal void CheckIfYearExists(List<List<object>> sortedList)
        {
            if (sortedList == null)
            {
                outputController.DisplayMessage("NoDataForYear");
            }
            else
            {
                tableVisualisationEngine.DisplaySessionsInMounths(sortedList);
            }
        }
    }
}
