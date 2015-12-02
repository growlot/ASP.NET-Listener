﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Client.Message
{
    public class ChangeDeviceStatusMessage
    {
        public string EquipmentNumber { get; set; }
        public string EquipmentType { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CompanyId { get; set; }
    }
}