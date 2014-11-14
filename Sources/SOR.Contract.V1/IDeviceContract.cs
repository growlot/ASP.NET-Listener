namespace AMSLLC.Listener.SOR.Contract.V1
{
    using System;
    using System.AddIn.Contract;
    using System.Collections.Generic;

    public interface IDeviceContract : IContract
    {
        int Id(); // new field

        string ServiceType(); // serviceType

        string DeviceType(); // deviceType

        string Company(); // company

        string EquipmentNumber(); // meterNumber

        string Location(); // location

        string Barcode(); // classificationCode

        string FirmwareVersion(); // commModFirmwareVersion

        IDictionary<string, string> CustomFields(); 
        // commModVersion
        // testerID
        // testStandard
        // reasonForTest
        // lastTestDate
        // newDevice
        // lossCompensation
    }
}
