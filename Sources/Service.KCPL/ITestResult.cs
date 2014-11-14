using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service.KCPL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITestResult" in both code and config file together.
    [ServiceContract]
    public interface ITestResult
    {
        [OperationContract]
        void TestResult(TestResultServiceRequest request);
    }
    [DataContract]
    public class TestResultServiceRequest
    {
        [DataMember]
        public int listenerTransactionId;        // 
        [DataMember]
        public string badgeNo;                      // tmeter_test_results - eqp_no (varchar(20))
        [DataMember]
        public DateTime testDateTime;               // tmeter_test_results - test_date_start (datetime)
        [DataMember]
        public string testType;                     // tmeter_test_results - test_type (varchar(10)) (KCPL provided values: NS,NT,SS,MT - How this maps to our values: FL, LL, PF ?)
        [DataMember]
        public string testerId;                     // tmeter_test_results - tester_id (varchar(30))
        [DataMember]
        public TestResults testResults;             // complex type
    }

    [DataContract]
    public class TestResults
    {
        [DataMember]
        public Measurements asFound;                // complex type (af element)
        [DataMember]
        public Measurements asLeft;                 // complex type (al element)
        [DataMember]
        public string seriesPowerFactor;            // shouldn't this be same complex type as asFound and asLeft???
        [DataMember]
        public IList<MeterReads> meterReadsList;    // list of complex type
        [DataMember]
        public string testLocation;                 // tmeter_test_results - test_location (varchar(20)) (KCPL provided values: FL, MN, SH - do we need to map?)
    }

    [DataContract]
    public class Measurements
    {
        [DataMember]
        public string fullLoad;                     // tmeter_test_results - af/al (float) (element=S, test_type=FL)
        [DataMember]
        public string lightLoad;                    // tmeter_test_results - af/al (float) (element=S, test_type=LL)
        [DataMember]
        public string weightedAverage;              // ?
    }

    [DataContract]
    public class MeterReads
    {
        [DataMember]
        public string channel;                      // ??? (KCPL example: KWH CONSUMED, KVARH CONSUMED)
        [DataMember]
        public string reading;                      // ??? (KCPL example: 9876,         8906)
    }
}
