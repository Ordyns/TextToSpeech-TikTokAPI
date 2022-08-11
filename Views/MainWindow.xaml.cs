using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using ModernWpf;
using ModernWpf.Controls;


namespace TikTokTTS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance;

        public MainPage MainPage { get; } = new MainPage();
        public HistoryPage HistoryPage { get; } = new HistoryPage();
        public SettingsPage SettingsPage { get; } = new SettingsPage();

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            ThemeManagerProxy.Current.AccentColor = Color.FromRgb(0, 132, 255);
            MoveToMainPage();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e) => MoveToMainPage();
        private void HistoryButton_Click(object sender, RoutedEventArgs e) => MoveToHistoryPage();

        public void MoveToHistoryPage()
        {
            NavigationView.SelectedItem = GetItemByTagFromNavigationMenu(nameof(HistoryPage));
            ContentFrame.Navigate(HistoryPage);
        }

        public void MoveToMainPage()
        {
            NavigationView.SelectedItem = GetItemByTagFromNavigationMenu(nameof(MainPage));
            ContentFrame.Navigate(MainPage);
        }

        public void MoveToSettingsPage() => ContentFrame.Navigate(SettingsPage);

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                MoveToSettingsPage();
            }
            else
            {
                NavigationViewItem selectedItem = (NavigationViewItem)args.SelectedItem;
   
                string currentNamespace = typeof(MainPage).Namespace;
                Assembly assembly = typeof(MainPage).Assembly;
                Type selectedPageType = assembly.GetType($"{currentNamespace}.{(string)selectedItem.Tag}");

                if (selectedPageType == typeof(MainPage))
                    MoveToMainPage();
                else if (selectedPageType == typeof(HistoryPage))
                    MoveToHistoryPage();
            }
        }

        private NavigationViewItem GetItemByTagFromNavigationMenu(string tag)
        {
            NavigationViewItem[] menuItems = new NavigationViewItem[NavigationView.MenuItems.Count];
            NavigationView.MenuItems.CopyTo(menuItems, 0);

            NavigationViewItem item = menuItems.ToList().Find(menuItem => menuItem.Tag as string == tag);
            return item;
        }
    }
}
