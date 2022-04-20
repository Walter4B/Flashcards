namespace Flashcards.Models
{
    internal class Stack
    {
        public int StackId { get; set; }

        public string StackName { get; set; }

        public FlashCard[] Flashcards { get; set; }

        public string DateTimeCreation { get; set; }

        public string DateTimeEdit { get; set; }

    }
}
