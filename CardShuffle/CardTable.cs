using CardShuffle.Public;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using CardShuffle.Properties;
using System.Globalization;
using System.Threading.Tasks;

namespace CardShuffle
{
    public partial class CardTable : Form
    {

        private Deck deck;
       // ArrayList images = new ArrayList();
        Random rand = new Random();
        Card[] hand1 = new Card[5];
        Card[] hand2 = new Card[5];
        int selectedCards = 0;
        

        public CardTable()
        {
            InitializeComponent();
            deck = new Deck();
            deck.shuffleCards();

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
            for(var i = 0; i < hand1.Length; i++)
            {
                GetHandOneLabels()[i].Text = hand1[i].ToString();
                GetHandOneLabels()[i].ForeColor = Color.White;
                GetHandTwoLabels()[i].Text = hand2[i].ToString();
                GetHandTwoLabels()[i].ForeColor = Color.White;
            }
        }

        private void ClearTable()
        {
            for(var i = 0; i < hand1.Length; i++)
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

        private void SetCardClicks()
        {
            ArrayList tempCards = new ArrayList();
            tempCards.AddRange(ShowHandOne());
            tempCards.AddRange(ShowHandTwo());
            foreach(PictureBox pb in tempCards)
            {
                pb.Click += ClickOnImage;
            }
            
        }

        private void ClickOnImage(object sender, EventArgs eventArgs)
        {

            var picBox = (PictureBox)sender;
            if (selectedCards <= 2)
            {
                picBox.BorderStyle = BorderStyle.Fixed3D;
            }
            selectedCards++;
            
        }

        private void CardTable_Load(object sender, EventArgs e)
        {
            pbDrawDeck.SizeMode = PictureBoxSizeMode.StretchImage;
            pbDrawDeck.Image = Resources.blue_back;
        }

        private async void btnDeal_Click(object sender, EventArgs e)
        {
            ClearTable();
            pbDiscard.Image = null;
            for (var i = 0; i < (hand1.Length); i++)
            {
                await PutTaskDelay();
                hand1[i] = deck.DealCard();
                ShowHandOne()[i].SizeMode = PictureBoxSizeMode.StretchImage;
                ShowHandOne()[i].Image = hand1[i].Front;

                await PutTaskDelay();
                hand2[i] = deck.DealCard();
                ShowHandTwo()[i].SizeMode = PictureBoxSizeMode.StretchImage;
                ShowHandTwo()[i].Image = hand2[i].Front;
            }
            await PutTaskDelay();
            pbDiscard.SizeMode = PictureBoxSizeMode.StretchImage;
            pbDiscard.Image = deck.getCurrentCard().Front;
            DisplayCardDetails();
            SetCardClicks();
        }
    }
}
