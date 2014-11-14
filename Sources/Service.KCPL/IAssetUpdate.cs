using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service.KCPL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAssetUpdate" in both code and config file together.
    [ServiceContract]
    public interface IAssetUpdate
    {
        [OperationContract]
        void AssetUpdate(AssetUpdateServiceRequest request);
    }

    [DataContract]
    public class AssetUpdateServiceRequest
    {
        [DataMember]
        public int listenerTransactionId;        // 
        [DataMember]
        public string badgeNo;                      // teqp_meter - eqp_no (varchar(20))
        [DataMember]
        public DateTime statusDateTime;             // teqp_meter - status_date (datetime)
        [DataMember]
        public string status;                       // teqp_meter - eqp_status (varchar(20)) (KCPL provided values: RETIRED, ACTIVE, REPAIR - do we need to map?)
        [DataMember]
        public string retirementReasonCode;         // teqp_meter - retire_code (varchar(3)) (KCPL provided values: BROKEN, FAILED, TBD... - do we need to map?) (how it relates to eqp_status?)
        [DataMember]
        public string comment;                      // tcomment - comments (varchar(250)) (how to track if comment is related to specific situation?)
    }

}
