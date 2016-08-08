using OHM.Nodes.Properties;
using System;
using WUnderground.Api.Data;
using WUnderground.Commands;

namespace WUnderground.Nodes
{
    public class StationConditionNode : WUndergroundNodeAbstract
    {
        #region Public Ctor

        public StationConditionNode(string keyId, string name)
            : base(keyId, name)
        { }

        #endregion

        #region Protected Methods

        protected override void RegisterCommands()
        {
            this.RegisterCommand(new RefreshCondition("refreshCondition", "Refresh", ""));
        }

        protected override void RegisterProperties()
        {
            this.RegisterProperty(new NodeProperty("DewPoint_C", "Dew Point Celsius", typeof(Double), true));
            this.RegisterProperty(new NodeProperty("DewPoint_F", "Dew Point F", typeof(Double), true));
            this.RegisterProperty(new NodeProperty("FeelsLike_C", "Feels Like Celsius", typeof(Double), true));
            this.RegisterProperty(new NodeProperty("FeelsLike_F", "Feels Like F", typeof(Double), true));
            this.RegisterProperty(new NodeProperty("HeatIndex_C", "Heat Index Celsius", typeof(String), true));
            this.RegisterProperty(new NodeProperty("HeatIndex_F", "Heat Index F", typeof(String), true));
            this.RegisterProperty(new NodeProperty("ImageUrl", "Image Url", typeof(String), true));
            this.RegisterProperty(new NodeProperty("Pressure_in", "Pressure inch", typeof(Double), true));
            this.RegisterProperty(new NodeProperty("Pressure_mb", "Pressure millibar", typeof(Double), true));
            this.RegisterProperty(new NodeProperty("PressureTrend", "Pressure Trend", typeof(String), true));
            this.RegisterProperty(new NodeProperty("RelativeHumidity", "Relative Humidity", typeof(String), true));
            this.RegisterProperty(new NodeProperty("Temperature_C", "Temperature Celsius", typeof(Double), true));
            this.RegisterProperty(new NodeProperty("Temperature_F", "Temperature F", typeof(Double), true));
            this.RegisterProperty(new NodeProperty("TermOfService", "Term Of Service", typeof(String), true));
            this.RegisterProperty(new NodeProperty("StationId", "Station Id", typeof(String), true));
            this.RegisterProperty(new NodeProperty("UV", "UV", typeof(Int64), true));
            this.RegisterProperty(new NodeProperty("Version", "Version", typeof(String), true));
            this.RegisterProperty(new NodeProperty("Visibility_Km", "Visibility Kilometers", typeof(Double), true));
            this.RegisterProperty(new NodeProperty("Visibility_Mi", "Visibility Miles", typeof(Double), true));
            this.RegisterProperty(new NodeProperty("Weather", "Weather", typeof(String), true));
            this.RegisterProperty(new NodeProperty("WindDegrees", "Wind Degrees", typeof(Int64), true));
            this.RegisterProperty(new NodeProperty("WindDirection", "Wind Direction", typeof(String), true));
            this.RegisterProperty(new NodeProperty("WindGust_Kph", "WindGust Kph", typeof(Double), true));
            this.RegisterProperty(new NodeProperty("WindGust_Mph", "WindGust Mph", typeof(Double), true));
            this.RegisterProperty(new NodeProperty("WindSpeed_Kph", "WindSpeed Kph", typeof(Double), true));
            this.RegisterProperty(new NodeProperty("WindSpeed_Mph", "WindSpeed Mph", typeof(Double), true));
        }

        #endregion

        #region Internal Methods

        internal bool refresh()
        {
            return ((StationNode)this.Parent).GetCondition(this);
        }

        internal bool update(WUndergroundConditionsResponse data)
        {
            this.UpdateProperty("DewPoint_C",       data.DewPoint_C);
            this.UpdateProperty("DewPoint_F",       data.DewPoint_F);
            this.UpdateProperty("FeelsLike_C",      data.FeelsLike_C);
            this.UpdateProperty("FeelsLike_F",      data.FeelsLike_F);
            this.UpdateProperty("HeatIndex_C",      data.HeatIndex_C);
            this.UpdateProperty("HeatIndex_F",      data.HeatIndex_F);
            this.UpdateProperty("ImageUrl",         data.ImageUrl);
            this.UpdateProperty("Pressure_in",      data.Pressure_in);
            this.UpdateProperty("Pressure_mb",      data.Pressure_mb);
            this.UpdateProperty("PressureTrend",    data.PressureTrend);
            this.UpdateProperty("RelativeHumidity", data.RelativeHumidity);
            this.UpdateProperty("StationId",        data.StationId);
            this.UpdateProperty("Temperature_C",    data.Temperature_C);
            this.UpdateProperty("Temperature_F",    data.Temperature_F);
            this.UpdateProperty("TermOfService",    data.TermOfService);
            this.UpdateProperty("UV",               data.UV);
            this.UpdateProperty("Version",          data.Version);
            this.UpdateProperty("Visibility_Km",    data.Visibility_Km);
            this.UpdateProperty("Visibility_Mi",    data.Visibility_Mi);
            this.UpdateProperty("Weather",          data.Weather);
            this.UpdateProperty("WindDegrees",      data.WindDegrees);
            this.UpdateProperty("WindDirection",    data.WindDirection);
            this.UpdateProperty("WindGust_Kph",     data.WindGust_Kph);
            this.UpdateProperty("WindGust_Mph",     data.WindGust_Mph);
            this.UpdateProperty("WindSpeed_Kph",    data.WindSpeed_Kph);
            this.UpdateProperty("WindSpeed_Mph",    data.WindSpeed_Mph);
                
            return true;
        }

        #endregion
    }
}
