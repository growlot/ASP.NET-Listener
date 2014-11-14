namespace AMSLLC.Listener.SOR.Alliant.Adapter.V1
{
    using System;
    using System.AddIn.Pipeline;
    using System.Collections.Generic;
    using AMSLLC.Listener.SOR.Alliant.Views;
    using AMSLLC.Listener.SOR.Contract.V1;

    public class DeviceViewToContractAdapter : ContractBase, IDeviceContract
    {
        private Device view;

        public DeviceViewToContractAdapter(Device view)
        {
            this.view = view;
        }

        public virtual int Id()
        {
            return this.view.Id();
        }

        public virtual string ServiceType()
        {
            return this.view.ServiceType();
        }

        public virtual string DeviceType()
        {
            return this.view.DeviceType();
        }

        public virtual string Company()
        {
            return this.view.Company();
        }

        public virtual string EquipmentNumber()
        {
            return this.view.EquipmentNumber();
        }

        public virtual string Location()
        {
            return this.view.Location();
        }

        public virtual string Barcode()
        {
            return this.view.Barcode();
        }

        public virtual string FirmwareVersion()
        {
            return this.view.FirmwareVersion();
        }

        public virtual IDictionary<string, string> CustomFields()
        {
            IDictionary<string, string> customFields = new Dictionary<string, string>();
            customFields.Add("TesterId", this.view.TesterId());
            customFields.Add("TestStandard", this.view.TestStandard());
            customFields.Add("User05", this.view.ReasonForTest());
            customFields.Add("User04", this.view.LastTestDate());
            customFields.Add("User03", this.view.NewDevice());
            customFields.Add("User06", this.view.LossCompensation());
            return customFields;
        }

        internal Device GetSourceView()
        {
            return this.view;
        }

    }
}
