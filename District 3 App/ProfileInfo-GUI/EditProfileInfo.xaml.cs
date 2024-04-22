using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
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
using System.Xml.Linq;

namespace District_3_App.ProfileInfo_GUI
{
    /// <summary>
    /// Interaction logic for EditProfileInfo.xaml
    /// </summary>
    public partial class EditProfileInfo : UserControl
    {
        private ProfileInfoDisplay profileInfoDisplay;

        /*public EditProfileInfo()
        {
            InitializeComponent();
        }*/

        public EditProfileInfo(ProfileInfoDisplay profileInfoDisplay)
        {
            InitializeComponent();
            this.profileInfoDisplay = profileInfoDisplay;

            EmailTextBox.Text = profileInfoDisplay.TextBlockEmail.Text;
            PhoneNumberTextBox.Text = profileInfoDisplay.TextBlockPhoneNumber.Text;
            DatePickerBirthDate.SelectedDate = DateTime.ParseExact(profileInfoDisplay.TextBlockDateOfBirth.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            UsernameTextBox.Text = profileInfoDisplay.TextBlockUsername.Text;
            NameTextBox.Text = profileInfoDisplay.TextBlockName.Text;
            WorkTextBox.Text = profileInfoDisplay.TextBlockWork.Text;
            PositionTextBox.Text = profileInfoDisplay.TextBlockWorkPosition.Text;
            DatePickerWorkStartDate.SelectedDate = DateTime.ParseExact(profileInfoDisplay.TextBlockWorkStartDate.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            DatePickerWorkEndDate.SelectedDate = DateTime.ParseExact(profileInfoDisplay.TextBlockWorkEndDate.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            WorkLocationComboBox.SelectedItem = WorkLocationComboBox.Items.OfType<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == profileInfoDisplay.TextBlockWorkLocation.Text);
            DescriptionTextBox.Text = profileInfoDisplay.TextBlockDescription.Text;
            EducationComboBox.SelectedItem = EducationComboBox.Items.OfType<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == profileInfoDisplay.TextBlockEducation.Text);
            EducationLevelTextBox.Text = profileInfoDisplay.TextBlockEducationLevel.Text;
            DatePickerEducationStartDate.SelectedDate = DateTime.ParseExact(profileInfoDisplay.TextBlockEducationStartDate.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            DatePickerEducationEndDate.SelectedDate = DateTime.ParseExact(profileInfoDisplay.TextBlockEducationEndDate.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            EducationLocationComboBox.SelectedItem = EducationLocationComboBox.Items.OfType<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == profileInfoDisplay.TextBlockEducationLocation.Text);
            HobbiesTextBox.Text = profileInfoDisplay.TextBlockHobbies.Text;
            MusicTextBox.Text = profileInfoDisplay.TextBlockMusic.Text;
            PlacesComboBox.SelectedItem = PlacesComboBox.Items.OfType<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == profileInfoDisplay.TextBlockPlaces.Text);
        }

        private void SaveChanges_Button(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string phoneNumber = PhoneNumberTextBox.Text;
            string dateOfBirth = DatePickerBirthDate.SelectedDate?.ToString("dd.MM.yyyy");
            string username = UsernameTextBox.Text;
            string name = NameTextBox.Text;
            string work = WorkTextBox.Text;
            string position = PositionTextBox.Text;
            string workStartDate = DatePickerWorkStartDate.SelectedDate?.ToString("dd.MM.yyyy");
            string workEndDate = DatePickerWorkEndDate.SelectedDate?.ToString("dd.MM.yyyy");
            string workLocation = (WorkLocationComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string description = DescriptionTextBox.Text;
            string education = (EducationComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string educationLevel = EducationLevelTextBox.Text;
            string educationStartDate = DatePickerEducationStartDate.SelectedDate?.ToString("dd.MM.yyyy");
            string educationEndDate = DatePickerEducationEndDate.SelectedDate?.ToString("dd.MM.yyyy");
            string educationLocation = (EducationLocationComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string hobbies = HobbiesTextBox.Text;
            string music = MusicTextBox.Text;
            string places = (PlacesComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Redirect to ProfileInfoDisplay page

            // Clear the EditProfileInfoStackPanel
            EditProfileInfoStackPanel.Children.Clear();

            // Add a new ProfileInfoDisplay instance to display the updated profile information
            var newProfileInfoDisplay = new ProfileInfoDisplay();
            newProfileInfoDisplay.UpdateProfileInfo(
                email, phoneNumber, dateOfBirth, name,
                username, education, educationLevel,
                educationStartDate, educationEndDate,
                educationLocation, hobbies, music, places,
                work, position, workStartDate,
                workEndDate, workLocation, description);

            var newMainWindow = new MainWindow();
            newMainWindow.UpdateAboutYou(
            email, phoneNumber, dateOfBirth, name,
            education, educationLevel,
            educationStartDate, educationEndDate,
            educationLocation, hobbies, music, places,
            work, position, workStartDate,
            workEndDate, workLocation, description);

            EditProfileInfoStackPanel.Children.Add(newProfileInfoDisplay);
        }

        private void Cancel_Button(object sender, RoutedEventArgs e)
        {
            var newContent = new ProfileInfoDisplay();

            EditProfileInfoStackPanel.Children.Clear();
            EditProfileInfoStackPanel.Children.Add(newContent);
        }
    }
}