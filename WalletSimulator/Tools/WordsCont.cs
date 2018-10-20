using System;
using System.Text.RegularExpressions;

namespace KMA.APZRPMJ2018.RequestSimulator.Tools
{
    

    public class WordsCont 
    {
        private string _text;
        private long _numberOfWords;
        private long _numberOfLines;
        private long _numberOfCharacters;
        public WordsCont(string text)
        {
            this._text = text;
            init();
        }

        private void init()
        {
            _numberOfLines = _text.Split('\n').Length;
            _numberOfWords = Regex.Matches(_text, @"\b\w+\b").Count;
            _numberOfCharacters = _text.Length;
        }

        public long NumberOfWords
        {
            get { return _numberOfWords; }
            private set { _numberOfWords = value; }
        }

        public long NumberOfLines
        {
            get { return _numberOfLines; }
            private set { _numberOfLines = value; }
        }
        public long NumberOfCharacters
        {
            get { return _numberOfCharacters; }
            private set { _numberOfCharacters = value; }
        }


    }
}
