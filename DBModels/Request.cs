using System;
using System.Data.Entity.ModelConfiguration;

namespace KMA.APZRPMJ2018.RequestSimulator.DBModels
{
    [Serializable]
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
        
        public User User { get; private set; }
        
        public Guid UserGuid { get; private set; }

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
            User = user;
            UserGuid = user.Guid;

        }
        private Request()
        {
        }
        #endregion
        public override string ToString()
        {
            return Path;
        }
        
        public class RequestEntityConfiguration : EntityTypeConfiguration<Request>
        {
            public RequestEntityConfiguration()
            {
                ToTable("Requests");
                HasKey(s => s.Guid);

                Property(p => p.Guid)
                    .HasColumnName("Guid")
                    .IsRequired();
                Property(p => p.Path)
                    .HasColumnName("Path")
                    .IsRequired();
                Property(s => s.NumberOfChars)
                    .HasColumnName("NumberOfChars")
                    .IsRequired();
                Property(s => s.NumberOfWords)
                    .HasColumnName("NumberOfWords")
                    .IsRequired();
                Property(s => s.NumberOfStrings)
                    .HasColumnName("NumberOfStrings")
                    .IsRequired();
                Property(s => s.DateRequest)
                    .HasColumnName("DateRequest")
                    .IsRequired();
                
               
            }
        }
        
        public void DeleteDatabaseValues()
        {
            User = null;
        }
        
    }   
}
