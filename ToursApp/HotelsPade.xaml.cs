﻿using System;
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

namespace ToursApp
{
    /// <summary>
    /// Логика взаимодействия для HotelsPade.xaml
    /// </summary>
    public partial class HotelsPade : Page
    {
        public HotelsPade()
        {
            InitializeComponent();
            DGridHotels.ItemsSource = ToursBaseEntities.GetContext().Hotel.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPade((sender as Button).DataContext as Hotel));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPade(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var hotelForRemoving = DGridHotels.SelectedItems.Cast<Hotel>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следущие {hotelForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo , MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

            }
            try
            {
                ToursBaseEntities.GetContext().Hotel.RemoveRange(hotelForRemoving);
                ToursBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные удалены!");

                DGridHotels.ItemsSource = ToursBaseEntities.GetContext().Hotel.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Page_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ToursBaseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridHotels.ItemsSource = ToursBaseEntities.GetContext().Hotel.ToList();
            }
        }
    }
}