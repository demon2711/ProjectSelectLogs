namespace test.Models
{
    public class User
    {
        public User() { }

        public User(String? login, String? password) 
        {
            this.login = login;
            this.password = password;
        }

        public String? login;
        public String? password;
    }
}





