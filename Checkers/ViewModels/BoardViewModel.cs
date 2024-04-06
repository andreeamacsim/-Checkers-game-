using Checkers.Models;
using Checkers.Services;
using Checkers.Command;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Checkers.Views;


namespace Checkers.ViewModels
{
    public class BoardViewModel : BaseViewModel
    {
        
        private GameStatistics _statistics = new GameStatistics();

        private readonly GameViewModel _gameService;

        private ObservableCollection<Cell> _cells;
        public string CreatorName { get; } = "Creator's name: Macsim Andreea-Lavinia";
        public string EmailAddress { get; } = "Institutional mail: andreea.macsim@student.unitbv.ro";
        public string Group { get; } = "Group number: 10LF223";
        public string GameDescription { get; } = "The game is a checkers game implemented in C# with a WPF user interface, following the MVVM design pattern. It features two sets of pieces: white and red. The game board consists of 8 rows and 8 columns.\r\n\r\nInitial Setup:\r\nThe game starts with the board set up as shown in Figure 1. The player with the red pieces makes the first move, followed by alternating moves between players. The application visually indicates which player is currently making a move and displays the number of pieces remaining on the board for each player.\r\n\r\nTypes of Moves:\r\n\r\nSimple Move: A piece moves one square diagonally forward. If a piece reaches the opponent's back row, it becomes a \"king\" and gains the ability to move both forward and backward diagonally.\r\nJump Over Opponent's Piece: If a player's piece has an opponent's piece diagonally adjacent to it and an empty square immediately beyond, the player can jump over the opponent's piece, capturing it.\r\nMultiple Jumps: If a player captures an opponent's piece and another opponent's piece is immediately adjacent and can be captured, the player can continue jumping in a chain until no further captures are possible.\r\nPlayers can only perform multiple jumps if the option is enabled at the beginning of the game.\r\n\r\nEnd of Game:\r\nThe game ends when one player has no more pieces on the board. The opponent is declared the winner.";

        public ObservableCollection<Cell> Cells
        {
            get { return _cells; }

            set
            {
                _cells = value;
                OnPropertyChanged(nameof(Cells));
            }
        }


        public PlayerType CurrentPlayer
        {
            get { return _gameService.CurrentPlayer; }

            set
            {
                if (_gameService.CurrentPlayer != value)
                {
                    _gameService.CurrentPlayer = value;
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
                if (_allowMultipleJump != value)
                {
                    _allowMultipleJump = value;
                    OnPropertyChanged(nameof(AllowMultipleJump));
                    
                }
            }
        }

        public BoardViewModel()
        {
            _gameService = new GameViewModel(_statistics);

            Cells = new ObservableCollection<Cell>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    bool isBlack = (i + j) % 2 == 1;
                    if (i < 3 && isBlack)
                    {
                        Cells.Add(new Cell(isBlack, i, j, CheckerTypes.WhitePawn));
                    }
                    else if (i > 4 && isBlack)
                    {
                        Cells.Add(new Cell(isBlack, i, j, CheckerTypes.RedPawn));
                    }

                    else
                    {
                        Cells.Add(new Cell(isBlack, i, j));
                    }
                }
            }

            MovePieceCommand = new RelayCommand(MovePiece, isMoveValid);

            ClickCellCommand = new RelayCommand(ClickCell);

            NewGameCommand = new RelayCommand(NewGame);
            SaveGameCommand = new RelayCommand(SaveGame);
            OpenGameCommand = new RelayCommand(OpenGame);
            ShowStatisticsCommand = new RelayCommand(ShowStatistics);
        }


        public ICommand ClickCellCommand { get; set; }

        public ICommand MovePieceCommand { get; set; }

        public ICommand NewGameCommand { get; set; }
        public ICommand SaveGameCommand { get; set; }
        public ICommand OpenGameCommand { get; set; }
        public ICommand ShowStatisticsCommand { get; set; }

