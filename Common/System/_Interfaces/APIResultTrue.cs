
namespace OHM.SYS
{
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
}
