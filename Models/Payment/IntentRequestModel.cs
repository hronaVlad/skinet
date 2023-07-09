namespace Models.Payment
{
    public class IntentRequestModel
    {
        public string? IntentId {get; set;} 
        public long Total {get; set;}
    }
}