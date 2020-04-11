namespace _24hplusdotnetcore.Common
{
    public static class Message
    {
        public static string SUCCESS = "";
        public static string LOGIN_SUCCESS = "";
        public static string IS_LOGGED_IN_ORTHER_DEVICE = "Your account has already logged in another device";
        public static string UNAUTHORIZED = "";
        public static string ERROR = "";
        internal static string VERSION_IS_OLD = "Your application version is too old";
    }
    public enum ResponseCode : int
    {
        SUCCESS = 1,
        ERROR,
        IS_LOGGED_IN_ORTHER_DEVICE,
        UNAUTHORIZED
    }
}