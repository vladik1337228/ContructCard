using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ContructCard
{
    class Skill
    {
        public Skill(string nameSkill, string pathImage, Uri pathPattern, string toolTipMessage)
        {
            NameSkill = nameSkill;
            PathImage = pathImage;
            PathPattern = pathPattern;
            ToolTipMessage = toolTipMessage;
        }

        public Skill()
        { }

        public string NameSkill { get; set; }
        public string PathImage { get; set; }
        public Uri PathPattern { get; set; }
        public string ToolTipMessage { get; set; }
    }

    class Skills
    { 
        public ObservableCollection<Skill> CollectionSkill { get; set; }

        public Skills()
        {
            CollectionSkill = new ObservableCollection<Skill>();

            DirectoryInfo directoryInfo = new DirectoryInfo("Skills");
            foreach (var item in directoryInfo.GetFiles())
            {
                CollectionSkill.Add(new Skill(item.Name, item.FullName, new Uri("Dictionary2.xaml", UriKind.Relative), "Хуйня"));
            }
        }
    }

    class Card
    {
        public Card(string nameCard, string pathImage, Uri pathPattern, string toolTipMes)
        {
            NameCard = nameCard;
            PathImage = pathImage;
            PathPattern = pathPattern;
            ToolTipMessage = toolTipMes;
        }

        public string NameCard { get; set; }
        public string PathImage { get; set; }
        public Uri PathPattern { get; set; }
        public string ToolTipMessage { get; set; }
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
