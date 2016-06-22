using OHM.Common.Vr;
using OHM.Data;
using OHM.Interfaces;
using OHM.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OHM.Sys
{

    public interface IAPIResult
    {
        bool IsSuccess { get; }

        object Result { get; }
    }

    public sealed class APIResultFalse : IAPIResult
    {
        public APIResultFalse() {}

        public bool IsSuccess
        {
            get { return false; }
        }

        public object Result
        {
            get { return null; }
        }
    }

    public sealed class APIResultTrue : IAPIResult
    {
        private object _result;

        public APIResultTrue(object result) 
        {
            _result = result;
        }

        public bool IsSuccess
        {
            get { return true; }
        }

        public object Result
        {
            get { return _result; }
        }
    }

    public interface IAPI
    {
        IAPIResult ExecuteCommand(string key);

        IAPIResult ExecuteCommand(string key, Dictionary<String, object> arguments);

        event PropertyChangedEventHandler PropertyChanged;
    }

    public interface IOhmSystemInternal : IOhmSystem
    {
        IAPI API { get; }
    }
}
