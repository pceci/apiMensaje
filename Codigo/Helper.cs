namespace apiMensaje.Codigo
{
    public sealed class Helper
    {
        private Helper()
        {
        }

        public static string getConnectionStringSQL
        {
            set; get;
        }
        public static string getConnectionStringSqlLite
        {
            set; get;
        }
        public static string getPathSiteWeb
        {
            set; get;
        }
        public static string getPathLog
        {
            set; get;
        }
    }
}