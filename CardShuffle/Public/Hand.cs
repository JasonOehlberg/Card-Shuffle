using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShuffle.Public
{
    class Hand
    {
        private const int HAND_SIZE = 5;
        public Card[] Cards { set { } get { return Cards; } }
        public int selectedCards = 0;

        public Hand(Card[] cards)
        {
            Cards = new Card[HAND_SIZE];
            for(var i = 0; i < cards.Length; i++)
            {
                Cards[i] = cards[i];
            }
        }
    }
}
