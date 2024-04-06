using Checkers.Models;

using Checkers.Services;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;

namespace Checkers.ViewModels
{
    public class GameViewModel: BaseViewModel
    {
        private (int line, int column)? _currentCell, _newCell;

        private PlayerType _currentPlayer = PlayerType.Red;

        public PlayerType CurrentPlayer
        {
            get { return _currentPlayer; }

            set
            {
                if (_currentPlayer != value)
                {
                    _currentPlayer = value;
                    OnPropertyChanged(nameof(CurrentPlayer));  
                }
            }
        }
        private bool _allowMultipleJump;

        public bool AllowMultipleJump
        {
            get { return _allowMultipleJump; }
            set
            {
                _allowMultipleJump = value;
                OnPropertyChanged(nameof(AllowMultipleJump)); 
            }
        }
        private GameStatistics _statistics;
        public GameViewModel(GameStatistics statistics)
        {
            LoadStatistics();
            _statistics = statistics;
            _currentCell = null;
            _newCell = null;
            _currentPlayer = PlayerType.Red;
        }

        public bool IsMoveValid(ObservableCollection<Cell> cells)
        {
            if (_currentCell == null || _newCell == null)
                return false;
            return IsSimpleMoveValid(cells) || IsJumpValid(cells); ;
        }

        private bool IsSimpleMoveValid(ObservableCollection<Cell> cells)
        {
            if (_currentCell == null || _newCell == null)
                return false;

            int rowDifference = _newCell.Value.line - _currentCell.Value.line;
            int currentCellIndex = _currentCell.Value.line * 8 + _currentCell.Value.column;
            bool isKing = cells[currentCellIndex].Content == CheckerTypes.RedKing || cells[currentCellIndex].Content == CheckerTypes.WhiteKing;

          
            if (isKing)
                return Math.Abs(rowDifference) == 1 && Math.Abs(_currentCell.Value.column - _newCell.Value.column) == 1;

          
            if (_currentPlayer == PlayerType.Red)
                return rowDifference < 0 && Math.Abs(_currentCell.Value.column - _newCell.Value.column) == 1;
            else if (_currentPlayer == PlayerType.White)
                return rowDifference > 0 && Math.Abs(_currentCell.Value.column - _newCell.Value.column) == 1;

            return false;
        }
        private bool IsJumpValid(ObservableCollection<Cell> cells)
        {
            if (_currentCell == null || _newCell == null)
                return false;

            int rowDifference = _newCell.Value.line - _currentCell.Value.line;
            int colDifference = _newCell.Value.column - _currentCell.Value.column;

           
            if (Math.Abs(rowDifference) == 2 && Math.Abs(colDifference) == 2)
            {
                int midRow = _currentCell.Value.line + rowDifference / 2;
                int midCol = _currentCell.Value.column + colDifference / 2;
                int opponentCellIndex = midRow * 8 + midCol;

                
                if (cells[opponentCellIndex].IsOccupied &&
                    Checker.GetPlayerTypeFromChecker(cells[opponentCellIndex].Content) != _currentPlayer)
                {
                    
                    return !cells[_newCell.Value.line * 8 + _newCell.Value.column].IsOccupied;
                }
            }
            return false;
        }


        private void AssignCurrentCell(Cell cell)
        {
            if (Checker.GetPlayerTypeFromChecker(cell.Content) == _currentPlayer)
            {
                _currentCell = (cell.Line, cell.Column);
            }
        }

        private void AssignNewCell(Cell cell)
        {
            if (Checker.GetPlayerTypeFromChecker(cell.Content) == PlayerType.None)
            {
                _newCell = (cell.Line, cell.Column);
            }
        }

        public void AssignCheckerType(IList<Cell> cells, (int line, int column) cell, CheckerTypes checkerType)
        {
            var cellIndex = cell.line * 8 + cell.column;
            cells[cellIndex].Content = checkerType;
        }

        int redWins = 0;
        int whiteWins = 0;


        public void MovePiece(ObservableCollection<Cell> cells)
        {
            var currentCellIndex = _currentCell.Value.line * 8 + _currentCell.Value.column;
            var newCellIndex = _newCell.Value.line * 8 + _newCell.Value.column;

            if (IsJumpValid(cells))
            {
                PerformJump(cells, currentCellIndex, newCellIndex);
                
                CheckForKing(cells, newCellIndex);
                CheckForAdditionalJumps(cells, _newCell.Value);
            }
            else if (IsSimpleMoveValid(cells) && !cells[newCellIndex].IsOccupied)
            {
                PerformRegularMove(cells, currentCellIndex, newCellIndex);
                
                FinalizeTurn(cells, _newCell.Value);
            }
        }

        private void FinalizeTurn(ObservableCollection<Cell> cells, (int line, int column) cell)
        {
            var cellIndex = cell.line * 8 + cell.column;
            CheckForKing(cells, cellIndex); 

            int redCount = cells.Count(c => c.Content == CheckerTypes.RedPawn || c.Content == CheckerTypes.RedKing);
            int whiteCount = cells.Count(c => c.Content == CheckerTypes.WhitePawn || c.Content == CheckerTypes.WhiteKing);

            if (redCount == 0 || whiteCount == 0)
            {
                DeclareWinner(redCount, whiteCount);
            }
            else
            {
                SwitchPlayer();
            }
        }

