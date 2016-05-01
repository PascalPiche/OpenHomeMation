using OHM.Data;
using OHM.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Common.Vr
{
    public interface IVrManager
    {

        bool Init(IDataStore data, IOhmSystemInternal system);

        bool RegisterVrType(string key, IVrType vrType/*, IOhmSystemInternal system*/);


    }
}
