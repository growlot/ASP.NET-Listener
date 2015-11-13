namespace AMSLLC.Listener.Client.Message
{
    public class DeviceUpdateRequestMessage : BaseListenerRequestMessage
    {
        public DeviceUpdateRequestMessage(string entityCategory)
            : base(entityCategory, "Update")
        {
        }

        public string EntityKey { get; set; }
        public string Owner { get; set; }
    }
}
