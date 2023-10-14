namespace Shopping_Cart_Web_Application_V1._0.Models
{
    public class LoginViewModel
    {
        public bool IsNullUsernameOrPassword { get; set; }
        public bool WrongUsernameOrPassword { get; set; }

        public LoginViewModel()
        {
            IsNullUsernameOrPassword = false;
            WrongUsernameOrPassword = false;
        }
    }
}
