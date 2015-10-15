using System;

namespace ODataService.Actions
{
    public class ElectricMeterActionContainer
    {
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
    }
}