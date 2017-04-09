using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tst2
{
    public partial class Form1 : Form
    {
        //get the form drawing stuff 
        System.Drawing.Graphics g = null;
        Random random = new Random();
        int speed = 10;        
        int DrawDuration = 10000;
        int x = 120;
        int y = 120;
        int directionX = 1;
        int directionY = 1;
        int x1 = 20;
        int y1 = 620;
        int directionX1 = 1;
        int directionY1 = 1;
        int ColorR = 0;
        int ColorG = 0;
        int ColorB = 0;
        int directionColor = 1;
        bool ColorGradientToggle = false;

        public int GetLineSize()
        {            
            int linesize = ((x - x1) * (x - x1) + (y - y1) * (y - y1));
            return linesize;
        }
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //put the drawing stuff on your form
            g = this.CreateGraphics();
            numericUpDown5.Value = GetLineSize();
        }

        //click to draw for 10 seconds
        private void btnDrawLine_Click(object sender, EventArgs e)
        {
            MicroTimer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawDuration = 999999999;
            MicroTimer();
        }

        private void DrawLine(object sender,EventArgs e)
        {
            //donothing
            Pen colourPen = new Pen(Color.FromArgb(ColorR, ColorG, ColorB), 1);
            int randomColor = random.Next(0, 250);
            
            Pen colourPen1 = new Pen(Color.FromArgb(ColorR, randomColor, ColorB), 1);
            Point point1 = new Point(x, y);
            Point point2 = new Point(x1, y1);            
            //draw a line on the screen
            g.DrawLine(colourPen, point1, point2);
            g.DrawLine(colourPen1, new Point((point2.X + point1.X) , (point1.Y + point2.Y)), point1);
            EdgeAvoidance();
            ColorGradient();           
        }

        private void EdgeAvoidance()
        {
            x = x + directionX;
            y = y + directionY;

            x1 = x1 + directionX1;
            y1 = y1 + directionY1;
            
            if (x >= this.Width)    //  <---
            {                
                directionX = directionX * -1;
            }
            if (x <= 0)             // --->
            {                
                directionX = directionX * -1; 
            }
            if (y >= this.Height)   // ^
            {
                directionY = directionY * -1;
            }
            if (y <= 0)             // v
            {
                directionY = directionY * -1;
            }
            if (x1 >= this.Width)   //  <---
            {               
                directionX1 = directionX1 * -1;
            }            
            if (x1 <= 0)            // --->
            {             
                directionX1 = directionX1 * -1; 
            }
            if (y1 >= this.Height)
            {
                directionY1 = directionY1 * -1;
            }
            if (y1 <= 0)
            {             
                directionY1 = directionY1 * -1;
            }
        }
 
        private void ColorGradient()
        {
            if (ColorGradientToggle == true)
            {
                if (ColorR >= 250 || ColorG >= 250 || ColorB >= 250) { directionColor = -1; }
                if (ColorR == 0 || ColorG == 0 || ColorB == 0) { directionColor = 1; }

                ColorR += directionColor;
                ColorG += directionColor;
                ColorB += directionColor;
            }
        }

        private void MicroTimer()
        {
            // Instantiate new MicroTimer and add event handler
            MicroLibrary.MicroTimer microTimer = new MicroLibrary.MicroTimer();
            microTimer.MicroTimerElapsed +=
                new MicroLibrary.MicroTimer.MicroTimerElapsedEventHandler(DrawLine);

            microTimer.Interval = speed; // Call micro timer every 1000µs (1ms)    

            microTimer.Enabled = true; // Start timer

            // Do something whilst events happening, for demo sleep 2000ms (2sec)
            System.Threading.Thread.Sleep(DrawDuration);

            microTimer.Enabled = false; // Stop timer (executes asynchronously)

            // Alternatively can choose stop here until current timer event has finished
            // microTimer.StopAndWait(); // Stop timer (waits for timer thread to terminate)

        }

        private void SetText()
        {
            label1.Text = directionX.ToString();
            label2.Text = directionY.ToString();
            label3.Text = directionX1.ToString();
            label4.Text = directionY1.ToString();
            label8.Text = (speed * -1).ToString();
            label9.Text = ColorR.ToString();
            label10.Text = ColorG.ToString();
            label11.Text = ColorB.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SetText();
        }

        private void btnSpeedIncrease_Click(object sender, EventArgs e)
        {
            speed -= 1;
        }

        private void btnSpeedDecrease_Click(object sender, EventArgs e)
        {
            speed += 1;
        }

        //buttons
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ColorGradientToggle = !ColorGradientToggle;
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ColorR = Convert.ToInt32(numericUpDown1.Value);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            ColorG = Convert.ToInt32(numericUpDown2.Value);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            ColorB = Convert.ToInt32(numericUpDown3.Value);
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {


        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            speed = Convert.ToInt32(numericUpDown4.Value);
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

