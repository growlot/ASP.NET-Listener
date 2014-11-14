namespace AMSLLC.Listener.SOR.Alliant.Adapter.V1
{
    using System;
    using System.AddIn.Pipeline;
    using System.Collections.Generic;
    using AMSLLC.Listener.SOR.Alliant.Views;
    using AMSLLC.Listener.SOR.Contract.V1;

    public class DeviceContractToViewAdapter : Device, IDisposable
    {
        private IDeviceContract contract;
        private ContractHandle handle;

        public DeviceContractToViewAdapter(IDeviceContract contract) 
        {
            this.contract = contract;
            this.handle = new ContractHandle(this.contract);
        }

        public override int Id()
        {
            return this.contract.Id();
        }

        public override string ServiceType()
        {
            return this.contract.ServiceType();
        }

        public override string DeviceType()
        {
            return this.contract.DeviceType();
        }

        public override string Company()
        {
            return this.contract.Company();
        }

        public override string EquipmentNumber()
        {
            return this.contract.EquipmentNumber();
        }

        public override string Location()
        {
            return this.contract.Location();
        }

        public override string Barcode()
        {
            return this.contract.Barcode();
        }

        public override string FirmwareVersion()
        {
            return this.contract.FirmwareVersion();
        }

        public override string TesterId()
        {
            string result;
            this.contract.CustomFields().TryGetValue("TesterId", out result);
            return result;
        }

        public override string TestStandard()
        {
            string result;
            this.contract.CustomFields().TryGetValue("TestStandard", out result);
            return result;
        }

        public override string NewDevice()
        {
            string result;
            this.contract.CustomFields().TryGetValue("User03", out result);
            return result;
        }

        public override string LastTestDate()
        {
            string result;
            this.contract.CustomFields().TryGetValue("User04", out result);
            return result;
        }

        public override string ReasonForTest()
        {
            string result;
            this.contract.CustomFields().TryGetValue("User05", out result);
            return result;
        }

        public override string LossCompensation()
        {
            string result;
            this.contract.CustomFields().TryGetValue("User06", out result);
            return result;
        }
        
        internal IDeviceContract GetSourceContract()
        {
            return this.contract;
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.handle != null)
                {
                    this.handle.Dispose();
                }
            }
        }

    }
}
