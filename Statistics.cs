using System;
using System.Collections.Generic;
using System.Text;

namespace SteadyHandVersion5
{
    //Этот класс хранит итоги всех игр. Отсюда берёт информацию форма StatList.
    public static class Statistics
    {
        //Statistics хранит описание класса Match, в который вносится информация о том, какой в игре был счёт и была ли она выграна.
        public class Match
        {
            public bool Victory;
            public int Score;
            public Match(bool victory, int score)
            {
                Victory = victory;
                Score = score;
            }
        }
        public delegate void MatchInput(Match match);
        //В список в Statistics вносятся объекты класса Match, который заимствует StatList.
        public static List<Match> MatchList = new List<Match>();
    }
}