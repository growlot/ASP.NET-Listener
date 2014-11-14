namespace AMSLLC.Listener.SOR.Contract.V1
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.AddIn.Pipeline;
    using System.AddIn.Contract;


    interface ISORContract : IContract
    {

        IDeviceContract GetDevice(IDeviceContract device);

        void SetDeviceTest(IDeviceTestContract deviceTest);

        IBarcodeContract GetBarcodeBatch();
    }
}
