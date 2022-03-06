namespace Flashcards
{
    internal class Validation
    {
        internal bool ValidateInt(string userInput)
        {
            if (string.IsNullOrEmpty(userInput) || !int.TryParse(userInput, out _))
                return false;
            else
                return true;
        }
    }
}
