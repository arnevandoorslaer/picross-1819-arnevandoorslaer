using Cells;
using DataStructures;
using PiCross;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private readonly IPlayablePuzzle playablePuzzle;

        private Cell<bool> _completed;

        public GameViewModel(Puzzle puzzle)
        {
            var facade = new PiCrossFacade();
            this.playablePuzzle = facade.CreatePlayablePuzzle(puzzle);
            this.Grid = this.playablePuzzle.Grid.Map(square => new SquareVM(square));
            this.RowConstraints = this.playablePuzzle.RowConstraints.Map(row => new RowConstraintsVM(row));
            this.ColumnConstraints = this.playablePuzzle.ColumnConstraints.Map(column => new ColumnConstraintsVM(column));
            this.Completed = this.playablePuzzle.IsSolved;

        }

        public Cell<bool> Completed
        {
            get { return _completed; }
            set { _completed = value;}
        }

        public object Grid { get; }
        public object RowConstraints { get; }
        public object ColumnConstraints { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class ColumnConstraintsVM
    {
        private IPlayablePuzzleConstraints column;

        public ColumnConstraintsVM(IPlayablePuzzleConstraints column)
        {
            this.column = column;
            this.Satisfied = column.IsSatisfied;
        }

        public object Values => column.Values;

        Cell<bool> Satisfied { get; }
    }

    public class RowConstraintsVM
    {
        private IPlayablePuzzleConstraints row;

        public RowConstraintsVM(IPlayablePuzzleConstraints row)
        {
            this.row = row;
            this.Satisfied = row.IsSatisfied;
        }

        public object Values => row.Values;

        Cell<bool> Satisfied { get; }
    }

    public class SquareVM
    {

        private readonly IPlayablePuzzleSquare square;

        public SquareVM(IPlayablePuzzleSquare square)
        {

            this.square = square;
            this.PressLeft = new ClickLeftSquareCommand(this);
            this.PressRight = new ClickRightSquareCommand(this);
        }

        public object Contents => square.Contents;


        public ICommand PressLeft { get; }

        public ICommand PressRight { get; }

        private class ClickLeftSquareCommand : ICommand
        {

            private SquareVM _viewModel;

            public ClickLeftSquareCommand(SquareVM vm)
            {
                _viewModel = vm;
            }


            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                if (_viewModel.square.Contents.Value == Square.FILLED)
                {
                    _viewModel.square.Contents.Value = Square.UNKNOWN;
                }
                else
                {
                    _viewModel.square.Contents.Value = Square.FILLED;
                }
            }
        }

        private class ClickRightSquareCommand : ICommand
        {
            private SquareVM _viewModel;

            public ClickRightSquareCommand(SquareVM vm)
            {
                _viewModel = vm;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                if (_viewModel.square.Contents.Value == Square.EMPTY)
                {
                    _viewModel.square.Contents.Value = Square.UNKNOWN;
                }
                else
                {
                    _viewModel.square.Contents.Value = Square.EMPTY;
                }
            }
        }
    }
}