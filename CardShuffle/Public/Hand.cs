using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShuffle.Public
{
    class Hand
    {
        private Card[] cardsInHand;
        private int handPosition;
        private int[] selected;

        public Hand(int numCards, int swaps)
        {
            cardsInHand = new Card[numCards];
            handPosition = 0;
            selected = new int[swaps];
            
        }
        public Card[] GetHand()
        {
            return cardsInHand;
        }

        public void AddCard(Card card)
        {
            if(handPosition < cardsInHand.Length)
            {
                cardsInHand[handPosition] = card;
                handPosition++;
            }
        }

        public void ReplaceCard(Card card, int index)
        {
            cardsInHand[index] = card;
        }

        // ***** Not sure if needed *****
        public void AddSelected(int index)
        {
           
        }


    }
}
