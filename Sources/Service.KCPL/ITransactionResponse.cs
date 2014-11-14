using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service.KCPL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITransactionResponse" in both code and config file together.
    [ServiceContract]
    public interface ITransactionResponse
    {
        [OperationContract]
        void TransactionResponse(TransactionResponseServiceRequest reqeust);
    }

    [DataContract]
    public class TransactionResponseServiceRequest
    {
        [DataMember (IsRequired=true)]
        public int listenerTransactionId;           // 
        [DataMember(IsRequired = true, EmitDefaultValue = false)]
        public string status;                       // tmeter_test_results - eqp_no (varchar(20))
        [DataMember]
        public string message;                      // tmeter_test_results - eqp_no (varchar(20))
        [DataMember]
        public string debugInfo;                      // tmeter_test_results - eqp_no (varchar(20))
    }

}
