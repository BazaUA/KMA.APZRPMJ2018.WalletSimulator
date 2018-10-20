using System;

namespace KMA.APZRPMJ2018.RequestSimulator.Models
{
    public class Request
    {
        #region Fields

        #endregion

        #region Properties
        public Guid Guid { get; private set; }

        public string Path { get; set; }

        public long NumberOfChars { get; private set; }

        public long NumberOfWords { get; private set; }

        public long NumberOfStrings { get; private set; }

        public DateTime DateRequest { get; private set; }

        #endregion

        #region Constructor
        public Request(string title, long numberOfCharacters, long numberOfWords, long numberOfLines, User user) : this()
        {
            Guid = Guid.NewGuid();
            Path = title;
            NumberOfChars = numberOfCharacters;
            NumberOfWords = numberOfWords;
            NumberOfStrings = numberOfLines;
            DateRequest = DateTime.Now;
            user.Requests.Add(this);
        }
        private Request()
        {
        }
        #endregion
        public override string ToString()
        {
            return Path;
        }
    }   
}
