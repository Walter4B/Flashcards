namespace Flashcards
{
    internal class FlashcardsController
    {
        InputController inputController = new InputController();
        OutputController outputController = new OutputController();
        SQLController sqlController = new SQLController();
        StudyController studyController = new StudyController();   

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

        internal void StudyMenu() //TODO
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
                        studyController.NewStudySession(); //TODO
                        break;
                    case 2:
                        sqlController.GetStudySessions(); //TODO
                        break;
                    default:
                        outputController.DisplayMessage("InvalidInput");
                        break;

                }
            }
        }
    }
}
