namespace DiscordHello.Models.Context
{
    public class Message
    {
        public string Server { get; set; }
        public string Content { get; set; }
        public int TalonID { get; set; }
        public Talon MasterTalon { get; set; }
    }
}
