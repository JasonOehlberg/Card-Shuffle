using CardShuffle.Public;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using CardShuffle.Properties;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CardShuffle
{
    public partial class CardTable : Form
    {
        private const int CARDS_IN_HAND = 5;
        private const int NUM_SWAPS = 2;
        private const int PLAYERS = 2;

        private Deck deck;
        // ArrayList images = new ArrayList();
        Random rand = new Random();
        //Card[] hand1 = new Card[5];
        //Card[] hand2 = new Card[5];
        Hand[] players;
        
        int clicks = 0;



        public CardTable()
        {
            InitializeComponent();
            deck = new Deck();
            deck.ShuffleCards();

            players = new Hand[PLAYERS] 
            {
                new Hand(CARDS_IN_HAND, NUM_SWAPS),
                new Hand(CARDS_IN_HAND, NUM_SWAPS)
            };
        }

        private void CardTable_Load(object sender, EventArgs e)
        {
            pbDrawDeck.SizeMode = PictureBoxSizeMode.StretchImage;
            pbDrawDeck.Image = Resources.blue_back;
        }

        async Task PutTaskDelay()
        {
            await Task.Delay(500);
        }

        private Label[] GetHandOneLabels()
        {
            var labels = new Label[5] { lblHandOne1, lblHandOne2, lblHandOne3, lblHandOne4, lblHandOne5 };
            return labels;
        }

        private Label[] GetHandTwoLabels()
        {
            var labels = new Label[5] { lblHandTwo1, lblHandTwo2, lblHandTwo3, lblHandTwo4, lblHandTwo5 };
            return labels;
        }

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
    
        private void ClearTable()
        {
            for (var i = 0; i < CARDS_IN_HAND; i++)
            {
                GetHandOneLabels()[i].Text = String.Empty;
                GetHandTwoLabels()[i].Text = String.Empty;
                ShowHandOne()[i].Image = null;
                ShowHandTwo()[i].Image = null;
            }
        }

        private PictureBox[] ShowHandOne()
        {
            var pictureBoxes = new PictureBox[5] { pbHandOne1, pbHandOne2, pbHandOne3, pbHandOne4, pbHandOne5 };
            return pictureBoxes;
        }

        private PictureBox[] ShowHandTwo()
        {
            var pictureBoxes = new PictureBox[5] { pbHandTwo1, pbHandTwo2, pbHandTwo3, pbHandTwo4, pbHandTwo5 };
            return pictureBoxes;
        }

        private Card[] GetHand(Card[] cards)
        {
            return cards;
        }

        private void SetImageClicks(PictureBox[] picBoxes)
        {
            foreach (PictureBox pb in picBoxes)
            {
                pb.Click += ClickOnImage;
            }
        }

        private void ClickOnImage(object sender, EventArgs eventArgs)
        {
            var picBox = (PictureBox)sender;
            // if (Array.Exists<PictureBox>(ShowHandOne(), pic => pic == picBox))
            //{

            //}
            if (clicks > 0)
                btnSwap.Enabled = true;
            else
            {
                btnSwap.Enabled = false;
                lblOutput2.Text = $"Press 'Swap' to switch up to {NUM_SWAPS} cards.";
            }

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

        private async void btnDeal_Click(object sender, EventArgs e)
        {
            ClearTable();
            pbDiscard.Image = null;
            for (var i = 0; i < CARDS_IN_HAND; i++)
            {
                await PutTaskDelay();
                players[0].AddCard(deck.DealCard());
                ShowHandOne()[i].SizeMode = PictureBoxSizeMode.StretchImage;
                ShowHandOne()[i].Image = players[0].GetHand()[i].Front;

                await PutTaskDelay();
                players[1].AddCard(deck.DealCard());
                ShowHandTwo()[i].SizeMode = PictureBoxSizeMode.StretchImage;
                ShowHandTwo()[i].Image = players[1].GetHand()[i].Front;
            }
            await PutTaskDelay();
            DisplayCardDetails();
            SetImageClicks(ShowHandOne());
            lblOutput1.Text = "Player One";
            lblOutput2.Text = "Please select cards to trade";

        }

        private void btnFlip_Click(object sender, EventArgs e)
        {
            pbDiscard.SizeMode = PictureBoxSizeMode.StretchImage;
            pbDiscard.Image = deck.getCurrentCard().Front;
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {

            var picBoxes = new ArrayList();
            picBoxes.AddRange(ShowHandOne());
            picBoxes.AddRange(ShowHandTwo());

            foreach (PictureBox pb in picBoxes)
            {
                if (pb.BackColor == Color.Red)
                {
                    foreach(Hand hand in players)
                    {
                        for(var i = 0; i < hand.GetHand().Length; i++)
                        {
                            if(hand.GetHand()[i].Front == pb.Image)
                            {
                                hand.GetHand()[i] = deck.DealCard();
                                pb.Image = hand.GetHand()[i].Front;
                                var index = Array.IndexOf(picBoxes.ToArray(), pb);
                                if(index > 4)
                                {
                                    index /= 2;
                                    GetHandTwoLabels()[index].Text = hand.GetHand()[i].ToString();
                                }
                                else
                                {
                                    GetHandOneLabels()[index].Text = hand.GetHand()[i].ToString();
                                } 
                            }
                        }
                    }
                }
            }

            
        }
    }
}

