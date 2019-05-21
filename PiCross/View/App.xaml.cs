using PiCross;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MediaPlayer mediaPlayer;
        private List<string> songs;
        private static Random random = new Random();
        protected override void OnStartup(StartupEventArgs e)
        {
            songs = new List<string>();
            songs.Add("../wii.mp3");
            songs.Add("../portal.mp3");

            base.OnStartup(e);

            var window = new MainWindow();
            window.DataContext = new Navigator();
            window.Show();

            mediaPlayer = new MediaPlayer();
            mediaPlayer.MediaEnded += OnMediaEnded;
            var randomSongs = songs.OrderBy(a => random.Next());
            mediaPlayer.Open(new Uri((randomSongs.ElementAt(0)).ToString(), UriKind.Relative));
            mediaPlayer.Play();
        }

        private void OnMediaEnded(object sender, EventArgs e)
        {
            var songsRandomized = songs.OrderBy(a => random.Next());
            mediaPlayer.Open(new Uri((songsRandomized.ElementAt(0)).ToString(), UriKind.Relative));
            mediaPlayer.Play();
        }
    }
}
