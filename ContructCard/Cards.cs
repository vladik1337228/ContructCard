using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContructCard
{
    class Card
    {
        public Card(string nameCard, string pathImage)
        {
            NameCard = nameCard;
            PathImage = pathImage;
        }

        public string NameCard { get; set; }
        public string PathImage { get; set; }
    }

    class Cards
    {
        public ObservableCollection<Card> CollectionCard { get; set; }

        public Cards()
        {
            CollectionCard = new ObservableCollection<Card>();
        }
    }

    class SizeFont
    { 
        public int FontSize { get; set; }

        public SizeFont(int px)
        {
            FontSize = px;
        }

    }
}
