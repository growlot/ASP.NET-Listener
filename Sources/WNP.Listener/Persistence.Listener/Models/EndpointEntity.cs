namespace AMSLLC.Listener.Persistence.Listener
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [MetadataType(typeof(EndpointMetadata))]
    public partial class EndpointEntity
    {
        public virtual ProtocolTypeEntity ProtocolType { get; set; }
        public virtual EndpointTriggerTypeEntity EndpointTriggerType { get; set; }

        internal sealed class EndpointMetadata
        {
            [ForeignKey("ProtocolType")]
            public int ProtocolTypeId { get; set; }

            [ForeignKey("EndpointTriggerType")]
            public int EndpointTriggerTypeId { get; set; }
        }
    }
}
