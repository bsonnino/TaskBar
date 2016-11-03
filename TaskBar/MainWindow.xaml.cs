using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace TaskBar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        string[] images;
        int imageNo;

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            images = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "*.png", SearchOption.AllDirectories);
            LoadCurrentImage();
        }

        private void LoadCurrentImage()
        {
            img.Source = new BitmapImage(new Uri(images[imageNo], UriKind.Absolute));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CarregaImagemAnterior();
        }

        private void CarregaImagemAnterior()
        {
            if (imageNo > 0)
            {
                --imageNo;
                LoadCurrentImage();
                UpdateProgress();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CarregaProximaImagem();
        }

        private void CarregaProximaImagem()
        {
            if (imageNo < images.Length - 1)
            {
                ++imageNo;
                LoadCurrentImage();
                UpdateProgress();
            }
        }

        private void btnAnteriorClick(object sender, EventArgs e)
        {
            CarregaImagemAnterior();
        }

        private void btnProximoClick(object sender, EventArgs e)
        {
            CarregaProximaImagem();
        }

        private void UpdateProgress()
        {
            taskItemInfo.ProgressValue = (double)imageNo / (images.Length - 1);
            if (taskItemInfo.ProgressValue < 0.50)
                taskItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Normal;
            else if (taskItemInfo.ProgressValue < 0.80)
                taskItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Paused;
            else
                taskItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Error;

            if (imageNo == 0 || imageNo == images.Length - 1)
                taskItemInfo.Overlay = new BitmapImage(new Uri("pack://application:,,,/Images/stop.png"));
            else
                taskItemInfo.Overlay = null;

        }

    }
}
