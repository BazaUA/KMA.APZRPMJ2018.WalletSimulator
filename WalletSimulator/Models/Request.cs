using System;

namespace KMA.APZRPMJ2018.RequestSimulator.Models
{
    public class Request
    {
        #region Fields
        private Guid _guid;
        private string _path;
        private long _numberOfCharts;
        private long _numberOfWords;
        private long _numberOfStrings;
        private DateTime _dateRequest;

        #endregion

        #region Properties
        public Guid Guid
        {
            get { return _guid; }
            private set { _guid = value; }
        }
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }
        public long NumberOfCharts
        {
            get { return _numberOfCharts; }
            private set { _numberOfCharts = value; }
        }
        public long NumberOfWords
        {
            get { return _numberOfWords; }
            private set { _numberOfWords = value; }
        }

        public long NumberOfStrings
        {
            get { return _numberOfStrings; }
            private set { _numberOfStrings = value; }
        }

        public DateTime DateRequest
        {
            get { return _dateRequest; }
            private set { _dateRequest = value; }
        }
        #endregion

        #region Constructor
        public Request(string title, long numberOfCharacters, long numberOfWords, long numberOfLines, User user) : this()
        {
            _guid = Guid.NewGuid();
            _path = title;
            _numberOfCharts = numberOfCharacters;
            _numberOfWords = numberOfWords;
            _numberOfStrings = numberOfLines;
            _dateRequest = DateTime.Now;
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
