namespace DiscordHello.Models
{
    public class Settings
    {
        public static string BufferPath = "S:\\repos\\Betterdiscord";
        public static string InputJson = BufferPath + "\\input_buffer.json";
        public static Type CurrentSender = typeof(FileBetterDiscordSender);
    }
}
