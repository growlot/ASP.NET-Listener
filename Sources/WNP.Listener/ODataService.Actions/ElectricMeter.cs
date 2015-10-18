using System;
using AMSLLC.Listener.Persistence.Metadata;

namespace AMSLLC.Listener.ODataService.Actions
{
    public class ElectricMeter : IActionsContainer
    {
        public string GetTableName() => DBMetadata.EqpMeter.RealTableName;

        /*
         * EquipmentNumber - string - electric meter equipment number used as a key parameter to select specific meter
         * SiteId - int - site where equipment will be installed
         * CircuitIndex - int - circuit index in the site 
         * UserName - string - user name of the users who did the installation
         * InstallationDate - DateTime - time when installation was performed 

         * ~/ElectricMeters('1')/Listener.ElectricMeter.Install
        */
        public void Install(string equipmentNumber, int siteId, int circuitIndex, string userName, DateTime installationDate)
        {
            // link to command layer
        }

        public string Test(string mystr)
        {
            return mystr;
        }
    }
}