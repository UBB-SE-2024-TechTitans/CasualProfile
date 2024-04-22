using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using District_3_App.FriendsWindow;

namespace District_3_App
{
    /// <summary>
    /// Interaction logic for UserControl6.xaml
    /// </summary>
    public partial class UserControl6 : UserControl
    {
        public UserControl6()
        {
            InitializeComponent();
        }

        private void Anita_Button_Click(object sender, RoutedEventArgs e)
        {
            CustomWindow newWindow = new CustomWindow();
            newWindow.Show();
        }
    }
}
