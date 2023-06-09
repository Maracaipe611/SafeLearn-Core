namespace SafeLearn.Configurations
{
    public class TwilioConfiguration : ITwilioConfiguration
    {
        public string AccountSID { get; set; }
        public string Auth { get; set; }
        public string Number { get; set; }
    }
}
