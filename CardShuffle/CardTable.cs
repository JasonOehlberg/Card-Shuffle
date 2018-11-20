// Author: Jason Oehlberg
// Project: Deck of Cards
// Date: 2018.11.19


using CardShuffle.Public;
using System;

using System.Drawing;
using System.Windows.Forms;
using CardShuffle.Properties;
using System.Threading.Tasks;


namespace CardShuffle
{

    public partial class CardTable : Form
    {
        // The constants for the game at the CardTable
        private const int CARDS_IN_HAND = 5;
        // number of swaps a player can make
        private const int NUM_SWAPS = 2;
        // number of players
        private const int PLAYERS = 2;

        // the deck for the game
        private Deck deck;
        // the hand array for each player
        Hand[] players;
        // keeps track of who has just played
        int currentHand = 0;
        // keeps track of how many cards are chosen for the swap
        int clicks = 0;

        // initializes the deck and shuffles and creates an instance of each Hand
        public CardTable()
        {
            InitializeComponent();
            deck = new Deck();
            deck.ShuffleCards();

            players = new Hand[PLAYERS] 
            {
                new Hand("Player One",CARDS_IN_HAND, NUM_SWAPS),
                new Hand("Player Two",CARDS_IN_HAND, NUM_SWAPS)
            };
        }

        // Default values when the form loads
        private void CardTable_Load(object sender, EventArgs e)
        {
            pbDrawDeck.SizeMode = PictureBoxSizeMode.StretchImage;
            pbDrawDeck.Image = Resources.blue_back;
            Icon = Resources.AceHigh;
            Text = "Card Shuffle";
        }

        // a Async method to put a delay on displaying each card in both hands
        async Task PutTaskDelay()
        {
            await Task.Delay(500);
        }

        // Gets all of the Card string display Labels for Handone
        private Label[] GetHandOneLabels()
        {
            var labels = new Label[CARDS_IN_HAND] { lblHandOne1, lblHandOne2, lblHandOne3, lblHandOne4, lblHandOne5 };
            return labels;
        }

        // Gets all of the Card string display Labels for Handtwo
        private Label[] GetHandTwoLabels()
        {
            var labels = new Label[CARDS_IN_HAND] { lblHandTwo1, lblHandTwo2, lblHandTwo3, lblHandTwo4, lblHandTwo5 };
            return labels;
        }

        // Displays the images assigned for each dealt card in a hand to a picturebox
        private void DisplayCardDetails()
        {
            for (var i = 0; i < CARDS_IN_HAND; i++)
            {
                GetHandOneLabels()[i].Text = players[0].GetHand()[i].ToString();
                GetHandOneLabels()[i].ForeColor = Color.White;
                GetHandTwoLabels()[i].Text = players[1].GetHand()[i].ToString();
                GetHandTwoLabels()[i].ForeColor = Color.White;
            }
        }
    
        // rests the form to default values for another deal
        private void ClearTable()
        {
            DefaultMessage();
            currentHand = 0;
            clicks = 0;
            for (var i = 0; i < CARDS_IN_HAND; i++)
            {
                GetHandOneLabels()[i].Text = String.Empty;
                GetHandTwoLabels()[i].Text = String.Empty;
                ShowHandOne()[i].Image = null;
                ShowHandOne()[i].BackColor = Color.Transparent;
                ShowHandTwo()[i].Image = null;
                ShowHandTwo()[i].BackColor = Color.Transparent;
            }
        }

        // gets the PictureBoxes that will display the images for handone
        private PictureBox[] ShowHandOne()
        {
            var pictureBoxes = new PictureBox[CARDS_IN_HAND] { pbHandOne1, pbHandOne2, pbHandOne3, pbHandOne4, pbHandOne5 };
            return pictureBoxes;
        }

        // gets the PictureBoxes that will display the images for handtwo
        private PictureBox[] ShowHandTwo()
        {
            var pictureBoxes = new PictureBox[CARDS_IN_HAND] { pbHandTwo1, pbHandTwo2, pbHandTwo3, pbHandTwo4, pbHandTwo5 };
            return pictureBoxes;
        }

        // gets a card array
        private Card[] GetHand(Card[] cards)
        {
            return cards;
        }
    
        // gets the appropriate labels based on whos turn it is
        private Label[] GetHandLabel()
        {
            if (currentHand == 0)
                return GetHandOneLabels();
            else
                return GetHandTwoLabels();
        }


