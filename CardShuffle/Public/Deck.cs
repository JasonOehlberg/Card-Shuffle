// Author: Jason Oehlberg
// Project: Deck of Cards
// Date: 2018.11.19


using CardShuffle.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace CardShuffle.Public
{
    class Deck
    {
        // A constant value for the size of the deck
        private const int DECK_COUNT = 52;
        // a random instance to shuffle the cards
        private static Random rand = new Random();

        // Card array for the Deck
        private Card[] cards = new Card[DECK_COUNT];

        // Holds each card and the front image to be set for each Card
        public Dictionary<Card, Image> Display { get; }

        // The currentCard is the next card in the stack
        public int currentCard = 0;
        // A value to hold wether the deck has been dealt or not
        public bool deckDealt = false;
      
        // Constructor sets up the cards in the Dictionary
        public Deck()
        {
            var faceData = new Dictionary<string, int>() {
                { "Ace", 1},
                { "Deuce", 2 },
                { "Three",3 },
                { "Four",4 },
                { "Five", 5 },
                { "Six", 6 },
                { "Seven", 7 },
                { "Eight", 8 },
                { "Nine", 9 },
                { "Ten", 10 },
                { "Jack", 11 },
                { "Queen", 12 },
                { "King", 13 }
            };

            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            generateCards(faceData, suits);
        }

        
        // method generates each card for the Card array in the Deck
        public void generateCards(Dictionary<string, int> faceData, string[] suits)
        {
            int index = 0;
            int picNum = 1;
            foreach (string suit in suits)
            {
                foreach (KeyValuePair<string, int> items in faceData)
                {
                    char letter = suit.ToCharArray()[0];
                    cards[index] = new Card(items.Key, suit, items.Value, (Bitmap)Resources.ResourceManager.GetObject("_" + picNum + letter));
                    picNum++;
                    index++;
                }
                picNum = 1;
            }
        }

        // Shuffles the array using a randomly generated number
        public void ShuffleCards()
        {
            // ********** Look at Logic ***************
            // not sure if this is necessary
            /*if (deckDealt)
            {
                // this was intended to shuffle the cards and keep the last two at position
                // However, when a swap is made this throws off the number
                // it does not break but need revising
                currentCard = 2;
                Card temp = cards[cards.Length - 1];
                cards[cards.Length - 1] = cards[0];
                cards[0] = temp;
                temp = cards[cards.Length];
                cards[cards.Length] = cards[1];
                cards[1] = temp; 
            }
            else
            {
                currentCard = 0;
            }*/
            
            for(var i = currentCard; i < cards.Length; ++i)
            {
                var j = rand.Next(DECK_COUNT);

                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
            deckDealt = true;
        }
        
        // Deals the next card and moves the currentCard position in the array
        public Card DealCard()
        {
            if (currentCard < cards.Length)
            {
                Debug.WriteLine("1: " + currentCard);
                return cards[currentCard++];
            }
            else
            {
                ShuffleCards();
                currentCard = 0;
                return cards[currentCard++];
            }
        }

        
        // retries the Card at the current position for fliping a card to the discard pile
        public Card getCurrentCard()
        {
            return cards[currentCard];
        }

    }
        
}
