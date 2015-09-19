namespace AMSLLC.Listener.Communication
{
    public class TransactionRequestMessage
    {
        public int SourceApplicationId { get; set; }
        public int DestinationApplicationId { get; set; }
        public string OperationKey { get; set; }
    }
}
