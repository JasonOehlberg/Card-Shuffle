// Author: Jason Oehlberg
// Project: Deck of Cards
// Date: 2018.11.19


namespace CardShuffle.Public
{
    class Hand
    {
        // Fields for the Hand class
        // It is a container for each hand after the deal
        private Card[] cardsInHand;
        private int handPosition;
        // hold the selected cards for a swap
        private int[] selected;
        // can hold the name of the player
        private string name;

        // constructor for the Hand
        public Hand(string name, int numCards, int swaps)
        {
            this.name = name;
            cardsInHand = new Card[numCards];
            handPosition = 0;
            selected = new int[swaps];
            
        }

        // returns the Hand Card array
        public Card[] GetHand()
        {
            return cardsInHand;
        }

        // method adds a single card to the Card array
        public void AddCard(Card card)
        {
            if(handPosition < cardsInHand.Length)
            {
                cardsInHand[handPosition] = card;
                handPosition++;
            }
            else
            {
                handPosition = 0;
                cardsInHand[handPosition] = card;
            }
        }

        // this replaces the card in the hand with a swapped card
        public void ReplaceCard(Card card, int index)
        {
            cardsInHand[index] = card;
        }

        // Method gets the name of the player
        public string GetName()
        {
            return name;
        }


    }
}
