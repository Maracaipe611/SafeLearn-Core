namespace SafeLearn.Configurations
{
    public interface ITwilioConfiguration
    {
        public string AccountSID { get; set; }
        public string Auth { get; set; }
        public string Number { get; set; }
    }
}
