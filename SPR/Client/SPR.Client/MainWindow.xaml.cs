using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SPR.Client.Abstractions.Services;
using SPR.Client.Commands;
using SPR.Client.ViewModels;
using SPR.Client.Views.Buttons;

namespace SPR.Client
{
    public partial class MainWindow : Window
    {
        private readonly INavigationService _navigationService;

        public MainWindow(INavigationService navigationService)
        {
            _navigationService = navigationService;
            DataContext = new MainViewModel(_navigationService);
           InitializeComponent();
        }
        
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if(Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_messages.Visibility = Visibility.Collapsed;
                tt_maps.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_messages.Visibility = Visibility.Visible;
                tt_maps.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            //img_bg.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            //img_bg.Opacity = 0.3;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void sidebar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = sidebar.SelectedItem as NavigationButton;
            if (selected is not null)
            {
            }
        }
    }
}