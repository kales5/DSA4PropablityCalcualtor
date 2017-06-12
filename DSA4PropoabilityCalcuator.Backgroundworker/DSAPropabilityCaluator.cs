using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA4PropoabilityCalcuator.Backgroundworker
{
    internal class DSAPropabilityCaluator
    {
        private int _eigenschaft1;
        private int _eigenschaft2;
        private int _eigenschaft3;

        public DSAPropabilityCaluator(int e1, int e2, int e3)
        {
            _eigenschaft1 = e1;
            _eigenschaft2 = e2;
            _eigenschaft3 = e3;
        }

        public double CalcualteDicePropability(int taw)
        {
            return CountOfValidScores(taw) / 8000.0;
        }

        private int CountOfValidScores(int taw)
        {
            int numberOfValid = 0;
            for (int w1 = 1; w1 <= 20; w1++)
            {
                for (int w2 = 1; w2 <= 20; w2++)
                {
                    for (int w3 = 1; w3 <= 20; w3++)
                    {
                        if (CombinationIsValid(w1, w2, w3, taw))
                            numberOfValid++;
                    }
                }
            }
            return numberOfValid;
        }

        private bool CombinationIsValid(int w1, int w2, int w3, int taw)
        {
            var ws = new[] { w1, w2, w3 };
            if (ws.Count(w => w == 1) >= 2)
                return true;
            if (ws.Count(w => w == 20) >= 2)
                return false;

            if (w1 > _eigenschaft1)
                taw -= (w1 - _eigenschaft1);
            if (w2 > _eigenschaft2)
                taw -= (w2 - _eigenschaft2);
            if (w3 > _eigenschaft3)
                taw -= (w3 - _eigenschaft3);

            return taw >= 0;

        }
    }
}
