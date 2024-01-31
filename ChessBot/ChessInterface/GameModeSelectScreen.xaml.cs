using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ChessLogic;

namespace ChessInterface
{
    /// <summary>
    /// Logique d'interaction pour GameModeSelectScreen.xaml
    /// </summary>
    public partial class GameModeSelectScreen : UserControl
    {
        private MainWindow mainWindow;
        

        
        public GameModeSelectScreen(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            if (mainWindow._checkBox)
            {
                ToggleBooks.IsChecked = true;
            }
            var humanVsHumanUri = new Uri("pack://application:,,,/Assets/Icons/human-vs-human.png");
            HumanVSHumanImg.Source = new BitmapImage(humanVsHumanUri);

            var humanVsAIUri = new Uri("pack://application:,,,/Assets/Icons/human-vs-ai.png");
            HumanVSAiImg.Source = new BitmapImage(humanVsAIUri);

            var aiVsAIUri = new Uri("pack://application:,,,/Assets/Icons/ai-vs-ai.png");
            AiVSAiImg.Source = new BitmapImage(aiVsAIUri);



        }


        private void HumanVSHumanImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.HumanVSHuman();
        }

        private void AiVSAiImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.BotVsBot();
        }

        private void HumanVSAiImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.HumanVsBot();
        }

        private void ToogleBooks(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            bool isChecked = cb.IsChecked ?? false;
            if (isChecked)
            {
                mainWindow.BookActivate();

            }
            else
            {
                mainWindow.BookDesactivate();
            }
        }


    }
}
