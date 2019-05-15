using PiCross;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Puzzle puzzle = Puzzle.FromRowStrings(
            "xxxxxxx",
            "x.....x",
            "x.....x",
            "x.....x",
            "x.....x",
            "x.....x",
            "xxxxxxx"
            );


            var window = new MainWindow();
            window.DataContext = new GameViewModel(puzzle);
            window.Show();
        }
    }
}
