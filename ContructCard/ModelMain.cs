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
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.IO.Packaging;
using System.Windows;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.ServiceProcess;
using Microsoft.Dism;
using System.Diagnostics;
using System.Windows.Markup;

namespace ContructCard
{
    public class TextElement
    {
        public TextElement(InlineUIContainer inlineUIContainer, int index)
        {
            InlineUIContainer = inlineUIContainer;
            Index = index;
        }

        public InlineUIContainer InlineUIContainer { get; set; }
        public int Index { get; set; }

        public TextElement(InlineUIContainer inlineUIContainer)
        {
            InlineUIContainer = inlineUIContainer;
        }
    }

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
                
                using (FileStream fs = System.IO.File.Open(saveFileDialog.FileName + "\\card.xaml", FileMode.Create))
                    XamlWriter.Save(cardSerialization.FlowDocument, fs);
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

            using (FileStream fs = System.IO.File.Open(path + "\\card.xaml", FileMode.Create))
                XamlWriter.Save(cardSerialization.FlowDocument, fs);

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

                using (FileStream fs = System.IO.File.Open(openFileDialog.FileName.Replace(".json", $".xaml"), FileMode.Open))
                    obj.FlowDocument = (FlowDocument)XamlReader.Load(fs);

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
        public int SkillCard { get; set; }
        public string TextCard { get; set; }
        public string PathImage { get; set; }
        public int PatternCard { get; set; }
        public int TitleFontSize { get; set; }
        public int TextFontSize { get; set; }
        public int AlignmentX { get; set; }
        public int AlignmentY { get; set; }
        public double Scale { get; set; }
        public bool SecondSkill { get; set; }
        public int SkillCard2 { get; set; }
        public string DmgDop { get; set; }
        public string HPDop { get; set; }
        public string CountStepDop { get; set; }
        public List<TextElement> CollectionTextElement;
        public FlowDocument FlowDocument;

