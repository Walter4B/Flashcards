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
    }
}
