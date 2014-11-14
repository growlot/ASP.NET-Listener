using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service.KCPL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAssetLoad" in both code and config file together.
    [ServiceContract]
    public interface IAssetLoad
    {
        [OperationContract]
        void AssetLoad(AssetLoadServiceRequest request);
    }

    [DataContract]
    public class AssetLoadServiceRequest
    {
        [DataMember]
        public int listenerTransactionId;           // 
        [DataMember]
        public string assetType;                    // ?? (KCPL example value: AMI-1PH-KWH)
        [DataMember]
        public string boStatus;                     // ?? teqp_meter - shop_status (varchar(20)) (KCPL example value: INSTORE - do we need to map?)
        [DataMember]
        public string specification;                // ?? (KCPL example value: 1145)
        [DataMember]
        public string configuration;                // ?? (KCPL example value: AMIAL)
        [DataMember]
        public IList<AnyType> customElements;       // clarify with KCPL
        [DataMember]
        public FormattedElements formattedElements; // complex type

    }
    [DataContract]
    public class AnyType{ }
    
    [DataContract]
    public class FormattedElements
    {
        [DataMember]
        public string manufacturer;                 // ?? (KCPL example value: 1)
        [DataMember]
        public string model;                        // teqp_meter - model_no (varchar(30)) (KCPL example value: FOCUSAL)
        [DataMember]
        public string numberOfDials;                // ?? teqp_meter - kwh_dials or kw_dials (KCPL example value: 5)
        [DataMember]
        public string badgeNo;                      // teqp_meter - eqp_no (varchar(20))
        [DataMember]
        public string serialNo;                     // teqp_meter - serial_no (varchar(20))
        [DataMember]
        public string itronId;                      // ??
        [DataMember]
        public string hhfId;                        // ??
        [DataMember]
        public string endpointId;                   // ??
        [DataMember]
        public string metrologyFirmware;            // teqp_meter - firmware_rev01 (varchar(20))
        [DataMember]
        public string commModuleFirmware;           // teqp_meter - firmware_rev02 (varchar(20))
        [DataMember]
        public string zigbeeFirmware;               // teqp_meter - firmware_rev03 (varchar(20))
        [DataMember]
        public string dcw;                          // teqp_meter - firmware_rev04 (varchar(20)) (KCPL example value: 1B0D.05.10)
        [DataMember]
        public string meterForm;                    // teqp_meter - form
        [DataMember]
        public string kh;                           // teqp_meter - kh
        [DataMember]
        public string meterBase;                    // teqp_meter - base
        [DataMember]
        public string meterClass;                   // ?? teqp_meter - aep_code
        [DataMember]
        public string wires;                        // teqp_meter - wire
        [DataMember]
        public string voltage;                      // ?? teqp_meter - test_volts (KCPL example value: 120)
        [DataMember]
        public string phase;                        // teqp_meter - phase
        [DataMember]
        public string testAmps;                     // teqp_meter - test_amps
        [DataMember]
        public string ownershipTerritory;           // teqp_meter - ownter -> towner -  owner_desc
        [DataMember]
        public string disconnectSwitchStatus;       // ??
        [DataMember]
        public DateTime meterReceiptDate;           // ?? teqp_meter - purchase_date
        [DataMember]
        public DateTime warrantyExpirationDate;     // ?? teqp_meter - purchase_date + warranty_period
        [DataMember]
        public string warrantyDetail;               // ??
        [DataMember]
        public string vendor;                       // ?? teqp_meter - mfr
        [DataMember]
        public string vendorPartNo;                 // ??
        [DataMember]
        public string purchaseOrderNo;              // ??
    }
}
