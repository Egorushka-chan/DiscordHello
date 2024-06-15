namespace DiscordHello.Models
{
    public static class Extensions
    {
        public static bool IsEven(this int value)
        {
            return (value & 1) == 1;
        }
    }
}
