using System.Text.RegularExpressions;

namespace KMA.APZRPMJ2018.RequestSimulator.Tools
{


    public class WordsCount
    {

        public WordsCount()
        {
            NumberOfLines = 0;
            NumberOfWords = 0;
            NumberOfCharacters = 0;
        }

        public long NumberOfWords { get; private set; }

        public long NumberOfLines { get; private set; }

        public long NumberOfCharacters { get; private set; }

        public void LineProcessing(string line)
        {
            NumberOfLines += line.Split('\n').Length;
            NumberOfWords += Regex.Matches(line, @"\b\w+\b").Count;
            NumberOfCharacters += line.Length;
        }
    }
}
