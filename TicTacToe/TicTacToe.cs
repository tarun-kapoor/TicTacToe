using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class FrmMain : Form
    {
        bool bCross = true;
        string _MainText = "TicTacToe";
        
        public FrmMain()
        {
            InitializeComponent();
            fnClear();            
        }

        private void fnClear()
        {
            //Next turn
            Text = _MainText + " -Cross";

            //Clear picture box
            pictureBoxLow1.InitialImage = null;
            pictureBoxLow2.InitialImage = null;
            pictureBoxLow3.InitialImage = null;
            pictureBoxMid1.InitialImage = null;
            pictureBoxMid2.InitialImage = null;
            pictureBoxMid3.InitialImage = null;
            pictureBoxTop1.InitialImage = null;
            pictureBoxTop2.InitialImage = null;
            pictureBoxTop3.InitialImage = null;

            pictureBoxLow1.Image = null;
            pictureBoxLow2.Image = null;
            pictureBoxLow3.Image = null;
            pictureBoxMid1.Image = null;
            pictureBoxMid2.Image = null;
            pictureBoxMid3.Image = null;
            pictureBoxTop1.Image = null;
            pictureBoxTop2.Image = null;
            pictureBoxTop3.Image = null;

            pictureBoxLow1.ImageLocation = null;
            pictureBoxLow2.ImageLocation = null;
            pictureBoxLow3.ImageLocation = null;
            pictureBoxMid1.ImageLocation = null;
            pictureBoxMid2.ImageLocation = null;
            pictureBoxMid3.ImageLocation = null;
            pictureBoxTop1.ImageLocation = null;
            pictureBoxTop2.ImageLocation = null;
            pictureBoxTop3.ImageLocation = null;

            pictureBoxLow1.Enabled = true;
            pictureBoxLow2.Enabled = true;
            pictureBoxLow3.Enabled = true;
            pictureBoxMid1.Enabled = true;
            pictureBoxMid2.Enabled = true;
            pictureBoxMid3.Enabled = true;
            pictureBoxTop1.Enabled = true;
            pictureBoxTop2.Enabled = true;
            pictureBoxTop3.Enabled = true;
        }

        private void fnDisableGame()
        {
            pictureBoxLow1.Enabled = false;
            pictureBoxLow2.Enabled = false;
            pictureBoxLow3.Enabled = false;
            pictureBoxMid1.Enabled = false;
            pictureBoxMid2.Enabled = false;
            pictureBoxMid3.Enabled = false;
            pictureBoxTop1.Enabled = false;
            pictureBoxTop2.Enabled = false;
            pictureBoxTop3.Enabled = false;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            //Get control - Apply picture based on next in list
            fnUpdateImage(sender as PictureBox);
        }

        private void fnUpdateImage(PictureBox pictureBox)
        {
            if (bCross)
            {
                pictureBox.ImageLocation = (pictureBox.ImageLocation == null) ? @"Images\Cross.png" : pictureBox.ImageLocation;
                bCross = false;
                Text = _MainText + " -Zero";
            }
            else
            {
                pictureBox.ImageLocation = (pictureBox.ImageLocation == null) ? @"Images\Zero.png" : pictureBox.ImageLocation;
                bCross = true;
                Text = _MainText + " -Cross";
            }

            pictureBox.Update();

            Task.Run(() => CheckWinner());
        }

        private void CheckWinner()
        {
            bool bNoWinner = false;
            //Check win
            //Horizontals
            if (pictureBoxTop1.ImageLocation != null && (pictureBoxTop1.ImageLocation == pictureBoxTop2.ImageLocation) && (pictureBoxTop2.ImageLocation == pictureBoxTop3.ImageLocation))
            {
                Invoke(new Action(() => fnDisableGame()));
                MessageBox.Show($"{(bCross ? "Zero" : "Cross")} won", "Top");
            }

            else if (pictureBoxLow1.ImageLocation != null && (pictureBoxLow1.ImageLocation == pictureBoxLow2.ImageLocation) && (pictureBoxLow2.ImageLocation == pictureBoxLow3.ImageLocation))
            {
                Invoke(new Action(() => fnDisableGame()));
                MessageBox.Show($"{(bCross ? "Zero" : "Cross")} won", "Bottom");
            }
            else if (pictureBoxMid1.ImageLocation != null && (pictureBoxMid1.ImageLocation == pictureBoxMid2.ImageLocation) && (pictureBoxMid2.ImageLocation == pictureBoxMid3.ImageLocation))
            {
                Invoke(new Action(() => fnDisableGame()));
                MessageBox.Show($"{(bCross ? "Zero" : "Cross")} won", "H-Center");
            }
            //Verticals
            else if (pictureBoxTop1.ImageLocation != null && (pictureBoxTop1.ImageLocation == pictureBoxMid1.ImageLocation) && (pictureBoxMid1.ImageLocation == pictureBoxLow1.ImageLocation))
            {
                Invoke(new Action(() => fnDisableGame()));
                MessageBox.Show($"{(bCross ? "Zero" : "Cross")} won", "Left");
            }
            else if (pictureBoxTop2.ImageLocation != null && (pictureBoxTop2.ImageLocation == pictureBoxMid2.ImageLocation) && (pictureBoxMid2.ImageLocation == pictureBoxLow2.ImageLocation))
            {
                Invoke(new Action(() => fnDisableGame()));
                MessageBox.Show($"{(bCross ? "Zero" : "Cross")} won", "V-Center");
            }
            else if (pictureBoxTop3.ImageLocation != null && (pictureBoxTop3.ImageLocation == pictureBoxMid3.ImageLocation) && (pictureBoxMid3.ImageLocation == pictureBoxLow3.ImageLocation))
            {
                Invoke(new Action(() => fnDisableGame()));
                MessageBox.Show($"{(bCross ? "Zero" : "Cross")} won", "Right");
            }
            //Diagonals
            else if (pictureBoxTop1.ImageLocation != null && (pictureBoxTop1.ImageLocation == pictureBoxMid2.ImageLocation) && (pictureBoxMid2.ImageLocation == pictureBoxLow3.ImageLocation))
            {
                Invoke(new Action(() => fnDisableGame()));
                MessageBox.Show($"{(bCross ? "Zero" : "Cross")} won", "D1");
            }
            else if (pictureBoxTop3.ImageLocation != null && (pictureBoxTop3.ImageLocation == pictureBoxMid2.ImageLocation) && (pictureBoxMid2.ImageLocation == pictureBoxLow1.ImageLocation))
            {
                Invoke(new Action(() => fnDisableGame()));
                MessageBox.Show($"{(bCross ? "Zero" : "Cross")} won", "D2");
            }
            else
            {
                //Check completion
                foreach (Control c in this.Controls)
                {
                    if (c is PictureBox && (c as PictureBox).ImageLocation == null)
                    {
                        bNoWinner = true;
                        break;
                    }
                }
            }

            //Someone won or moves exhausted
            if (!bNoWinner)
            {
                if (DialogResult.Yes == MessageBox.Show("Play again?", "TicTacToe", MessageBoxButtons.YesNo))
                {
                    Invoke(new Action(() => fnClear()));
                }
                else
                {
                    Invoke(new Action(() => fnDisableGame()));
                }
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fnClear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tarun Kapoor - cols_mark@msn.com # fb.me/godofcode", "Created by");
        }
    }
}
