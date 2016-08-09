
namespace OHM.SYS
{

    public interface IAPIResult
    {
        bool IsSuccess { get; }

        object Result { get; }
    }
}
