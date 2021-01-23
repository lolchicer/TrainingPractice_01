using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteadyHandVersion5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Определение делегата, который будет вызван при конце игры.
            ScoreOutputDelegate = ScoreOutput;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
        }

        //При нажатии этой кнопки вызывается игра.
        private void button1_Click(object sender, EventArgs e)
        {
            new ShootingRange(this).Show();
        }

        //При нажатии этой кнопки вызывается список результатов предыдущих игр.
        private void button2_Click(object sender, EventArgs e)
        {
            new StatList().Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //Этот делегат вызывается в конце работы формы ShootingRange.
        public Statistics.MatchInput ScoreOutputDelegate;
        //В конструкторе Form1 эта функция является значением ScoreOutputDelegate. Она изменяет текст label1 на итоги ShootingRange.
        void ScoreOutput(Statistics.Match match)
        {
            if (match.Victory) label1.Text = "Победа! ";
            else label1.Text = "Поражение. ";
            label1.Text += "Счёт: " + match.Score;
        }
    }
}