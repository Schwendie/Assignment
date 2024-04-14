namespace SimpleSlotMachine.Models
{
    public class UserSession
    {
        public string SessionId { get; set; }
        public int StartingCredits { get; set; }
        public int CurrentCredits { get; set; }
    }
}
