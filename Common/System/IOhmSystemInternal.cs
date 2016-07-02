using OHM.Common.Vr;
using OHM.Data;
using OHM.Interfaces;
using OHM.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OHM.Sys
{
    public interface IOhmSystemInternal : IOhmSystem
    {
        IAPI API { get; }
    }
}
