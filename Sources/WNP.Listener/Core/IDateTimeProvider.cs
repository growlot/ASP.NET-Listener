﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Core
{
    public interface IDateTimeProvider
    {
        DateTime Now();
    }
}