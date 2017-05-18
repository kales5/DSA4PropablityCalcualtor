using DSA4PropablityCalcualtor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DSA4PropablityCalcualtor
{
    public sealed partial class ResultViewControl : UserControl
    {
        public ResultViewControl()
        {
            this.InitializeComponent();
        }

        public ObservableCollection<TalentPropablityModel> OutputList { get; set; }
    }
}
