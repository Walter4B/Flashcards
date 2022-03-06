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
                        sqlController.CreateFlashcardStack();
                        break;
                    case 2:
                        sqlController.CreateFlashCard();
                        break;
                    case 3:
                        sqlController.DeleteFlashcardStack();
                        break;
                    case 4:
                        sqlController.DeleteFlashCard();
                        break;
                    case 5:
                        sqlController.ShowStacks();
                        break;
                    case 6:
                        sqlController.ShowFlashcardsInStack();
                        break;
                    case 7:
                        sqlController.UpdateStack();
                        break;
                    case 8:
                        sqlController.UpdateFlashcardInStack();
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
    }
}
