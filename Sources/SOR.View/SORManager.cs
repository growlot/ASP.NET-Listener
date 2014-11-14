namespace AMSLLC.Listener.SOR.Views
{
    using System;

    public abstract class SORManager
    {
        public abstract Device GetDevice(Device device);

        public abstract void SetDeviceTest(DeviceTest deviceTest);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "False positive. This is acutally a method.")]
        public abstract Barcode GetBarcodeBatch();
    }
}
