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
    partial class StatList : Form
    {
        public StatList()
        {
            InitializeComponent();
        }

        //ListBox выводит с помощью foreach объекты из Statistics.MatchList.
        private void Statistics_Load(object sender, EventArgs e)
        {
            foreach (Statistics.Match stat in Statistics.MatchList)
            {
                string victoryName;
                if (stat.Victory) victoryName = "Победа"; else victoryName = "Поражение";
                listBox1.Items.Add(Statistics.MatchList.IndexOf(stat) + 1 + ": " + victoryName + ", Счёт: " + stat.Score);
            }
        }
    }
}