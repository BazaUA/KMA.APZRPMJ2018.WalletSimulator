using System;
using System.Text.RegularExpressions;

namespace KMA.APZRPMJ2018.RequestSimulator.Tools
{
    

    public class WordsCont 
    {
        private readonly string _text;

        public WordsCont(string text)
        {
            this._text = text;
            Init();
        }

        private void Init()
        {
            NumberOfLines = _text.Split('\n').Length;
            NumberOfWords = Regex.Matches(_text, @"\b\w+\b").Count;
            NumberOfCharacters = _text.Length;
        }

        public long NumberOfWords { get; private set; }

        public long NumberOfLines { get; private set; }

        public long NumberOfCharacters { get; private set; }
    }
}
