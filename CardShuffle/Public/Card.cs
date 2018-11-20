// Author: Jason Oehlberg
// Project: Deck of Cards
// Date: 2018.11.19


using CardShuffle.Properties;
using System.Drawing;

namespace CardShuffle.Public
{
    class Card
    {
        // The properties for the Card Class
        // Each Card has a Face and Suit string name, a value for comparison and front and back Bitmap images
        public Bitmap Front { set;  get; }
        public Bitmap Back { get; }

        private int Value {  get; }
        private string Face { get; }
        private string Suit { get; }
        
        // constructor for each Card
        public Card(string face, string suit, int value, Image front)
        {
            Face = face;
            Suit = suit;
            Value = value;
            Front = new Bitmap(front);
            Back = new Bitmap(Resources.blue_back);
        }

        // overriden ToString method for each card
        public override string ToString()
        {
            return $" {Face} of {Suit}";
        }
    }
}