        public CardSerialization()
        {
        }

    }


    class ModelMain
    {
        public Cards Cards { get; set; }
        public AlignmentX ImageX { get; set; }
        public AlignmentY ImageY { get; set; }
        public ObservableCollection<SizeFont> CollectionSize { get; set; }
        public ObservableCollection<SizeFont> CollectionSizeTitle { get; set; }
        public Skills Skills { get; set; }
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
        public Skills Skills { get { return modelMain.Skills; } set { modelMain.Skills = value; OnPropertyChanged("Skills"); } }
        public int ValueSlider3 { get { return cardSerialization.AlignmentX; }  set { cardSerialization.AlignmentX = value; OnPropertyChanged("ValueSlider3"); } }
        public int ValueSliderY { get { return cardSerialization.AlignmentY; } set { cardSerialization.AlignmentY = value; OnPropertyChanged("ValueSliderY"); } }
        public AlignmentX ImageX { get { return modelMain.ImageX; } set { modelMain.ImageX = value; OnPropertyChanged("ImageX"); } }
        public AlignmentY AlignmentY { get { return modelMain.ImageY; } set { modelMain.ImageY = value; OnPropertyChanged("AlignmentY"); } }
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
        public int SkillCard { get { return cardSerialization.SkillCard; } set { cardSerialization.SkillCard = value; OnPropertyChanged("SkillCard"); } }
        public bool SecondSkill { get { return cardSerialization.SecondSkill; } set { cardSerialization.SecondSkill = value; OnPropertyChanged("SecondSkill"); } }
        public int SkillCard2 { get { return cardSerialization.SkillCard2; } set { cardSerialization.SkillCard2 = value; OnPropertyChanged("SkillCard2"); } }
        public string DmgDop { get { return cardSerialization.DmgDop; } set { cardSerialization.DmgDop = value; OnPropertyChanged("DmgDop"); } }
        public string HPDop { get { return cardSerialization.HPDop; } set { cardSerialization.HPDop = value; OnPropertyChanged("HPDop"); } }
        public string CountStepDop { get { return cardSerialization.CountStepDop; } set { cardSerialization.CountStepDop = value; OnPropertyChanged("CountStepDop"); } }
        public List<TextElement> CollectionTextElement { get { return cardSerialization.CollectionTextElement; } set { cardSerialization.CollectionTextElement = value; OnPropertyChanged("CollectionTextElement"); } }
        public FlowDocument FlowDocument { get { return cardSerialization.FlowDocument; } set { cardSerialization.FlowDocument = value; OnPropertyChanged("FlowDocument"); } }

        private string Uri;

        public ViewModelMain()
        {
            cardSerialization = new CardSerialization();
            modelMain = new ModelMain();
            Cards = new Cards();
            Skills = new Skills();
            FlowDocument = new FlowDocument(new Paragraph() { FontFamily = new System.Windows.Media.FontFamily("Fixedsys Excelsior 3.01"), TextAlignment = TextAlignment.Center, FontSize = 10 });
            CollectionTextElement = new List<TextElement>();

            ImageY = 1.0;
            AlignmentY = AlignmentY.Center;
            SecondSkill = false;
            DmgDop = "0";
            HPDop = "0";
            CountStepDop = "0";
            TextCard = "";

            CollectionSizeTitle = new ObservableCollection<SizeFont>();
            for (int i = 12; i <= 22; i += 2)
                CollectionSizeTitle.Add(new SizeFont(i));

            CollectionSize = new ObservableCollection<SizeFont>();
            for (int i = 10; i <= 22; i += 2)
                CollectionSize.Add(new SizeFont(i));

            System.IO.FileInfo fileInfo = new FileInfo(@"./1.jpg");

            ImagePath = fileInfo.FullName;
            OriginalImagePath = fileInfo.FullName;


            ChangePattern(new Uri("Dictionary2.xaml", UriKind.Relative));

            NuberCards();
        }

        private string oldstr = "";

        private CardCommand textChanged;
        public CardCommand TextChanged
        {
            get
            {
                return textChanged ?? (textChanged = new CardCommand(obj =>
                {
                    if(obj.ToString() != oldstr)
                        TextChangeMethod(obj);

                    oldstr = obj.ToString();
                }));
            }
        }

        private void TextChangeMethod(object obj)
        {
            var paragraf = FlowDocument.Blocks.LastBlock as Paragraph;

            string text = obj.ToString();
            var masText = text.Split('◙').ToList();
            
            if (masText.Count > 1)
            {
                int iteration = masText.Count;
                for (int i = 0, j = 0; j < iteration - 1; i++)
                    if (masText[i] != "◙")
                    {
                        masText.Insert(i + 1, "◙");
                        j++;
                    }
            }

            try
            {
                for (int i = 0, j = 0; i < masText.Count; i++)
                    if (masText[i].ToString() == "◙")
                    {
                        CollectionTextElement[j].Index = i;
                        j++;
                    }
            }
            catch
            {
                masText.RemoveAt(masText.Count - 1);
            }

            try
            {
                for (int i = 0; i < CollectionTextElement.Count; i++)
                    if (masText[CollectionTextElement[i].Index] != '◙'.ToString())
                        CollectionTextElement.RemoveAt(i);
            }
            catch
            {
                CollectionTextElement.Remove(CollectionTextElement.Last());
            }

            paragraf.Inlines.Clear();
            for (int i = 0, j = 0; i < masText.Count; i++)
            {
                if (CollectionTextElement.FirstOrDefault(x => i == x.Index) == default)
                    paragraf.Inlines.Add(new Run(masText[i].ToString()));
                else
                {
                    paragraf.Inlines.Add(CollectionTextElement[j].InlineUIContainer);
                    j++;
                }
            }
        }

        private CardCommand addImageInText;
        public CardCommand AddImageInText
        {
            get
            {
                return addImageInText ?? (addImageInText = new CardCommand(obj =>
                {
                    var paragraf = FlowDocument.Blocks.LastBlock as Paragraph;
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
                    FileInfo fileInfo = new FileInfo("Skills");
                    openFileDialog.InitialDirectory = fileInfo.FullName;
                    if (openFileDialog.ShowDialog().Value == true)
                    {
                        System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                        BitmapImage bitmapImage = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Relative));
                        bitmapImage.Freeze();
                        image.Source = bitmapImage;
                        image.Width = CollectionSize[TextFontSize].FontSize;
                        image.Height = image.Width;
                        CollectionTextElement.Add(new TextElement(new InlineUIContainer(image), TextCard?.Length ?? 0));
                        TextCard += " ◙ ";
                        paragraf.Inlines.Add(CollectionTextElement.Last().InlineUIContainer);
                    }
                }));
            }
        }

        private CardCommand sizeChanged;
        public CardCommand SizeChanged
        {
            get
            {
                return sizeChanged ?? (sizeChanged = new CardCommand(obj =>
                {
                    var paragraf = FlowDocument.Blocks.LastBlock as Paragraph;
                    paragraf.FontSize = CollectionSize[TextFontSize].FontSize;
                    foreach (var item in CollectionTextElement)
                    {
                        item.InlineUIContainer.Child.SetValue(System.Windows.Controls.Image.WidthProperty, (double)CollectionSize[TextFontSize].FontSize);
                        item.InlineUIContainer.Child.SetValue(System.Windows.Controls.Image.HeightProperty, (double)CollectionSize[TextFontSize].FontSize);
                    }
                }));
            }
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

        private CardCommand sliderValueY;
        public CardCommand SliderValueY
        {
            get
            {
                return sliderValueY ?? (sliderValueY = new CardCommand(obj =>
                {
                    switch (ValueSliderY)
                    {
                        case 0:
                            AlignmentY = AlignmentY.Bottom;
                            break;
                        case 1:
                            AlignmentY = AlignmentY.Center;
                            break;
                        case 2:
                            AlignmentY = AlignmentY.Top;
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

                        System.IO.File.Copy(InputImagePath, SaveImagePath += $"\\{Path.GetRandomFileName()}.png");

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
                StartService("Spooler");
                DismChack();

                OpenFileDialog openFileDialog = new OpenFileDialog();
                var panel = obj as Canvas;

                PrintDialog pd = new PrintDialog();
                pd.PrintQueue = new PrintQueue(new PrintServer(), "Microsoft Print to PDF");
                pd.PrintTicket.PageOrientation = PageOrientation.Portrait;
                pd.PrintVisual(panel, "card");

                openFileDialog.Title = "Выберите PDF файл для отправки на гугл диск";
                openFileDialog.Filter = "PDF Files(*.PDF;)|*.PDF;|All files (*.*)|*.*";

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

                        file.Title = $"{Environment.UserName}card.xaml";

                        using (Stream stream = System.IO.File.Open(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"\\ConstructorVladika\\PatternCard{Environment.UserName}\\card.xaml", FileMode.Open, FileAccess.Read))
                        {
                            insertMediaUpload = driveService.Files.Insert(file, stream, "application/xaml");
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
            catch (Exception e)
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
                    ImageBrush image = obj as ImageBrush;
                    image.ClearValue(UIElement.UidProperty);

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
                        var parag = FlowDocument.Blocks.LastBlock as Paragraph;

                        CollectionTextElement = parag.Inlines.Where(x => x is InlineUIContainer).Select(x => new TextElement(x as InlineUIContainer)).ToList();

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
                        SkillCard = cardSerialization.SkillCard;
                        SecondSkill = cardSerialization.SecondSkill;
                        SkillCard2 = cardSerialization.SkillCard2;
                        DmgDop = cardSerialization.DmgDop;
                        HPDop = cardSerialization.HPDop;
                        CountStepDop = cardSerialization.CountStepDop;
                        ValueSliderY = cardSerialization.AlignmentY;
                        FlowDocument = cardSerialization.FlowDocument;
                    }
                    NuberCards();
                }));
            }
        }

        public void StartService(string serviceName)
        {
            ServiceController service = new ServiceController(serviceName);
            // Проверяем не запущена ли служба
            if (service.Status != ServiceControllerStatus.Running)
            {
                // Запускаем службу
                service.Start();
                // В течении минуты ждём статус от службы
                service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMinutes(1));
                MessageBox.Show($"Прошу прощения я службу запустил для работы программы. Служба {serviceName} успешно запущена!");
            }
        }

        public void DismChack()
        {
            DismApi.Initialize(DismLogLevel.LogErrorsWarningsInfo);
            using (var session = DismApi.OpenOnlineSession())
            {
                var x = DismApi.GetFeatureInfo(session, "Printing-PrintToPDFServices-Features");

                CmdLoadComponents(x.FeatureState);
            }

            DismApi.Shutdown();
        }

        public void CmdLoadComponents(DismPackageFeatureState state)
        {
            if (state != DismPackageFeatureState.Installed)
            {
                Process psi = new Process();
                psi.StartInfo.FileName = "cmd";
                psi.StartInfo.Arguments = @"/c Dism /Online /Enable-Feature /FeatureName:Printing-PrintToPDFServices-Features";
                psi.Start();
                psi.WaitForExit();
            }
        }

        private CardCommand patternCardChangePatter;
        public CardCommand PatternCardChangePatter
        {
            get
            {
                return patternCardChangePatter ?? (patternCardChangePatter = new CardCommand(obj =>
                {
                    ChangePattern(Cards.CollectionCard[PatternCard].PathPattern);

                }));
            }
        }


        private CardCommand patternSkillChangePatter;
        public CardCommand PatternSkillChangePatter
        {
            get
            {
                return patternSkillChangePatter ?? (patternSkillChangePatter = new CardCommand(obj =>
                {
                    Skill ski = obj as Skill;

                    ChangePattern(ski.PathPattern);
                    PatternCard = 0;

                    if (ski.NameSkill == "Evolve")
                        SecondSkill = false;
                }));
            }
        }

        private CardCommand secondSkillCheck;
        public CardCommand SecondSkillCheck
        {
            get
            {
                return secondSkillCheck ?? (secondSkillCheck = new CardCommand(obj =>
                {
                    if (SecondSkill)
                    {
                        foreach (var item in Skills.CollectionSkill)
                            item.PathPattern = new Uri("Dictionary3.xaml", UriKind.Relative);

                        Skills.CollectionSkill.First(x => x.NameSkill == "Evolve").PathPattern = new Uri("Dictionary4.xaml", UriKind.Relative);
                        Cards.CollectionCard[0].PathPattern = new Uri("Dictionary3.xaml", UriKind.Relative);
                    }
                    else
                    {
                        foreach (var item in Skills.CollectionSkill)
                            item.PathPattern = new Uri("Dictionary2.xaml", UriKind.Relative);

                        Skills.CollectionSkill.First(x => x.NameSkill == "Evolve").PathPattern = new Uri("Dictionary4.xaml", UriKind.Relative);
                        Cards.CollectionCard[0].PathPattern = new Uri("Dictionary2.xaml", UriKind.Relative);
                        SkillCard2 = 0;
                    }

                    ChangePattern(Skills.CollectionSkill[SkillCard].PathPattern);
                }));
            }
        }

        public void ChangePattern(Uri uri)
        {
            if (uri.OriginalString != Uri)
            {
                ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
                Uri = uri.OriginalString;
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
