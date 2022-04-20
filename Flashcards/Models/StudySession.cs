using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class StudySession
    {
        public int Id { get; set; }
        public string StackName { get; set; }
        public int NumberOfquestions { get; set; }
        public int Score { get; set; }
    }
}
