namespace _24hplusdotnetcore.Common
{
    public static class Message
    {
        public static string SUCCESS = "";
        public static string LOGIN_SUCCESS = "";
        public static string IS_LOGGED_IN_ORTHER_DEVICE = "";
        public static string UNAUTHORIZED = "";
        public static string ERROR = "";
    }
    public enum ResponseCode : int
    {
        SUCCESS,
        IS_LOGGED_IN_ORTHER_DEVICE, 
        UNAUTHORIZED,
        ERROR
    }
}