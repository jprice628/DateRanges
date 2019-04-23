using System.Linq;

namespace DateRanges
{
    internal class IntersectOperation : SetOperationBase
    {
        internal IntersectOperation()
        {
        }

        protected override bool DetermineOutcome(int[] setStates)
        {
            return setStates.All(x => x > 0);
        }
    }
}
