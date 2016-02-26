namespace Repographer.Components
{
    public interface ITrueCounter
    {
        int GetCount(params bool[] values);

        bool Any(params bool[] values);
    }
}