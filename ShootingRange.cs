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
    public partial class ShootingRange : Form
    {
        //Переменная _index показывает, в какую из целей нужно попасть на момент игры.
        int _index;
        int Index { get { return _index; } set { ChallengeEnd(ref value); _index = value; } }
        //Статистика игры.
        int _score;
        bool _victory = false;
        List<Target> _targets;
        //Объект класса Timer. По его истечению запустится событие, в результате которого игра будет проиграна.
        System.Timers.Timer _timerTimer;
        //Поток, в котором будет истекать время _timerTimer.
        Task _timerTask;
        //Форма, из которой был создан объект ShootingRange.
        Form1 _form1;

        public ShootingRange(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
            //Список целей, нужный для определения, в какую цель нужно попасть, и их количества.
            _targets = new List<Target> { vystrel1, vystrel2, vystrel3, vystrel4, vystrel5, vystrel6, vystrel7, vystrel8 };
            //Определение потока. Он определяет _timerTimer, добавляет к _timerTimer.Elapsed функцию gameOver и запускает его.
            _timerTask = new Task(() => 
            {
                _timerTimer = new System.Timers.Timer(1000);
                _timerTimer.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) => gameOver();
                _timerTimer.Start();
            });
            _timerTask.Start();
            label1.Text = "Цель: 1 счёт: 0";
        }

        //Функция, которая вызывается при изменении поля Index.
        void ChallengeEnd(ref int index)
        {
            if (index == _targets.Count)
            {
                _victory = true;
                gameOver();
            }
            if (index < _targets.Count)
                if (index < _targets.Count && index > 0)
                {
                    _timerTimer.Interval = 1000;
                    label1.Text = "Цель: " + (index + 1) + " счёт: " + (_score);
                }
        }

        private void Targets_Load(object sender, EventArgs e)
        {
            
        }

        //Функция, которую вызывает нажатие на цель. При попадании в один из сегментов круга цели, начисляется соответствующее количество очки.
        private void TargetHit(object sender, MouseEventArgs e)
        {
            foreach (Target target in new List<object> { sender }) if (target.CircleHit(e) && Index == _targets.IndexOf(target))
                {
                    _score += 1;
                    if (target.CircleHit(e, 0.875)) _score++;
                    if (target.CircleHit(e, 0.75)) _score++;
                    if (target.CircleHit(e, 0.625)) _score++;
                    if (target.CircleHit(e, 0.5)) _score++;
                    if (target.CircleHit(e, 0.375)) _score++;
                    if (target.CircleHit(e, 0.25)) _score++;
                    if (target.CircleHit(e, 0.125)) _score++;
                    Index++;
                }
        }

        //Функция, которая запускается по истечении _timerTimer. В ней результат игры записывается в Statistics.MatchList и выводится на главное меню.
        private void gameOver()
        {
            _timerTimer.Stop();
            Statistics.Match match = new Statistics.Match(_victory, _score);
            //Так как _form1 изменяется в другом потоке, используется функция Invoke для записи результатов матча туда. Invoke требует делегат для передачи его в другой поток, так что используется делегат, определённый в _form1.
            Invoke(_form1.ScoreOutputDelegate, new object[1] { match });
            Statistics.MatchList.Add(match);
            Action close = Close;
            Invoke(close);
        }
    }
}
