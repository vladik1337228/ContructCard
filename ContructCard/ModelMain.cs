using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Drive.v2;
using Google.Apis.Util.Store;
using Google.Apis.Drive.v2.Data;
using System.Printing;
using System.Windows.Documents;
using System.Drawing;
using System.Drawing.Printing;
using ImageMagick;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.IO.Packaging;
using System.Windows;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace ContructCard
{
    public class Serialization
    {
        public static void Serialize(CardSerialization cardSerialization)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            string jsonText = JsonSerializer.Serialize<CardSerialization>(cardSerialization, options);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Укажите название папки шаблона";
            if (saveFileDialog.ShowDialog().Value == true)
            {
                Directory.CreateDirectory(saveFileDialog.FileName);
                System.IO.File.Create(saveFileDialog.FileName + "\\card.json").Close();
                System.IO.File.WriteAllText(saveFileDialog.FileName + "\\card.json", jsonText);
                System.IO.File.Copy(cardSerialization.PathImage, saveFileDialog.FileName + $"\\card.{cardSerialization.PathImage.Split('.').Last()}");
            }
        }

        public static void Serialize(CardSerialization cardSerialization, string path)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            string jsonText = JsonSerializer.Serialize<CardSerialization>(cardSerialization, options);
            Directory.CreateDirectory(path += $"\\ConstructorVladika\\PatternCard{Environment.UserName}");
            System.IO.File.Create(path + "\\card.json").Close();
            System.IO.File.WriteAllText(path + "\\card.json", jsonText);
            System.IO.File.Copy(cardSerialization.PathImage, path + $"\\card.{cardSerialization.PathImage.Split('.').Last()}");
        }

        public static CardSerialization Deserialize()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите JSON файл шаблона";
            openFileDialog.Filter = "JSON Files(*.JSON;)|*.JSON;";
            if (openFileDialog.ShowDialog().Value == true)
            {
                var obj = JsonSerializer.Deserialize<CardSerialization>(System.IO.File.ReadAllText(openFileDialog.FileName));
                obj.PathImage = openFileDialog.FileName.Replace(".json", $".{obj.PathImage.Split('.').Last()}");
                return obj;
            }
            else
                return null;
        }
    }

    public class CardSerialization
    {
        public int Dmg { get; set; }
        public int Hp { get; set; }
        public string TitleCard { get; set; }
        public string NumberCard { get; set; }
        public int Mana { get; set; }
        public int TypeCard { get; set; }
        public string TextCard { get; set; }
        public string PathImage { get; set; }
        public int PatternCard { get; set; }
        public int TitleFontSize { get; set; }
        public int TextFontSize { get; set; }
        public int AlignmentX { get; set; }
        public double Scale { get; set; }

        public CardSerialization()
        {
        }

        public CardSerialization(int dmg, int hp, string titleCard, string numberCard, int mana, int typeCard, string textCard, string pathImage, int patternCard, int titleFontSize, int textFontSize, int alignmentX, double scale)
        {
            Dmg = dmg;
            Hp = hp;
            TitleCard = titleCard;
            NumberCard = numberCard;
            Mana = mana;
            TypeCard = typeCard;
            TextCard = textCard;
            PathImage = pathImage;
            PatternCard = patternCard;
            TitleFontSize = titleFontSize;
            TextFontSize = textFontSize;
            AlignmentX = alignmentX;
            Scale = scale;
        }
    }


    class ModelMain
    {
        public Cards Cards { get; set; }
        public AlignmentX ImageX { get; set; }
        public ObservableCollection<SizeFont> CollectionSize { get; set; }
        public ObservableCollection<SizeFont> CollectionSizeTitle { get; set; }
        public string ImagePath { get; set; }

        public ModelMain()
        {
            CollectionSize = new ObservableCollection<SizeFont>();
        }

    }

    class ViewModelMain : INotifyPropertyChanged
    {
        private ModelMain modelMain;
        private CardSerialization cardSerialization;


        public Cards Cards { get { return modelMain.Cards; } set { modelMain.Cards = value; OnPropertyChanged("Cards"); } }
        public int ValueSlider3 { get { return cardSerialization.AlignmentX; }  set { cardSerialization.AlignmentX = value; OnPropertyChanged("ValueSlider3"); } }
        public AlignmentX ImageX { get { return modelMain.ImageX; } set { modelMain.ImageX = value; OnPropertyChanged("ImageX"); } }
        public double ImageY { get { return cardSerialization.Scale; } set { cardSerialization.Scale = value; OnPropertyChanged("ImageY"); } }
        public ObservableCollection<SizeFont> CollectionSize { get { return modelMain.CollectionSize; } set { modelMain.CollectionSize = value; OnPropertyChanged("CollectionSize"); } }
        public ObservableCollection<SizeFont> CollectionSizeTitle { get { return modelMain.CollectionSizeTitle; } set { modelMain.CollectionSizeTitle = value; OnPropertyChanged("CollectionSizeTitle"); } }
        public string ImagePath { get { return modelMain.ImagePath; } set { modelMain.ImagePath = value; OnPropertyChanged("ImagePath"); } }
        public string NumberCard { get { return cardSerialization.NumberCard; } set { cardSerialization.NumberCard = value; OnPropertyChanged("NumberCard"); } }
        public int Dmg { get { return cardSerialization.Dmg; } set { cardSerialization.Dmg = value; OnPropertyChanged("Dmg"); } }
        public int Mana { get { return cardSerialization.Mana; } set { cardSerialization.Mana = value; OnPropertyChanged("Mana"); } }
        public int Hp { get { return cardSerialization.Hp; } set { cardSerialization.Hp = value; OnPropertyChanged("Hp"); } }
        public string OriginalImagePath { get { return cardSerialization.PathImage; } set { cardSerialization.PathImage = value; OnPropertyChanged("OriginalImagePath"); } }
        public int PatternCard { get { return cardSerialization.PatternCard; } set { cardSerialization.PatternCard = value; OnPropertyChanged("PatternCard"); } }
        public int TypeCard { get { return cardSerialization.TypeCard; } set { cardSerialization.TypeCard = value; OnPropertyChanged("TypeCard"); } }
        public string TitleCard { get { return cardSerialization.TitleCard; } set { cardSerialization.TitleCard = value; OnPropertyChanged("TitleCard"); } }
        public string TextCard { get { return cardSerialization.TextCard; } set { cardSerialization.TextCard = value; OnPropertyChanged("TextCard"); } }
        public int TitleFontSize { get { return cardSerialization.TitleFontSize; } set { cardSerialization.TitleFontSize = value; OnPropertyChanged("TitleFontSize"); } }
        public int TextFontSize { get { return cardSerialization.TextFontSize; } set { cardSerialization.TextFontSize = value; OnPropertyChanged("TextFontSize"); } }

        public ViewModelMain()
        {

            cardSerialization = new CardSerialization();
            modelMain = new ModelMain();
            Cards = new Cards();

            ImageY = 1.0;

            CollectionSizeTitle = new ObservableCollection<SizeFont>();
            for (int i = 12; i <= 22; i += 2)
                CollectionSizeTitle.Add(new SizeFont(i));

            CollectionSize = new ObservableCollection<SizeFont>();
            for (int i = 10; i <= 22; i += 2)
                CollectionSize.Add(new SizeFont(i));

            System.IO.FileInfo fileInfo = new FileInfo(@"./1.jpg");

            ImagePath = fileInfo.FullName;
            OriginalImagePath = fileInfo.FullName;

            NuberCards();
        }

        private CardCommand sliderValue;
        public CardCommand SliderValue
        { get
            { return sliderValue ?? (sliderValue = new CardCommand(obj =>
                {
                    switch (ValueSlider3)
                    {
                        case 0:
                            ImageX = AlignmentX.Left;
                            break;
                        case 1:
                            ImageX = AlignmentX.Center;
                            break;
                        case 2:
                            ImageX = AlignmentX.Right;
                            break;
                    }
                }));
            }
        }

        private CardCommand openDialog;
        public CardCommand OpenDialog
        {
            get
            {
                return openDialog ?? (openDialog = new CardCommand(obj =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        string SaveImagePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\ConstructorVladika";
                        System.IO.Directory.CreateDirectory(SaveImagePath);
                        string InputImagePath = openFileDialog.FileName;
                        using (MagickImage image = new MagickImage(InputImagePath))
                        {
                            image.Emboss(1, 2);
                            image.Equalize();
                            image.Write(SaveImagePath += $"\\{Path.GetRandomFileName()}.png");

                            image.Dispose();
                        }
                        OriginalImagePath = SaveImagePath;
                        ImagePath = SaveImagePath;
                    }

                    NuberCards();

                }));
            }
        }



        public void NuberCards()
        {
            try
            {
                var list = driveService.Children.List("1KlZ24QRIZeb5LFXA6Gkd6-GkdX6Zpij7").Execute().Items;

                NumberCard = (list.Count + 1).ToString();
            }
            catch
            {
                MessageBox.Show("Где интернет сука!?");
            }
        }

        private static string[] Scopes = { DriveService.Scope.Drive };
        private static string AppName = "ContructCard";
        private static DriveService driveService = null;
        private static UserCredential userCredential = null;

        static ViewModelMain()
        {

            using (Stream stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string path = Path.Combine("driveApiCredentional", "drive-credentional.json");

                userCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.FromStream(stream).Secrets, Scopes, "User", System.Threading.CancellationToken.None, new FileDataStore(path, true)).Result;
                driveService = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = userCredential,
                    ApplicationName = AppName
                });
            }
        }

        private CardCommand save;
        public CardCommand Save { get { return save ?? (save = new CardCommand(obj =>
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                try
                {
                    var panel = obj as Canvas;

                    //PrintDialog pd = new PrintDialog();
                    //pd.PrintQueue = new PrintQueue(new PrintServer(), "Microsoft Print to PDF");
                    //pd.PrintTicket.PageOrientation = PageOrientation.Portrait;
                    //pd.PrintVisual(panel, "card");

                    openFileDialog.Title = "Выберите PDF файл для отправки на гугл диск";
                    openFileDialog.Filter = "PDF Files(*.PDF;)|*.PDF;|All files (*.*)|*.*";
                }
                catch
                {
                    MessageBox.Show("пизда");
                }

                if (openFileDialog.ShowDialog().Value == true)
                {
                    try
                    {
                        //папка
                        var direct = new Google.Apis.Drive.v2.Data.File();
                        direct.MimeType = "application/vnd.google-apps.folder";
                        direct.Title = $"NewCard{Environment.UserName}";
                        direct.Parents = new List<ParentReference>() { new ParentReference() { Id = "1KlZ24QRIZeb5LFXA6Gkd6-GkdX6Zpij7" } };

                        var requestx = driveService.Files.Insert(direct);
                        var id = requestx.Execute().Id;

                        //подпапка
                        var direct1 = new Google.Apis.Drive.v2.Data.File();
                        direct1.MimeType = "application/vnd.google-apps.folder";
                        direct1.Title = $"PatternCard{Environment.UserName}";
                        direct1.Parents = new List<ParentReference>() { new ParentReference() { Id = id } };

                        requestx = driveService.Files.Insert(direct1);
                        var id1 = requestx.Execute().Id;

                        //файлы папки
                        var file = new Google.Apis.Drive.v2.Data.File();
                        file.Title = $"{Environment.UserName}card.pdf";
                        file.Parents = new List<ParentReference>() { new ParentReference() { Id = id } };

                        FilesResource.InsertMediaUpload insertMediaUpload;

                        using (Stream stream = System.IO.File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                        {
                            insertMediaUpload = driveService.Files.Insert(file, stream, "application/pdf");
                            insertMediaUpload.Upload();
                        }

                        //файлы подпапки
                        Serialization.Serialize(cardSerialization, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

                        file.Title = $"{Environment.UserName}card.json";
                        file.Parents = new List<ParentReference>() { new ParentReference() { Id = id1 } };

                        using (Stream stream = System.IO.File.Open(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"\\ConstructorVladika\\PatternCard{Environment.UserName}\\card.json", FileMode.Open, FileAccess.Read))
                        {
                            insertMediaUpload = driveService.Files.Insert(file, stream, "application/json");
                            insertMediaUpload.Upload();
                        }

                        file.Title = $"{Environment.UserName}card.png";

                        using (Stream stream = System.IO.File.Open(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"\\ConstructorVladika\\PatternCard{Environment.UserName}\\card.{OriginalImagePath.Split('.').Last()}", FileMode.Open, FileAccess.Read))
                        {
                            insertMediaUpload = driveService.Files.Insert(file, stream, "image/*");
                            insertMediaUpload.Upload();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Где интернет сука!?");
                    }
                    finally
                    {
                        System.IO.Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"\\ConstructorVladika\\PatternCard{Environment.UserName}", true);
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        )); } }

        private CardCommand close;
        public CardCommand Close
        {
            get
            {
                return close ?? (close = new CardCommand(obj =>
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\ConstructorVladika");
                    var list = directoryInfo.GetFiles();
                    foreach (var item in list)
                        if (ImagePath != item.FullName)
                            item.Delete();
                }));
            }
        }

        private CardCommand saveElements;
        public CardCommand SaveElements
        {
            get
            {
                return saveElements ?? (saveElements = new CardCommand(obj =>
                {
                    Serialization.Serialize(cardSerialization);
                }));
            }
        }

        private CardCommand loadElements;
        public CardCommand LoadElements
        {
            get
            {
                return loadElements ?? (loadElements = new CardCommand(obj =>
                {
                    var tmp = Serialization.Deserialize();
                    if (tmp != null)
                    {
                        cardSerialization = tmp;
                        Dmg = cardSerialization.Dmg;
                        Hp = cardSerialization.Hp;
                        Mana = cardSerialization.Mana;
                        TitleCard = cardSerialization.TitleCard;
                        TextCard = cardSerialization.TextCard;
                        TextFontSize = cardSerialization.TextFontSize;
                        TitleFontSize = cardSerialization.TitleFontSize;
                        NumberCard = cardSerialization.NumberCard;
                        TypeCard = cardSerialization.TypeCard;
                        PatternCard = cardSerialization.PatternCard;
                        ValueSlider3 = cardSerialization.AlignmentX;
                        ImageY = cardSerialization.Scale;
                        ImagePath = cardSerialization.PathImage;
                    }
                    NuberCards();
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
