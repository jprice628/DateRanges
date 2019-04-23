using System;
using System.Collections.Generic;
using System.Text;

namespace DateRanges
{
    internal class DifferenceOperation : SetOperationBase
    {
        internal DifferenceOperation()
        {
        }

        protected override bool DetermineOutcome(int[] setStates)
        {
            if (setStates[0] == 0)
            {
                return false;
            }
            else
            {
                return TailCount(setStates) == 0;
            }
        }

        private int TailCount(int[] setStates)
        {
            int sum = 0;
            for(int i = 1; i < setStates.Length; i++)
            {
                sum += setStates[i];
            }
            return sum;
        }
    }
}
