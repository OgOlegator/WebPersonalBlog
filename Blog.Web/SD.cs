namespace Blog.Web
{
    public static class SD
    {
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
