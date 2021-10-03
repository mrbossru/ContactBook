using ContactBook.Helpers;
using ContactBook.Models;
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
using System.Windows.Shapes;

namespace ContactBook
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Contact _contact;
        public Contact contact
        {
            get
            {
                return _contact;
            }
            set
            {
                _contact = value;
                NameTextBox.Text = _contact.Name;
                SurnameTextBox.Text = _contact.Surname;
                CompanyTextBox.Text = _contact.Company;
                BirthdayDatePicker.SelectedDate = _contact.Birthday;
                FavoriteCheckBox.IsChecked = _contact.Favorite;
                EmailTextBox.Text = _contact.Email;
                PhoneTextBox.Text = _contact.Phone;
            }
        }
        public ContactContext contactContext { get; set; }

        public EditWindow()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!Validator.IsValidName(NameTextBox.Text))
            {
                InfoLabel.Content = "Имя должно содержать только символы";
                InfoLabel.Foreground = Brushes.Red;
                return;
            }
            if (!Validator.IsValidName(SurnameTextBox.Text))
            {
                InfoLabel.Content = "Фамилия должна содержать только символы";
                InfoLabel.Foreground = Brushes.Red;
                return;
            }
            if (CompanyTextBox.Text.Length == 0)
            {
                InfoLabel.Content = "Введите название компании";
                InfoLabel.Foreground = Brushes.Red;
                return;
            }
            if (BirthdayDatePicker.SelectedDate == null)
            {
                InfoLabel.Content = "Введите дату рождения";
                InfoLabel.Foreground = Brushes.Red;
                return;
            }
            if (!Validator.IsValidMailAddress(EmailTextBox.Text))
            {
                InfoLabel.Content = "Введите корректный E-mail";
                InfoLabel.Foreground = Brushes.Red;
                return;
            }
            if (!Validator.IsValidPhone(PhoneTextBox.Text))
            {
                InfoLabel.Content = "Введите корректный телефон";
                InfoLabel.Foreground = Brushes.Red;
                return;
            }
            if (_contact != null)
            {
                _contact.Name = NameTextBox.Text;
                _contact.Surname = SurnameTextBox.Text;
                _contact.Company = CompanyTextBox.Text;
                _contact.Birthday = (DateTime)BirthdayDatePicker.SelectedDate;
                _contact.Favorite = (bool)FavoriteCheckBox.IsChecked;
                _contact.Email = EmailTextBox.Text;
                _contact.Phone = PhoneTextBox.Text;
            }
            else
            {
                contactContext.Add(new Contact() { Id = DateTime.Now.Ticks.ToString(), Name = NameTextBox.Text, Surname = SurnameTextBox.Text, Company = CompanyTextBox.Text, Birthday = (DateTime)BirthdayDatePicker.SelectedDate, Favorite = (bool)FavoriteCheckBox.IsChecked, Email = EmailTextBox.Text, Phone = PhoneTextBox.Text });
            }
            contactContext.SaveChanges();
            if (Owner is MainWindow) ((MainWindow)Owner).Refresh();
            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
