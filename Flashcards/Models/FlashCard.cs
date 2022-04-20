namespace Flashcards.Models
{
    internal class FlashCard
    {
        public int FlashcardId { get; set; }

        public int StackReference { get; set; }

        public string FlashcardFront { get; set; }

        public string FlashcardBack { get; set; }

        public string DateTimeCreation { get; set; }

        public string DateTimeEdit { get; set; }
    }
}
