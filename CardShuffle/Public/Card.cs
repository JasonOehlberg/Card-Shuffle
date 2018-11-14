using CardShuffle.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShuffle.Public
{
    class Card
    {
        public Bitmap Front { set;  get; }
        public Bitmap Back { get; }

        private int Value {  get; }
        private string Face { get; }
        private string Suit { get; }
        
        
        public Card(string face, string suit, int value, Image front)
        {
            Face = face;
            Suit = suit;
            Value = value;
            Front = new Bitmap(front);
            Back = new Bitmap(Resources.blue_back);
        }

        public override string ToString()
        {
            return $" {Face} of {Suit}";
        }
    }
}
