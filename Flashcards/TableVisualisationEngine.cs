using ConsoleTableExt;

namespace Flashcards
{
    internal class TableVisualisationEngine
    {
        internal void DisplayStacks(List<List<Object>> ListOfTableLines)
        {
            ConsoleTableBuilder
                    .From(ListOfTableLines)
                    .WithColumn("Id", "Names Of Stacks", "Creation Date", "Edit Date")
                    .WithFormat(ConsoleTableBuilderFormat.Alternative)
                    .ExportAndWriteLine();
        }
        internal void DisplayFlashcardsInStack(List<List<Object>> ListOfTableLines)
        {
            ConsoleTableBuilder
                    .From(ListOfTableLines)
                    .WithColumn("Id", "Front of Card", "Back of Card", "Stack Reference", "Creation Date", "Edit Date")
                    .WithFormat(ConsoleTableBuilderFormat.Alternative)
                    .ExportAndWriteLine();
        }

        internal void DisplayFlashcardStudy(List<List<Object>> ListOfTableLines)
        {
            ConsoleTableBuilder
                    .From(ListOfTableLines)
                    .WithColumn("Front of Card")
                    .WithFormat(ConsoleTableBuilderFormat.Alternative)
                    .ExportAndWriteLine();
        }

        internal void DisplayStudySessions(List<List<Object>> ListOfTableLines)
        {
            ConsoleTableBuilder
                    .From(ListOfTableLines)
                    .WithColumn("Subject", "Score", "NumberOfQuestions", "Study Date")
                    .WithFormat(ConsoleTableBuilderFormat.Alternative)
                    .ExportAndWriteLine();
        }
    }
}
