using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fortuneAndroid
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class fortunePage : ContentPage
    {
        private Entry wordEntry1;
        private Button displayButton;
        private Button addButton;
        private Label outputLabel;
        private Button startGame;

        public const string fileName = "Dictionary1.txt";
        public fortunePage()
        {
            wordEntry1 = new Entry
            {
                Placeholder = "Введите предсказание"
            };

            addButton = new Button
            {
                Text = "Добавить",
                FontSize = 18,
                TextColor = Color.FromHex("#253158"),
                BorderColor = Color.FromHex("#00a693"),
                CornerRadius = 10,
                WidthRequest = 200,
                HeightRequest = 40,
                BorderWidth = 2,
                BackgroundColor = Color.FromHex("#c9f8a9"),
                HorizontalOptions = LayoutOptions.Center
            };
            addButton.Clicked += AddButton_Clicked;


            displayButton = new Button
            {
                Text = "Показать слова",
                FontSize = 18,
                TextColor = Color.FromHex("#253158"),
                BorderColor = Color.FromHex("#00a693"),
                CornerRadius = 10,
                WidthRequest = 200,
                HeightRequest = 40,
                BorderWidth = 2,
                BackgroundColor = Color.FromHex("#c9f8a9"),
                HorizontalOptions = LayoutOptions.Center
            };
            displayButton.Clicked += DisplayButton_Clicked;

            outputLabel = new Label
            {
                Text = "Предсказание будет отображаться здесь",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            startGame = new Button
            {
                Text = "Играть!",
                FontSize = 18,
                TextColor = Color.FromHex("#253158"),
                BorderColor = Color.FromHex("#00a693"),
                CornerRadius = 10,
                WidthRequest = 200,
                HeightRequest = 40,
                BorderWidth = 2,
                BackgroundColor = Color.FromHex("#c9f8a9"),
                HorizontalOptions = LayoutOptions.Center
            };

            startGame.Clicked += StartGame_ClickedAsync;

            Content = new StackLayout
            {
                Children = { wordEntry1, addButton, displayButton, startGame, outputLabel }
            };

            CreateDictionaryFile();
        }

        private void StartGame_ClickedAsync(object sender, EventArgs e)
        {
            string[] words = ReadWordsFromFile();

            string wordOutput = string.Empty;

            Random random = new Random();

            string randomWord = words[random.Next(words.Length)];

            outputLabel.Text = wordOutput + randomWord;

        }

        private void DisplayButton_Clicked(object sender, EventArgs e)
        {
            string[] words = ReadWordsFromFile();

            if (words != null && words.Length >= 2)
            {
                string wordOutput = string.Empty;

                foreach (string word in words)
                {
                    wordOutput += word + "\n";
                }

                outputLabel.Text = wordOutput;
               
            }
            else
            {
                outputLabel.Text = "Не удалось прочитать слова из файла";
            }
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            string word1 = wordEntry1.Text;

            if (!string.IsNullOrWhiteSpace(word1))
            {
                outputLabel.Text = $"Слово 1: {word1}\n";

                SaveWordsToFile(word1);
            }
            else
            {
                outputLabel.Text = "Пожалуйста, введите оба слова";
            }

            wordEntry1.Text = string.Empty;
        }

        private void SaveWordsToFile(string word1)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
            string content = $"{word1}\n";

            using (StreamWriter write = File.AppendText(filePath))
            {
                write.WriteLine(word1);

            }
        }
        private void CreateDictionaryFile()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

            if (!File.Exists(filePath))
            {
                string content = string.Empty;
                File.WriteAllText(filePath, content);
            }
        }

        public static string[] ReadWordsFromFile()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                string[] words = content.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                return words;
            }

            return null;
        }
    }
}