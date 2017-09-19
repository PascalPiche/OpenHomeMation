﻿using OHM.Nodes.Properties;

namespace ZWaveLib.Data
{
    public class ZWaveValueIdNodeProperty : NodeProperty
    {
        public ZWaveValueIdNodeProperty(string key, string name) 
            : base(key, name, typeof(OpenZWaveDotNet.ZWValueID), true, "")
        { }

        internal bool InternalSetValue()
        {
            return false;
        }
    }
}
