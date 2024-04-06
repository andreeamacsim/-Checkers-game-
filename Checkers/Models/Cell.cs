using System.ComponentModel;

namespace Checkers.Models
{
    public class Cell : INotifyPropertyChanged
    {
        private bool _isBlack;

        private bool _isOccupied;

        private CheckerTypes _content;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Line { get; set; }

        public int Column { get; set; }

        public bool IsBlack
        {
            get { return _isBlack; }

            set
            {
                _isBlack = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBlack)));
            }
        }

        public bool IsOccupied
        {
            get { return _isOccupied; }

            set
            {
                _isOccupied = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsOccupied)));
            }
        }

        public CheckerTypes Content
        {
            get { return _content; }

            set
            {
                _content = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Content)));
            }
        }

        public Cell(bool isBlack, int line, int column, CheckerTypes content = default)
        {
            Line = line;
            Column = column;
            IsBlack = isBlack;
            if (content != default)
            {
                IsOccupied = true;
            }
            else
            {
                IsOccupied = false;

            }
            Content = content;
        }
    }
}
