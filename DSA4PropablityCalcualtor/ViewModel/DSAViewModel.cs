using System.Collections.ObjectModel;
using System.Linq;
using DSA4PropablityCalcualtor.Model;

namespace DSA4PropablityCalcualtor.ViewModel
{
    public class DSAViewModel
    {
        private int _eigenschaft1 = 12;
        private int _eigenschaft2 = 12;
        private int _eigenschaft3 = 12;

        public int Eigentschaft1 {
            get { return _eigenschaft1; }
            set
            {
                _eigenschaft1 = value;
                CalcualteDicePropability();
            }
        }
               
        public int Eigentschaft2
        {
            get { return _eigenschaft2; }
            set
            {
                _eigenschaft2 = value;
                CalcualteDicePropability();
            }
        }

        public int Eigentschaft3
        {
            get { return _eigenschaft3; }
            set
            {
                _eigenschaft3 = value;
                CalcualteDicePropability();
            }
        }

        public int TawMax { get; set; } = 20;

        public ObservableCollection<TalentPropablityModel> OutputList { get; set; } = new ObservableCollection<TalentPropablityModel>();

        public DSAViewModel()
        {
            CalcualteDicePropability();
        }

        private void CalcualteDicePropability()
        {
            OutputList.Clear();
            for (int taW = 0; taW <= TawMax; taW++)
            {
                var talentPropablityModel = new TalentPropablityModel
                {
                    TaW = taW,
                    Propability = CountOfValidScores(taW) / 8000m
                };

                OutputList.Add(talentPropablityModel);
            }
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
