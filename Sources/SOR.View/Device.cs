namespace AMSLLC.Listener.SOR.Views
{
    using System;
    using System.Collections.Generic;

    public abstract class Device
    {
        public abstract int Id(); // new field

        public abstract string ServiceType(); // serviceType

        public abstract string DeviceType(); // deviceType

        public abstract string Company(); // company

        public abstract string EquipmentNumber(); // meterNumber

        public abstract string Location(); // location

        public abstract string Barcode(); // classificationCode

        public abstract string FirmwareVersion(); // commModFirmwareVersion

        public abstract IDictionary<string, string> CustomFields(); 
        // commModVersion
        // testerID
        // testStandard
        // reasonForTest
        // lastTestDate
        // newDevice
        // lossCompensation
    }
}
