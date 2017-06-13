using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace ProcessNote
{
    class SpeechRec
    {

        public SpeechRec()
        {
            SpeechRecognizer sr = new SpeechRecognizer();
            Choices commands = new Choices(new string[] {"start","exit" });
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(commands);
            Grammar grammar = new Grammar(gb);

            sr.LoadGrammar(grammar);
        }
    }
}
