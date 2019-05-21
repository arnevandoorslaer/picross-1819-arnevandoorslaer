using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cells;
using PiCross;
using System.Windows.Input;
using ViewModel;

namespace View
{
    public abstract class Screen
    {
        public readonly Navigator navigator;

        public Screen(Navigator navigator)
        {
            this.navigator = navigator;
        }

        public void SwitchTo(Screen screen)
        {
            this.navigator.CurrentScreen = screen;
        }
    }

    public class StartScreen : Screen
    {
        public StartScreen(Navigator navigator) : base(navigator)
        {
            Start = new EasyCommand(() => SwitchTo(new SelectScreen(navigator)));
        }

        public ICommand Start { get; }

    }

    public class SelectScreen : Screen
    {
        public SelectScreen(Navigator navigator) : base(navigator)
        {
            selectVM = new SelectVM();
            Start = new EasyCommand(() => SwitchTo(new GameScreen(navigator, selectVM.SelectedPuzzle.Puzzle)));
        }

        public ICommand Start { get; }

        public SelectVM selectVM { get; }
    }


    public class GameScreen : Screen
    {
        public GameScreen(Navigator navigator, Puzzle puzzle) : base(navigator)
        {

            gameVM = new GameVM(puzzle);
            Reset = new EasyCommand(() => SwitchTo(new GameScreen(navigator, puzzle)));
            Again = new EasyCommand(() => SwitchTo(new SelectScreen(navigator)));
        }

        public ICommand Again { get; }

        public ICommand Reset { get; }

        public GameVM gameVM { get; }
    }
}
