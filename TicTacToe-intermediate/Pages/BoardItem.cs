using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Pages
{
    public class BoardItem : INotifyPropertyChanged
    {
        public bool T = false;
        public bool B = false;
        public bool L = false;
        public bool R = false;

        public BoardItem(int index = 0, int x = 0, int y = 0, bool t = false, bool b = false, bool l = false, bool r = false, char ch = ' ')
        {
            Index = index;
            X = x;
            Y = y;
            T = t;
            B = b;
            L = l;
            R = r;
            Ch = ch;
        }

        private int index = 0;
        public int Index
        {
            get => index;
            set
            {
                if (index == value) return;
                index = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Index)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IndexText)));
            }
        }

        public string IndexText => Index.ToString();

        private int x = 0;
        public int X
        {
            get => x;
            set
            {
                if (x == value) return;
                x = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(X)));
            }
        }

        private int y = 0;
        public int Y
        {
            get => y;
            set
            {
                if (y == value) return;
                y = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Y)));
            }
        }

        private char ch = ' ';
        public char Ch
        {
            get => ch;
            set
            {
                if (ch == value) return;
                ch = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ch)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
