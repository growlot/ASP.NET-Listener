namespace AMSLLC.Listener.Client.Message
{
    using System;

    public class DeviceUpdateRequestMessage : BaseListenerRequestMessage
    {
        public DeviceUpdateRequestMessage(string entityCategory)
            : base(entityCategory, "Update")
        {
        }

        public string EntityKey { get; set; }
        public string Owner { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
