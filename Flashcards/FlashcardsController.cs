namespace Flashcards
{
    internal class FlashcardsController
    {
        InputController inputController = new InputController();
        OutputController outputController = new OutputController();
        SQLController sqlController = new SQLController();

        internal void MainSwitchLoop()
        {
            bool programRunning = true;
            while (programRunning)
            {
                outputController.DisplayMessage("Main");
                switch (inputController.GetUserInputInt())
                {
                    case 0:
                        programRunning = false;
                        break;
                    case 1:
                        sqlController.SQLConnectionCall(sqlController.CreateFlashcardStack);
                        break;
                    case 2:
                        sqlController.SQLConnectionCall(sqlController.CreateFlashCard);
                        break;
                    case 3:
                        sqlController.SQLConnectionCall(sqlController.DeleteFlashcardStack);
                        break;
                    case 4:
                        sqlController.SQLConnectionCall(sqlController.DeleteFlashCard);
                        break;
                    case 5:
                        sqlController.SQLConnectionCall(sqlController.ShowStacks);
                        break;
                    case 6:
                        sqlController.SQLConnectionCall(sqlController.ShowFlashcardsInStack);
                        break;
                    case 7:
                        sqlController.SQLConnectionCall(sqlController.UpdateStack);
                        break;
                    case 8:
                        sqlController.SQLConnectionCall(sqlController.UpdateFlashcardInStack);
                        break;
                    case 9:
                        sqlController.StudySession();
                        break;

                }
            }
        }

        internal int TestSkill()
        {
            int score = 0;

            while (true) //n <= PullStack.numberOfFlashcards 
            {
                if (inputController.GetUserInputString() == "true") // test if user input == flashcardBack
                    score++;
            }

            return score; //crate date and score table
        }

        internal void PullStackOfFlashcards()
        { 

        }

        internal string GetCurrentDateTime()
        {
            DateTime currentDateTime = DateTime.Now;
            string currentDateTimeString = currentDateTime.ToString();

            return currentDateTimeString;
        }
    }
}