        public void ClickCell(object parameter)
        {

            var cell = parameter as Cell;

            if (cell != null)
            {
                _gameService.CellClicked(cell);
            }
        }

        public void MovePiece(object parameter)
        {

            _gameService.MovePiece(Cells);
        }

        public bool isMoveValid(object parameter)
        {
            return _gameService.IsMoveValid(Cells);
        }

        private void NewGame(object parameter)
        {
            Cells.Clear();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    bool isBlack = (i + j) % 2 == 1;
                    if (i < 3 && isBlack)
                    {
                        Cells.Add(new Cell(isBlack, i, j, CheckerTypes.WhitePawn));
                    }
                    else if (i > 4 && isBlack)
                    {
                        Cells.Add(new Cell(isBlack, i, j, CheckerTypes.RedPawn));
                    }
                    else
                    {
                        Cells.Add(new Cell(isBlack, i, j));
                    }
                }
            }
        }
        private void SaveGame(object parameter)
        {
            try
            {
               
                var gameStateStringBuilder = new StringBuilder();

                foreach (var cell in Cells)
                {
                    gameStateStringBuilder.AppendLine($"{cell.Line},{cell.Column},{cell.IsBlack},{cell.Content}");
                }

                
                gameStateStringBuilder.AppendLine($"{CurrentPlayer},{AllowMultipleJump}");

                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                    DefaultExt = ".txt"
                };

                bool? result = saveFileDialog.ShowDialog();
                if (result == true)
                {
                    
                    string filePath = saveFileDialog.FileName;
                    File.WriteAllText(filePath, gameStateStringBuilder.ToString());
                    
                     MessageBox.Show("Game saved successfully.");
                }
            }
            catch (Exception ex)
            {
              
                 MessageBox.Show($"Error saving game: {ex.Message}");
            }
        }

        
        private void OpenGame(object parameter)
        {
            try
            {
                var openFileDialog = new Microsoft.Win32.OpenFileDialog
                {
                    Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                    DefaultExt = ".txt"
                };

                bool? result = openFileDialog.ShowDialog();
                if (result == true)
                {
                    
                    string filePath = openFileDialog.FileName;

                    string gameStateString = File.ReadAllText(filePath);

                    string[] lines = gameStateString.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < lines.Length - 1; i++)
                    {
                        string[] parts = lines[i].Split(',');
                        int line = int.Parse(parts[0]);
                        int column = int.Parse(parts[1]);
                        bool isBlack = bool.Parse(parts[2]);
                        CheckerTypes content = (CheckerTypes)Enum.Parse(typeof(CheckerTypes), parts[3]);

                        var cell = Cells.FirstOrDefault(c => c.Line == line && c.Column == column);
                        if (cell != null)
                        {
                            cell.IsBlack = isBlack;
                            cell.Content = content;
                            cell.IsOccupied = content != CheckerTypes.None;
                        }
                    }

                    string[] lastLineParts = lines[lines.Length - 1].Split(',');
                    CurrentPlayer = (PlayerType)Enum.Parse(typeof(PlayerType), lastLineParts[0]);
                    AllowMultipleJump = bool.Parse(lastLineParts[1]);

               
                    MessageBox.Show("Game loaded successfully.");
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Error opening game: {ex.Message}");
            }
        }

        private void UpdateStatistics(PlayerType winner, int piecesRemaining)
        {
            if (winner == PlayerType.White)
            {
                _statistics.WhiteWins++;
            }
            else if (winner == PlayerType.Red)
            {
                _statistics.RedWins++;
            }

            if (piecesRemaining > _statistics.MaxPiecesRemaining)
            {
                _statistics.MaxPiecesRemaining = piecesRemaining;
            }

           
        }
        private void ShowStatistics(object parameter)
        {
            var statisticsWindow = new StatisticsWindow();
            statisticsWindow.DataContext = _statistics;
            statisticsWindow.ShowDialog();
        }

    }
}
