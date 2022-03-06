using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class FlashCard
    {
        public int FlashcardId { get; set; }

        public string StackReference { get; set; }

        public string FlashcardFront { get; set; }

        public string FlashcardBack { get; set; }
    }
}
