namespace Flashcards
{
    internal class InputController
    {
        Validation validator = new Validation();
        OutputController outputController = new OutputController();
        internal string GetUserInputString()
        {
            string userInput = Console.ReadLine();
            return userInput;
        }

        internal int GetUserInputInt()
        {
            string userInput = Console.ReadLine();
            while (!validator.ValidateInt(userInput))
            {
                outputController.DisplayMessage("InvalidInput");
                userInput = Console.ReadLine(); 
            }
            int userInputValid = Convert.ToInt32(userInput);
            return userInputValid;
        }

        internal bool GetUserInputBool()
        {
            string userInput = Console.ReadLine();
            while (!validator.ValidateBool(userInput))
            {
                outputController.DisplayMessage("InvalidInput");
                userInput = Console.ReadLine();
            }
            if (userInput == "Y" || userInput == "y")
                return true;
            else
                return false;
        }
    }
}
