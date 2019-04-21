using System.Linq;

namespace DateRanges
{
    internal class UnionOperation : SetOperationBase
    {
        internal UnionOperation()
        {
        }

        protected override bool DetermineOutcome(int[] setStates)
        {
            return setStates.Any(x => x > 0);
        }
    }
}
