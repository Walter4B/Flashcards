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
                    .WithColumn("Front of Card", "Back of Card", "Creation Date", "Edit Date")
                    .WithFormat(ConsoleTableBuilderFormat.Alternative)
                    .ExportAndWriteLine();
        }
    }
}
