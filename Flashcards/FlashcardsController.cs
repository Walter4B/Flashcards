namespace Flashcards
{
    internal class FlashcardsController
    {
        InputController inputController = new InputController();
        OutputController outputController = new OutputController();
        SQLController sqlController = new SQLController();

        internal void MainMenu()
        {
            bool programRunning = true;
            while (programRunning)
            {
                outputController.DisplayMessage("MainMenu");
                switch (inputController.GetUserInputInt())
                {
                    case 0:
                        programRunning = false;
                        break;
                    case 1:
                        StacksMenu();
                        break;
                    case 2:
                        StudyMenu();
                        break;
                    case 3:
                        ReportMenu();
                        break;
                    default:
                        outputController.DisplayMessage("InvalidInput");
                        break;

                }
            }
        } 

        internal void StacksMenu()
        {
            bool programRunning = true;
            while (programRunning)
            {
                outputController.DisplayMessage("StackMenu");
                switch (inputController.GetUserInputInt())
                {
                    case 0:
                        programRunning = false;
                        break;
                    case 1:
                        sqlController.CreateStack();
                        break;
                    case 2:
                        FlashcardsMenu();
                        break;
                    case 3:
                        sqlController.ShowStacks();
                        break;
                    default:
                        outputController.DisplayMessage("InvalidInput");
                        break;

                }
            }
        }

        internal void FlashcardsMenu()
        {
            bool programRunning = true;
            while (programRunning)
            {
                outputController.DisplayMessage("FleshcardMenu");
                switch (inputController.GetUserInputInt())
                {
                    case 0:
                        programRunning = false;
                        break;
                    case 1:
                        sqlController.UpdateStack();
                        break;
                    case 2:
                        sqlController.DeleteStack();
                        break;
                    case 3:
                        sqlController.CreateFlashCard();
                        break;
                    case 4:
                        sqlController.DeleteFlashCard();
                        break;
                    case 5:
                        sqlController.UpdateFlashcard();
                        break;
                    case 6:
                        sqlController.ShowFlashcards();
                        break;
                    default:
                        outputController.DisplayMessage("InvalidInput");
                        break;

                }
            }
        }

        internal void StudyMenu()
        {
            bool programRunning = true;
            while (programRunning)
            {
                outputController.DisplayMessage("StudyMenu");
                switch (inputController.GetUserInputInt())
                {
                    case 0:
                        programRunning = false;
                        break;
                    case 1:
                        sqlController.StartStudySessions();
                        break;
                    case 2:
                        sqlController.GetStudySessions();
                        break;
                    default:
                        outputController.DisplayMessage("InvalidInput");
                        break;

                }
            }
        }

        internal void ReportMenu()
        {
            bool programRunning = true;
            while (programRunning)
            {
            outputController.DisplayMessage("ReportMenu");
                switch (inputController.GetUserInputInt())
                {
                    case 0:
                        programRunning = false;
                        break;
                    case 1:
                        sqlController.DisplayNumberOfMounthlySessions();
                        break;
                    case 2:
                        sqlController.DisplayMounthlyAvarageScore();
                        break;
                    default:
                        outputController.DisplayMessage("InvalidInput");
                        break;
                }
            }
        }
    }
}
