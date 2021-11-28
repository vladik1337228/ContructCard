using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ImageMagick;
using ImageMagick.Configuration;

namespace ContructCard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ViewModelMain viewModelMain = new ViewModelMain();

            viewModelMain.Cards.CollectionCard.Add(
                new Card("Стандартный шаблон", @"/StandartPattern.png", new Uri("Dictionary2.xaml", UriKind.Relative), "Карта игральной колоды. Неуникальна. Составляет игральную колоду игрока."));
            
            viewModelMain.Cards.CollectionCard.Add(
                new Card("Шаблон персонажа", @"/PersonalPattern.png", new Uri("Dictionary1.xaml", UriKind.Relative), "Карта персонажа. Уникальна. Выбирается в начале игры. Влияет на стиль игры."));
           
            comboBox1.SelectionChanged += ComboBox1_SelectionChanged;
            DataContext = viewModelMain;
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Card card = (Card)comboBox1.SelectedItem;

            ResourceDictionary resourceDictionary = Application.LoadComponent(card.PathPattern) as ResourceDictionary;

            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
