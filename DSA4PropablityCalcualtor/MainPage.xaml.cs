using DSA4PropablityCalcualtor.View;
using DSA4PropablityCalcualtor.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DSA4PropablityCalcualtor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage));
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="Common.NavigationHelper.LoadState"/>
        /// and <see cref="Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is DSAVoiceCommand)
            {
                var voiceCommand = (DSAVoiceCommand)e.Parameter;
                DSAControl.DataContext = new DSAViewModel(voiceCommand.Eigentschaft1, voiceCommand.Eigentschaft2, voiceCommand.Eigentschaft3);
               
                Pivot.SelectedItem = DSA;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
           //todo
        }

        #endregion
    }
}
