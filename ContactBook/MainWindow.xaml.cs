using ContactBook.Models;
using Microsoft.EntityFrameworkCore;
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

namespace ContactBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool favoriteChecked = false;
        ContactContext dbContext;
        public MainWindow()
        {
            InitializeComponent();
            SearchTextBox.Foreground = Brushes.Gray;
            SearchTextBox.Text = "Введите данные для поиска...";
            dbContext = new ContactContext();
            dbContext.Contacts.Load();
            contactsGrid.ItemsSource = dbContext.Contacts.Local.ToObservableCollection();
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dbContext.Dispose();
        }

        private void SearchTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (((TextBox)sender).Foreground == Brushes.Gray)
            {
                ((TextBox)sender).Text = "";
                ((TextBox)sender).Foreground = Brushes.Black;
            }
        }

        private void SearchTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                if (((TextBox)sender).Text.Trim().Equals(""))
                {
                    ((TextBox)sender).Foreground = Brushes.Gray;
                    ((TextBox)sender).Text = "Введите данные для поиска...";
                }
            }
        }

        private void AddMenu_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow();
            editWindow.Owner = this;
            editWindow.contactContext = dbContext;
            editWindow.ShowDialog();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = contactsGrid.SelectedItem as Contact;
            if (contact != null)
            {
                EditWindow editWindow = new EditWindow();
                editWindow.Owner = this;
                editWindow.contact = contact;
                editWindow.contactContext = dbContext;
                editWindow.ShowDialog();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = contactsGrid.SelectedItem as Contact;
            if (contact != null)
            {
                dbContext.Remove(contact);
                dbContext.SaveChanges();
                contactsGrid.Items.Refresh();
            }
        }

        private void contactsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Contact contact = contactsGrid.SelectedItem as Contact;
            if (contact != null)
            {
                EditWindow editWindow = new EditWindow();
                editWindow.Owner = this;
                editWindow.contact = contact;
                editWindow.contactContext = dbContext;
                editWindow.ShowDialog();
            }
        }

        public void Refresh()
        {
            contactsGrid.Items.Refresh();
            UpdateFilter();
        }

        private void UpdateFilter()
        {
            if ((dbContext != null) && (contactsGrid != null))
            {
                if ((SearchTextBox.Text != "") && (SearchTextBox.Text != "Введите данные для поиска..."))
                {
                    if (favoriteChecked)
                    {
                        contactsGrid.ItemsSource = dbContext.Contacts.Where(p => p.Name.Contains(SearchTextBox.Text) || p.Surname.Contains(SearchTextBox.Text) || p.Company.Contains(SearchTextBox.Text) || p.Phone.Contains(SearchTextBox.Text) || p.Favorite).ToList();
                    }
                    else
                    {
                        contactsGrid.ItemsSource = dbContext.Contacts.Where(p => p.Name.Contains(SearchTextBox.Text) || p.Surname.Contains(SearchTextBox.Text) || p.Company.Contains(SearchTextBox.Text) || p.Phone.Contains(SearchTextBox.Text)).ToList();
                    }

                }
                else
                {
                    if (favoriteChecked)
                    {
                        contactsGrid.ItemsSource = dbContext.Contacts.Where(p => p.Favorite).ToList();
                    }
                    else
                    {
                        contactsGrid.ItemsSource = dbContext.Contacts.Local.ToObservableCollection();
                    }
                }
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateFilter();
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
        }

        private void FavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            favoriteChecked = !favoriteChecked;
            FavoriteButton.Foreground = (FavoriteButton.Foreground == Brushes.Green) ? Brushes.Black : Brushes.Green;
            UpdateFilter();
        }
    }
}
