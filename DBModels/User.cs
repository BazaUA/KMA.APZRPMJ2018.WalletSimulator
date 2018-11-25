using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using KMA.APZRPMJ2018.RequestSimulator.Tools;

namespace KMA.APZRPMJ2018.RequestSimulator.DBModels
{
    [Serializable]
    public class User
    {
        #region Const

        private const string PrivateKey =
            "<RSAKeyValue><Modulus>sr4l8EwMgPqPhRTK+dPTwGs9uYhDeUohSRL48ZDf85/5/Lo469WltKFaQvA2Msy92xq14YPPN6p6mQD7stVbinRd+hihnYoqfclYRMe+FG6jqu/QACl0N6JgwM7iKGiyzBjL1vkvcSoaqwbxD8QPHgrdNlkyP0z6Vz7j79PFEos=</Modulus><Exponent>AQAB</Exponent><P>6OkjEhjlvbDCuOl8e0Ep2zACTxkfSta8WFBmdvrinhQSowkT5xDXL0EFa/Z03XNUmjJ0xGe1aNCgG+6dDpTnSw==</P><Q>xHZTH4hXAv7uJsb/VHrcYOM5l4AyC+OxP7bhmAoGJGf4TpPxh+B0RhMxssrkc1d/72TIfRpuPbSLEqkqCSk5wQ==</Q><DP>SKFzK1CSTB4UCv/crr76Y3zMK4hlBryCDXQ9D7ta8frGeQr6puLMh9LZ8vnvJaOybUdwvFKu8pmkZDF7zrFGkw==</DP><DQ>J3ZNBAxyzds/IvLd3q4/DgcWTmQlqVW3CMFHVy7MRQvNSJtW7KAdOuYoGW2/rZtpy0BHNTnV4vcc6EaqduSdAQ==</DQ><InverseQ>4/jjapjJHdDqr5FG5a29ISgO6mRnjty6nrOisPNDi4336JdEKfAdtZvDUQoBAwKsV0oMvJ9RtPB2tS0hf5i8pA==</InverseQ><D>qcnyY/b5kbNxjasYvIQ5i3jTY2BLJ/YA9FcvXtiNw/DdGPMUiwGhrJnxEdD4yvyuBGm1CAmbV3d7icfjUBdYIe9VaZqPQ2FgYzI5DbB401+4z6Di7uKBVajLIOawlnufW4+K68T0EAFO2l9eo1RcU66W921G/pz6hObeUXt65QE=</D></RSAKeyValue>";

        private const string PubblicKey =
            "<RSAKeyValue><Modulus>sr4l8EwMgPqPhRTK+dPTwGs9uYhDeUohSRL48ZDf85/5/Lo469WltKFaQvA2Msy92xq14YPPN6p6mQD7stVbinRd+hihnYoqfclYRMe+FG6jqu/QACl0N6JgwM7iKGiyzBjL1vkvcSoaqwbxD8QPHgrdNlkyP0z6Vz7j79PFEos=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        #endregion

        #region Fields

        #endregion

        #region Properties

        public Guid Guid { get; private set; }

        private string FirstName { get; set; }

        private string LastName { get; set; }

        private string Email { get; set; }

        public string Login { get; private set; }

        private string Password { get; set; }

        private DateTime LastLoginDate { get; set; }

        public List<Request> Requests { get; private set; }

        #endregion

        #region Constructor

        public User(string firstName, string lastName, string email, string login, string password) : this()
        {
            Guid = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Login = login;
            LastLoginDate = DateTime.Now;

            SetPassword(password);
        }

        private User()
        {
            Requests = new List<Request>();
        }

        #endregion

        private void SetPassword(string password)
        {
            Password = Encrypting.Encrypt(password, PrivateKey).Result;
        }

        public bool CheckPassword(string password)
        {
            try
            {
                var res = Encrypting.Decrypt(Password, PrivateKey);
                return res.Result.Equals(password);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckPassword(User userCandidate)
        {
            try
            {
                return Password.Equals(userCandidate.Password);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName}";
        }

        public class UserEntityConfiguration : EntityTypeConfiguration<User>
        {
            public UserEntityConfiguration()
            {
                ToTable("Users");
                HasKey(s => s.Guid);

                Property(p => p.Guid)
                    .HasColumnName("Guid")
                    .IsRequired();
                Property(p => p.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired();
                Property(p => p.LastName)
                    .HasColumnName("LastName")
                    .IsRequired();
                Property(p => p.Email)
                    .HasColumnName("Email")
                    .IsRequired();
                Property(p => p.Login)
                    .HasColumnName("Login")
                    .IsRequired();
                Property(p => p.Password)
                    .HasColumnName("Password")
                    .IsRequired();
                Property(p => p.LastLoginDate)
                    .HasColumnName("LastLoginDate")
                    .IsRequired();

                HasMany(s => s.Requests)
                    .WithRequired(w => w.User)
                    .HasForeignKey(w => w.UserGuid)
                    .WillCascadeOnDelete(true);
            }
        }
    }
}