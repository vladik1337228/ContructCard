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
                new Card("Шаблон Стандартной карты", @"/StandartPattern.png", new Uri("Dictionary2.xaml", UriKind.Relative), "Карта игральной колоды. Неуникальна. Составляет игральную колоду игрока."));
            
            viewModelMain.Cards.CollectionCard.Add(
                new Card("Шаблон карты персонажа", @"/PersonalPattern.png", new Uri("Dictionary1.xaml", UriKind.Relative), "Карта персонажа. Уникальна. Выбирается в начале игры. Влияет на стиль игры."));

            viewModelMain.Cards.CollectionCard.Add(
                new Card("Шаблон карты общей колоды", @"/Additional.png", new Uri("Dictionary5.xaml", UriKind.Relative), "Карта общей колоды. Находится в общей колоде. Служит для дополнения игральных механик."));

            DataContext = viewModelMain;
        }
    }
}
