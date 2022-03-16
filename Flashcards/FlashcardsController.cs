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
                        sqlController.SQLConnectionCall(sqlController.CreateFlashCard);
                        break;
                    case 2:
                        sqlController.SQLConnectionCall(sqlController.DeleteFlashCard);
                        break;
                    case 3:
                        sqlController.SQLConnectionCall(sqlController.UpdateFlashcard);
                        break;
                    case 4:
                        sqlController.SQLConnectionCall(sqlController.ShowTableBySubjects);
                        break;
                    case 5:
                        StudySession();
                        break;

                }
            }
        }

        internal int TestSkill()
        {
            int score = 0;
            bool inProgress = true;

            while (inProgress) //n <= PullStack.numberOfFlashcards 
            {
                while (inputController.GetUserInputString() == "true") // test if user input == flashcardBack
                    score++; // needs to be fixed with logic for scoring
                inProgress = false;
            }

            return score; //crate date and score table
        }

        internal void StudySession()
        {
            int score = TestSkill(); //TODO
        }

        internal void PullStackOfFlashcards()
        { 

        }
    }
}
