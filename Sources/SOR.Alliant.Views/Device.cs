namespace AMSLLC.Listener.SOR.Alliant.Views
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

        public abstract string TesterId(); // testerID

        public abstract string TestStandard(); // testStandard

        public abstract string ReasonForTest(); // reasonForTest

        public abstract string LastTestDate(); // lastTestDate

        public abstract string NewDevice(); // newDevice

        public abstract string LossCompensation(); // lossCompensation
    }
}
