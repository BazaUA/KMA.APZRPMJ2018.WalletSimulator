using System;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;

namespace KMA.APZRPMJ2018.RequestSimulator.DBModels
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class Request
    {
        #region Properties

        [DataMember] public Guid Guid { get; private set; }
        [DataMember] public string Path { get; set; }
        [DataMember] public long NumberOfChars { get; private set; }
        [DataMember] public long NumberOfWords { get; private set; }
        [DataMember] public long NumberOfStrings { get; private set; }
        [DataMember] public DateTime DateRequest { get; private set; }
        [DataMember] public User User { get; private set; }
        [DataMember] public Guid UserGuid { get; private set; }

        #endregion

        #region Constructor

        public Request(string title, long numberOfCharacters, long numberOfWords, long numberOfLines,
            User user) : this()
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