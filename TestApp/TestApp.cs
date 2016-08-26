using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MikeMath;

namespace TestApp
{
    public partial class TestApp : Form
    {
        private new BigNumber Left { get; set; }
        private new BigNumber Right { get; set; }

        public TestApp()
        {
            InitializeComponent();
            labelErrorA.Text = "";
            labelErrorB.Text = "";
        }

        private bool ParseOperands()
        {
            bool valid = true;
            try
            {
                Left = BigNumber.Parse(textBoxA.Text);
                textBoxA.Text = Left;
                labelErrorA.Text = "";
            }
            catch (Exception e)
            {
                labelErrorA.Text = e.Message;
                valid = false;
            }
            try
            {
                Right = BigNumber.Parse(textBoxB.Text);
                textBoxB.Text = Right;
                labelErrorB.Text = "";
            }
            catch (Exception e)
            {
                labelErrorB.Text = e.Message;
                valid = false;
            }
            return valid;
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            if (!ParseOperands()) return;
            var result = Left + Right;
            textBoxResult.Text = result;
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            if (!ParseOperands()) return;
            var result = Left - Right;
            textBoxResult.Text = result;
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            if (!ParseOperands()) return;
            var result = Left * Right;
            textBoxResult.Text = result;
        }

        private void buttonDivide_Click(object sender, EventArgs e)
        {
            if (!ParseOperands()) return;
            if (Right == BigNumber.Zero)
            {
                textBoxResult.Text = "Division By Zero Error";
                return;
            }
            var result = Left / Right;
            textBoxResult.Text = result;
        }

        private void buttonGreater_Click(object sender, EventArgs e)
        {
            if (!ParseOperands()) return;
            textBoxResult.Text = (Left > Right).ToString();
        }

        private void buttonLess_Click(object sender, EventArgs e)
        {
            if (!ParseOperands()) return;
            textBoxResult.Text = (Left < Right).ToString();
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            if (!ParseOperands()) return;
            textBoxResult.Text = (Left == Right).ToString();
        }

    }
}
