namespace DiscordHello.Models.Context
{
    public class Talon
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string ServerID { get; set; }
        public string OwnerName { get; set; }
        public int Uses { get; set; }
        public List<Message> SentMessages { get; set; }
    }
}
