namespace BookStore.Admin.Validation
{
    public class RegexPatterns
    {
        public static string email = "^[a-zA-Z0-9]+[.+_-]{0,1}[a-z0-9]+[@][a-zA-Z0-9]+[.][a-z]{2,3}([.][a-z]{2}){0,1}$";
        public static string password = "[A-Z a-z 0-9]{8,}";
        public static string firstName= "^[A-Z][a-z]{3,}?";
        public static string lastName= "^[A-Z][a-z]{3,}?";
    }
}
