using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using District_3_App.ExtraInfo;
using District_3_App.ProfileSocialNetworkInfoStuff.Entities;
using District_3_App.ProfileSocialNetworkInfoStuff.ProfileNetworkInfo_Repository;
using Log_In;

namespace District_3_App.LogIn
{
    public partial class SignInPage : UserControl
    {
        private UsersRepository usersRepository;
        private UserManager userManager;
        private User User { get; set; }
        public SignInPage()
        {
            InitializeComponent();
            string filePath = "./Users.xml";
            usersRepository = new UsersRepository(filePath);
            userManager = new UserManager(filePath);
        }

        private void ForgotPasswordLink_Click(object sender, RoutedEventArgs e)
        {
            SignInGrid.Children.Clear();
            var newContent = new ForgotPassword();
            Grid.SetColumn(newContent, 1);
            Grid.SetRow(newContent, 1);
            Grid.SetRowSpan(newContent, 6);
            SignInGrid.Children.Add(newContent);
        }

        private void BackToSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignInGrid.Children.Clear();
            var newContent = new SignUp();
            SignInGrid.Children.Add(newContent);
        }

        private void TogglePasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordVisibilityIcon.Source.ToString().Contains("hidePasswordIcon") && txtVisiblePassword.Visibility == Visibility.Collapsed)
            {
                txtPassword.Visibility = Visibility.Collapsed;
                txtVisiblePassword.Visibility = Visibility.Visible;
                PasswordVisibilityIcon.Source = new BitmapImage(new Uri("pack://application:,,,/images/showPasswordIcon.png"));
                txtVisiblePassword.Text = txtPassword.Password;
            }
            else
            {
                txtPassword.Visibility = Visibility.Visible;
                txtVisiblePassword.Visibility = Visibility.Collapsed;
                PasswordVisibilityIcon.Source = new BitmapImage(new Uri("pack://application:,,,/images/hidePasswordIcon.png"));
                txtPassword.Password = txtVisiblePassword.Text;
            }
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            string usernameOrEmail = txtUsernameAndEmail.Text;
            string password = txtPassword.Password;

            if (!string.IsNullOrWhiteSpace(usernameOrEmail) && !string.IsNullOrWhiteSpace(password))
            {
                User user = usersRepository.GetUserByUsernameOrEmail(usernameOrEmail);

                if (user != null && user.Password == password)
                {
                    userManager.StartOrRenewSession(user);
                    var newContent = new MainWindow();
                    newContent.Username = user.Username;
                    newContent.Show();
                    Window.GetWindow(this).Close();
                }
                else
                {
                    MessageBox.Show("Invalid username/email or password. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter both username/email and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
