﻿using OHM.RAL;
using OHM.VAL;
using System;
using System.Collections.Generic;

namespace TimedCommand.VrTypes
{
    public class TimedAction : ValNodeAbstract, IVrType
    {
        public TimedAction(string key, string name)
            : base(key, name)
        {

        }

        protected override void RegisterCommands()
        {
            throw new NotImplementedException();
        }

        protected override void RegisterProperties()
        {
            throw new NotImplementedException();
        }

        public IList<string> GetAllowedSubVrType()
        {
            throw new NotImplementedException();
        }
    }
}
