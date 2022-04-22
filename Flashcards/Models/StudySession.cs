namespace Flashcards.Models
{
    internal class StudySession
    {
        public int Id { get; set; }

        public int StackId { get; set; }

        public string StackName { get; set; }

        public int NumberOfquestions { get; set; }

        public int Score { get; set; }

        public  string StudyDate { get; set; }
    }
}
