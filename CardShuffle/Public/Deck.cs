using CardShuffle.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CardShuffle.Public
{
    class Deck
    {
        private const int DECK_COUNT = 52;
        private static Random rand = new Random();

        private Card[] cards = new Card[DECK_COUNT];
        public Dictionary<Card, Image> Display { get; }

        public int currentCard = 0;
      
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

        public void generateCards(Dictionary<string, int> faceData, string[] suits)
        {
            int index = 0;
            int picNum = 1;
            foreach( string suit in suits)
            {
                foreach(KeyValuePair<string, int> items in faceData)
                {
                    char letter = suit.ToCharArray()[0];
                    cards[index] = new Card(items.Key, suit, items.Value, (Bitmap)Resources.ResourceManager.GetObject("_" + picNum + letter));
                    picNum++;
                    index++;
                }
                picNum = 1;
            }

        }
        public void shuffleCards()
        {
            currentCard = 0;
            
            for(var i = 0; i < cards.Length; ++i)
            {
                var j = rand.Next(DECK_COUNT);

                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }
        
        public Card DealCard()
        {
            if (currentCard < cards.Length)
            {
                return cards[currentCard++];
            }
            else
                return null;
        }

        public Card getCurrentCard()
        {
            return cards[currentCard];
        }

    }
        
}
