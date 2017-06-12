using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA4PropablityCalcualtor.View
{
    public class DSAVoiceCommand
    {
        public DSAVoiceCommand(string eigentschaft1, string eigentschaft2, string eigentschaft3)
        {
            int e1, e2, e3;
            if(int.TryParse(eigentschaft1, out e1))
            {
                Eigentschaft1 = e1;
            }
            else
            {
                Eigentschaft1 = 12;
            }

            if (int.TryParse(eigentschaft2, out e2))
            {
                Eigentschaft2 = e2;
            }
            else
            {
                Eigentschaft2 = 12;
            }

            if (int.TryParse(eigentschaft3, out e3))
            {
                Eigentschaft3 = e3;
            }
            else
            {
                Eigentschaft3 = 12;
            }
        }

        public int Eigentschaft1 { get; set; }

        public int Eigentschaft2 { get; set; }

        public int Eigentschaft3 { get; set; }
    }
}
