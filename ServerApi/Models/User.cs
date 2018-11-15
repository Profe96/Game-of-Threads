namespace ServerApi.Models
{
    public class User
    {
        public string name {get; set;}
        public string email { get; set; }
        public string authToken { get; set; }

        public bool isAuth
        {
            get { return (authToken != null) ? true : false; }
            set { isAuth = value; }
        }
    }
}