        private void CheckForKing(ObservableCollection<Cell> cells, int cellIndex)
        {
            var cell = cells[cellIndex];
            
            if ((cell.Content == CheckerTypes.RedPawn && cell.Line == 0) || (cell.Content == CheckerTypes.WhitePawn && cell.Line == 7))
            {
                cells[cellIndex].Content = cell.Content == CheckerTypes.RedPawn ? CheckerTypes.RedKing : CheckerTypes.WhiteKing;
            }
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == PlayerType.Red ? PlayerType.White : PlayerType.Red;
            _currentCell = null;
            _newCell = null;
        }


        private void CheckForAdditionalJumps(ObservableCollection<Cell> cells, (int line, int column) cell)
        {
            _currentCell = cell;
            _newCell = null; 
            

            foreach (var direction in new[] { (1, 1), (1, -1), (-1, 1), (-1, -1) })
            {
                var potentialNewCell = (cell.line + direction.Item1 * 2, cell.column + direction.Item2 * 2);
                if (potentialNewCell.Item1 >= 0 && potentialNewCell.Item1 < 8 && potentialNewCell.Item2 >= 0 && potentialNewCell.Item2 < 8)

                {
                    _newCell = potentialNewCell;
                    if (IsJumpValid(cells))
                    {
                       
                        return;
                    }
                }
            }

            FinalizeTurn(cells, cell);
        }


        private void DeclareWinner(int redCount, int whiteCount)
        {
            int piecesRemaining = redCount > 0 ? redCount : whiteCount; 

            
            if (redCount == 0)
            {
                _statistics.WhiteWins++;
                MessageBox.Show("White player wins!");
            }
            else if (whiteCount == 0)
            {
                _statistics.RedWins++;
                MessageBox.Show("Red player wins!");
            }

            
            if (piecesRemaining > _statistics.MaxPiecesRemaining)
            {
                _statistics.MaxPiecesRemaining = piecesRemaining;
            }

            SaveStatistics();
            ResetGameBoard();
            MessageBox.Show("The game has ended. Starting a new game.");
        }


        private void ResetGameBoard()
        {
            _currentCell = null;
            _newCell = null;
        }


        private void SaveStatistics()
        {
            string path = @"C:\Users\Andreea\Desktop\Checkers\file.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
                {
                    writer.WriteLine($"Red player wins: {_statistics.RedWins}");
                    writer.WriteLine($"White player wins: {_statistics.WhiteWins}");
                    writer.WriteLine($"Max pieces remaining for a winner: {_statistics.MaxPiecesRemaining}");
                    Console.WriteLine("Statistics saved successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
            }
        }

        private void LoadStatistics()
        {
            _statistics = new GameStatistics();
            string path = @"C:\Users\Andreea\Desktop\Checkers\file.txt";
            if (!File.Exists(path))
            {
                _statistics = new GameStatistics(); 
                return;
            }

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(':');
                        if (parts.Length == 2)
                        {
                            switch (parts[0].Trim())
                            {
                                case "Red player wins":
                                    _statistics.RedWins = int.Parse(parts[1].Trim());
                                    break;
                                case "White player wins":
                                    _statistics.WhiteWins = int.Parse(parts[1].Trim());
                                    break;
                                case "Max pieces remaining for a winner":
                                    _statistics.MaxPiecesRemaining = int.Parse(parts[1].Trim());
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading statistics: {ex.Message}");
                _statistics = new GameStatistics(); 
            }
        }

        private void PerformRegularMove(ObservableCollection<Cell> cells, int currentCellIndex, int newCellIndex)
        {

            cells[newCellIndex].Content = cells[currentCellIndex].Content;
            cells[newCellIndex].IsOccupied = true;
            cells[currentCellIndex].IsOccupied = false;
            cells[currentCellIndex].Content = CheckerTypes.None;
        }

        private void PerformJump(ObservableCollection<Cell> cells, int currentCellIndex, int newCellIndex)
        {

            int opponentCellIndex = (_newCell.Value.line + _currentCell.Value.line) / 2 * 8 +
                                   (_newCell.Value.column + _currentCell.Value.column) / 2;
            if (cells[opponentCellIndex].IsOccupied &&
                cells[opponentCellIndex].Content != cells[currentCellIndex].Content)
            {
                cells[newCellIndex].Content = cells[currentCellIndex].Content;
                cells[newCellIndex].IsOccupied = true;
                cells[currentCellIndex].IsOccupied = false;
                cells[currentCellIndex].Content = CheckerTypes.None;


                cells[opponentCellIndex].Content = CheckerTypes.None;
                cells[opponentCellIndex].IsOccupied = false;
            }
            else
            {
                MessageBox.Show("Invalid move. You can't jump over an empty space or your own piece.");
                return;
            }
        }


        public void CellClicked(Cell cell)
        {
            if (cell.IsOccupied)
            {
                AssignCurrentCell(cell);
            }
            else
            {
                AssignNewCell(cell);
            }
        }


    }
}
