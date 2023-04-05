namespace Blog.Web
{
    public static class SD
    {
        public const string AdminRole = "Admin";
        public const string UserRole = "User";

        public static string PostApiBase { get; set; }

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
