using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MegaChallengeCasino
{
    public partial class Default : System.Web.UI.Page
    {
        Random random = new Random();
        string[] images = new string[12] { "Bar.png", "Bell.png", "Cherry.png", "Clover.png", "Diamond.png", "HorseShoe.png", "Lemon.png", "Orange.png", "Plum.png", "Seven.png", "Strawberry.png", "Watermellon.png" };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Display starting images
                displayAllImages();
                int currentBalence = 100;
                ViewState.Add("CurrentBalence", currentBalence);
                moneyLabel.Text = string.Format("Players Money: {0:C}", currentBalence);
            }

        }

        protected void leverButton_Click(object sender, EventArgs e)
        {
            // Generate 3 images           
            displayAllImages();
            // Determine if Winner or Loser
            // Calculate winnings or loss
            // Display results
            calculateMoneyWonOrLost();
        }

        // Helper Methods below

        private string randomImage()
        {       
            string displayImage = images [random.Next(11)];
            return displayImage;
        }

        private void displayAllImages()
        {
            Image1.ImageUrl = randomImage();
            Image2.ImageUrl = randomImage();
            Image3.ImageUrl = randomImage();
        }

        private bool winOrLose()
        {
            // if true run winner(), false run loser()
            if (Image1.ImageUrl == images[0] || Image2.ImageUrl == images[0] || Image3.ImageUrl == images[0]) return false;
            else if (Image1.ImageUrl == images[2] || Image2.ImageUrl == images[2] || Image3.ImageUrl == images[2]) return true;
            else if (Image1.ImageUrl == images[9] && Image2.ImageUrl == images[9] && Image3.ImageUrl == images[9]) return true;
            else return false;
        }

        private void winner()
        {
            int currentBalence = int.Parse(ViewState["CurrentBalence"].ToString());
            int bet = int.Parse(betTextBox.Text.Trim());
            int winnings = 0;

            if (Image1.ImageUrl == images[9] && Image2.ImageUrl == images[9] && Image3.ImageUrl == images[9])
            {
                winnings = bet * 100;
            }
            else if (Image1.ImageUrl == images[2] && Image2.ImageUrl == images[2] && Image3.ImageUrl == images[2])
            {
                winnings = bet * 4;
            }
            else if (Image1.ImageUrl == images[2] && Image2.ImageUrl == images[2])
            {
                winnings = bet * 3;
            }
            else if (Image2.ImageUrl == images[2] && Image3.ImageUrl == images[2])
            {
                winnings = bet * 3;
            }
            else if (Image1.ImageUrl == images[2] && Image3.ImageUrl == images[2])
            {
                winnings = bet * 3;
            }
            else if (Image1.ImageUrl == images[2] || Image2.ImageUrl == images[2] || Image3.ImageUrl == images[2])
            {
                winnings = bet * 2;
            }

            currentBalence += winnings;
            ViewState["CurrentBalence"] = currentBalence;
            resultLabel.Text = string.Format("You bet {0:C}, and won {1:C}", int.Parse(betTextBox.Text.Trim()), winnings);
            moneyLabel.Text = string.Format("Players Money: {0:C}", currentBalence);
        }

        private void loser()
        {
            int currentBalence = int.Parse(ViewState["CurrentBalence"].ToString());
            currentBalence -= int.Parse(betTextBox.Text.Trim());
            ViewState["CurrentBalence"] = currentBalence;
            resultLabel.Text = string.Format("Sorry, you lost {0:C}, Better luck next time", int.Parse(betTextBox.Text.Trim()));
            moneyLabel.Text = string.Format("Players Money: {0:C}", currentBalence);
        }

        private void calculateMoneyWonOrLost()
        {           
            if (!winOrLose()) loser();
            else winner();
        }
    }
}