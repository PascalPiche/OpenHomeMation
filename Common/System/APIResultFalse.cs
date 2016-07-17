
namespace OHM.SYS
{
    public sealed class APIResultFalse : IAPIResult
    {
        public APIResultFalse() { }

        public bool IsSuccess
        {
            get { return false; }
        }

        public object Result
        {
            get { return null; }
        }
    }
}