        // gets the appropriate PictureBoxes based on whos turn it is
        private PictureBox[] GetPictureBox()
        {
            if (currentHand == 0)
                return ShowHandOne();
            else
                return ShowHandTwo();
        }

        // sets the Click methods for the appropriate hand and remove all the clicks from the previous hand
        private void SetImageClicks()
        {
            RemoveImageClicks();
            foreach (PictureBox pb in GetPictureBox())
            {
                pb.Click += ClickOnImage;
            }
        }

        // removes the Click methods so that a swap cannot be chosen after the swap is made
        private void RemoveImageClicks()
        {
           foreach (PictureBox pb in GetPictureBox())
           {
                pb.Click -= ClickOnImage;
                pb.BackColor = Color.Transparent;
           }

            clicks = 0;
        }

        // The defualt message for the output labels
        private void DefaultMessage()
        {
            lblOutput1.Text = $"{players[currentHand].GetName()}";
            lblOutput2.Text = "Please select cards to trade";
        }

        // Method is activated when an image is clicked
        private void ClickOnImage(object sender, EventArgs eventArgs)
        {
            // grabs the picturebox that was clicked
            var picBox = (PictureBox)sender;
           // Enables the Swap button
            btnSwap.Enabled = true;
            // sends a message to the output
            lblOutput2.Text = $"Press 'Swap' to switch up to {NUM_SWAPS} cards.";

            // selects or deselects an image based on the numbers of clicks available
            if (clicks < 2)
            {
                if (picBox.BackColor == Color.Red)
                {
                    clicks--;
                    picBox.BackColor = Color.Transparent;
                }
                else if (picBox.BackColor == Color.Transparent)
                {
                    clicks++;
                    picBox.BackColor = Color.Red;
                }
            }
            // this executes if the PictureBox has already been selected
            else if (clicks == 2 && picBox.BackColor == Color.Red)
            {
                clicks--;
                picBox.BackColor = Color.Transparent;
            }
            else if (clicks > 2)
            {
                clicks--;
            }
        }

        // Method deals cards to each hand and displays them on the form
        private async void btnDeal_Click(object sender, EventArgs e)
        {
            // clears first
            ClearTable();
            pbDiscard.Image = null;
            for (var i = 0; i < CARDS_IN_HAND; i++)
            {
                // delay on dealing
                await PutTaskDelay();
                players[0].AddCard(deck.DealCard());
                ShowHandOne()[i].SizeMode = PictureBoxSizeMode.StretchImage;
                ShowHandOne()[i].Image = players[0].GetHand()[i].Front;

                // delay on dealing
                await PutTaskDelay();
                players[1].AddCard(deck.DealCard());
                ShowHandTwo()[i].SizeMode = PictureBoxSizeMode.StretchImage;
                ShowHandTwo()[i].Image = players[1].GetHand()[i].Front;
            }
            // delay on displaying the label messages and sets the image clicks for the first hand
            await PutTaskDelay();
            DisplayCardDetails();
            DefaultMessage();
            SetImageClicks();

        }

        // Method displays the current card in the discard pile
        private void btnFlip_Click(object sender, EventArgs e)
        {
            pbDiscard.SizeMode = PictureBoxSizeMode.StretchImage;
            pbDiscard.Image = deck.getCurrentCard().Front;
        }

        // Method swaps the selected cards in a given hand
        private void btnSwap_Click(object sender, EventArgs e)
        {
          
            for(int i = 0; i < GetPictureBox().Length; i++)
            {
                // if the PictureBox's background color is red -- or selected
                if(GetPictureBox()[i].BackColor == Color.Red)
                {
                    // replaces the selected in the hand with the currentCard in the deck
                      players[currentHand].GetHand()[i] = deck.DealCard();
                      GetPictureBox()[i].Image = players[currentHand].GetHand()[i].Front;
                      GetHandLabel()[i].Text = players[currentHand].GetHand()[i].ToString();
                }
            }
            // removes the Click events on the hand that just swapped
            RemoveImageClicks();
            btnSwap.Enabled = false;
            // what to do if this hand is the first hand
            if (currentHand == 0)
            {
                currentHand++;
                SetImageClicks();
                DefaultMessage();
            }
            else
                // sets currenthand back to default
                currentHand = 0;
        }
                
    }
}

