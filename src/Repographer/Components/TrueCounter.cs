using System.Linq;

namespace Repographer.Components
{
    public class TrueCounter : ITrueCounter
    {
        public int GetCount(params bool[] values)
        {
            return values.Count(v => v);
        }

        public bool Any(params bool[] values)
        {
            return values.Any(c => c);
        }
    }
}