using System;
using System.IO;
using System.Collections.Generic;

namespace Barulin_Alexey_Romanovich_Task_05
{
    //Класс Symbol является шаблоном для описания действий символа в массиве Program._array.
    class Symbol
    {
        //Символ, который представляет объект.
        internal char SymbolChar;
        //Делегат, описывающий особое передвижение символа своим объектом. Он вызывается в Move.
        internal Program.IntCoordinatesInput Action;
        //Список объектов класса Relation, описывающего действия, происходящие при перемещении на какой-либо из символов в массиве.
        internal List<Relation> _relations = new List<Relation>();

        internal Symbol()
        {
            SymbolChar = '&';
            Action = BlankAction;
        }

        //Функция, описывающая действия символа, одинакового с SymbolChar, когда в Program.Game() приходит его очередь.
        internal void Move(char[,] _array, int x, int y)
        {
            //Новое местоположение символа. За символом всегда остаётся пробел, Program.Symbols[0].
            int[] coordinates = Action(_array, x, y);
            //Если новое местоположение находится вне границ массива, то смены координат не происходит.
            if (coordinates[0] >= 0 && coordinates[0] < _array.GetLength(0) && coordinates[1] >= 0 && coordinates[1] < _array.GetLength(1))
            {
                //Перебираются все объекты в списке _relations.
                //Если их _relator.SymbolChar равен символу на координатах coordinates, проверяется поле Relation Collision и выполняется делегат Relation Action.
                //Если Relation.Collision равно false, текущие координаты можно заменить на новые.
                foreach (Relation _relation in _relations)
                {
                    if (_relation.Valid(_array[coordinates[0], coordinates[1]]))
                    {
                        if (!_relation.Collision)
                        {
                            _array[x, y] = ' ';
                            _array[coordinates[0], coordinates[1]] = SymbolChar;
                        }
                        _relation.Action();
                    }
                }
            }
            else Program.Output = "Стена.";
        }

        //Функция передвижения для символа игрока.
        static internal int[] PlayerAction(char[,] _array, int x, int y)
        {
            int[] coordinates = new int[2];
            coordinates[0] = x;
            coordinates[1] = y;
            switch (Program.Input)
            {
                case "w":
                    coordinates[1] = y - 1;
                    break;
                case "a":
                    coordinates[0] = x - 1;
                    break;
                case "s":
                    coordinates[1] = y + 1;
                    break;
                case "d":
                    coordinates[0] = x + 1;
                    break;
            }
            return coordinates;
        }

        //Функция передвижения для символов противников.
        static internal int[] EnemyAction(char[,] array, int x, int y)
        {
            int[] coordinates = new int[2];
            coordinates[0] = x;
            coordinates[1] = y;
            //Чтобы противники не двигались в те стороны, в которые не могут, перед случайным выбором стороны, создаётся список возможных направлений.
            List<char> directions = new List<char>();
            if (y - 1 >= 0)
            {
                if (array[x, y - 1] == ' ') directions.Add('w');
                if (array[x, y - 1] == '@') directions.Add('w');
            }
            if (x - 1 >= 0)
            {
                if (array[x - 1, y] == ' ') directions.Add('a');
                if (array[x - 1, y] == '@') directions.Add('a');
            }
            if (y + 1 < array.GetLength(1))
            {
                if (array[x, y + 1] == ' ') directions.Add('s');
                if (array[x, y + 1] == '@') directions.Add('s');
            }
            if (x - 1 < array.GetLength(0))
            {
                if (array[x + 1, y] == ' ') directions.Add('d');
                if (array[x + 1, y] == '@') directions.Add('d');
            }
            Random _random = new Random();
            switch (directions[_random.Next(directions.Count)])
            {
                case 'w':
                    coordinates[1] = y - 1;
                    break;
                case 'a':
                    coordinates[0] = x - 1;
                    break;
                case 's':
                    coordinates[1] = y + 1;
                    break;
                case 'd':
                    coordinates[0] = x + 1;
                    break;
            }
            return coordinates;
        }

        //Незаполненнная функция для символов без действий.
        internal int[] BlankAction(char[,] array, int x, int y)
        {
            int[] coordinates = new int[2];
            coordinates[0] = x;
            coordinates[1] = y;
            return coordinates;
        }
    }

    //Объекты этого класса определяют, что происходит с имеющим их символом при перемещении на _relator.
    class Relation
    {
        //Символ, при перемещении на который происходит действие.
        Symbol _relator;
        internal Program.NoInput Action;
        internal bool Collision;

        internal Relation()
        {
            Action = Default;
            Collision = false;
        }

        internal bool Valid(char _symbol)
        {
            return (_symbol == _relator.SymbolChar);
        }

        static internal void Default() { }

        static internal void PlayerWall()
        {
            Program.Output = "Стена.";
        }

        static internal void PlayerEnemy()
        {
            Program.HealthPoints--;
            Program.Output = "Вы наступили на ёжика.";
        }

        static internal void PlayerExit()
        {
            Program.Output = "Выход из лабиринта.";
            Program.Victory = true;
        }

        static internal void EnemyPlayer()
        {
            Program.HealthPoints--;
            Program.Output = "Ёжик вас уколол.";
        }
    }

    static class Program
    {
        //Делегаты.
        internal delegate void NoInput();
        internal delegate int[] IntCoordinatesInput(char[,] _array, int x, int y);
        internal delegate bool BoolNoInput();

        //Массив с лабиринтом.
        static char[,] _array = new char[9, 9];
        //Название массива на диске.
        static string arrayName;

        //Список объектов класса Symbol. Один объект класса представляет собой тип символов, имеющий свои свойства.
        internal static List<Symbol> Symbols = new List<Symbol>();

        //Поля, отвечающие за конец игры.
        internal static bool Defeat = false;
        internal static bool Victory = false;

        //Поле, отвечающее за здоровье игрока.
        internal static int HealthPoints;

        //Поля, отвечающие за ввод и вывод в консоль.
        internal static string Input;
        internal static string Output;

        //Функция, перезаписывающая string в двумерный массив символов.
        static void ArraySet(string arrayString)
        {
            int indexString = 0;
            for (int indexY = 0; indexY < 9; indexY++)
            {
                for (int _indexX = 0; _indexX < 9; _indexX++)
                {
                    if (indexString < arrayString.Length)
                    {
                        if (LanguageCheck(arrayString[indexString])) _array[_indexX, indexY] = arrayString[indexString];
                    }
                    indexString++;
                }
            }
        }
        //Функция, построчно считывающая текст из файла.
        static string ArrayRead(string path)
        {
            string arrayString = "";
            try
            {
                string line;
                StreamReader sr = new StreamReader(path);
                while ((line = sr.ReadLine()) != null)
                {
                    arrayString += line;
                }
                Output = "Карта загружена.";
            }
            catch
            {
                Output = "Файл " + path + " не найден.";
            }
            arrayName = path;
            return arrayString;
        }
        //Функция, копирующая двумерный массив.
        static char[,] ArrayCopy(char[,] arrayInput)
        {
            char[,] arrayOutput = new char[arrayInput.GetLength(0), arrayInput.GetLength(1)];
            for (int indexY = 0; indexY < 9; indexY++)
            {
                for (int indexX = 0; indexX < 9; indexX++)
                {
                    arrayOutput[indexX, indexY] = arrayInput[indexX, indexY];
                }
            }
            return arrayOutput;
        }
        //Функция, записывающая двумерный массив символов в string.
        static string MapString(char[,] array)
        {
            string arrayString = "";
            for (int indexY = 0; indexY < array.GetLength(1); indexY++)
            {
                for (int indexX = 0; indexX < array.GetLength(0); indexX++)
                {
                    arrayString += (array[indexX, indexY]);
                }
                if (indexY != array.GetLength(1) - 1) arrayString += '\n';
            }
            return arrayString;
        }
        //Функция, заполняющая _array пробелами.
        static void MapClear()
        {
            for (int _indexY = 0; _indexY < _array.GetLength(1); _indexY++)
            {
                for (int _indexX = 0; _indexX < _array.GetLength(0); _indexX++)
                {
                    _array[_indexX, _indexY] = Symbols[0].SymbolChar;
                }
            }
        }
        //Функция, определяющая объекты класса Symbol.
        static void SymbolsInitialization()
        {
            Symbols.Add(new Symbol() { SymbolChar = ' ' });
            Symbols.Add(new Symbol() { SymbolChar = '█' });
            Symbols.Add(new Symbol() { SymbolChar = '@', Action = Symbol.PlayerAction });
            Symbols.Add(new Symbol() { SymbolChar = 'e' });
            Symbols.Add(new Symbol() { SymbolChar = '*', Action = Symbol.EnemyAction });

            foreach (Symbol _symbol in Symbols)
            {
                _symbol._relations.Add(new Relation() { Relator = Symbols[0] });
            }

            Symbols[2]._relations.Add(new Relation() { Relator = Symbols[1], Collision = true, Action = Relation.PlayerWall });
            Symbols[2]._relations.Add(new Relation() { Relator = Symbols[2], Collision = true, Action = Relation.Default });
            Symbols[2]._relations.Add(new Relation() { Relator = Symbols[3], Collision = true, Action = Relation.PlayerExit });
            Symbols[2]._relations.Add(new Relation() { Relator = Symbols[4], Collision = true, Action = Relation.PlayerEnemy });

            Symbols[4]._relations.Add(new Relation() { Relator = Symbols[1], Collision = true });
            Symbols[4]._relations.Add(new Relation() { Relator = Symbols[2], Collision = true, Action = Relation.EnemyPlayer });
            Symbols[4]._relations.Add(new Relation() { Relator = Symbols[3], Collision = true });
            Symbols[4]._relations.Add(new Relation() { Relator = Symbols[4], Collision = true });
        }
        //Функция, определяющая соответствие входного символа полю SymbolChar какого-либо объекта Symbol.
        static bool LanguageCheck(char checkingSymbol)
        {
            bool check = false;
            foreach (Symbol symbol in Symbols) if (checkingSymbol == symbol.SymbolChar) check = true;
            return check;
        }

        //Большая часть работы программы происходит в функции Main и некоторых других функциях.
        //Эти функции имеют цикл while, в котором пользователь выбирает действия через командную строку, и в котором выводится информация об этих действиях.
        //Информация выводится только через функцию SceneWrite, которая перед каждым её выводом очищает консоль. На протяжении действия цикла заполняется Output, после вывода очищающийся.
        //Также в этой функции через консоль вводится значение Input, с которым цикл работает.
        static void SceneWrite()
        {
            Console.Clear();
            Console.WriteLine(Output);
            Output = "";
            Input = Console.ReadLine();
        }
        //Функция, выводящая прохождение уровня, если таковое есть.
        static string Walkthrough()
        {
            string _walkthrough = "";
            try
            {
                string _line;
                StreamReader sr = new StreamReader(@"walkthroughs\" + arrayName);
                _walkthrough += '\n';
                while ((_line = sr.ReadLine()) != null)
                {
                    _walkthrough += _line + '\n';
                }
                Output = "Карта загружена.";
            }
            catch
            {
                Output = "Прохождение " + arrayName + " не найдено.";
            }
            return _walkthrough;
        }

        //Главное меню программы.
        static void Main(string[] args)
        {
            SymbolsInitialization();
            ArraySet(ArrayRead("e1m1.txt"));
            bool sceneEnd = false;

            Output = MainText();
            while (!sceneEnd)
            {
                SceneWrite();
                switch (Input)
                {
                    case "1":
                        Loader();
                        break;
                    case "2":
                        Game();
                        break;
                    case "3":
                        Editor();
                        break;
                    case "4":
                        sceneEnd = true;
                        break;
                }
                Output += MainText();
            }
        }
        static string MainText()
        {
            return "\n---------\n1: загрузить карту\n2: сыграть\n3: редактор\n4: выйти";
        }

        //Меню загрузки массива.
        static void Loader()
        {
            bool sceneEnd = false;
            Output += LoaderText();
            while (!sceneEnd)
            {
                SceneWrite();
                if (Input == "esc") sceneEnd = true;
                if (!sceneEnd)
                {
                    //Если пользователь не выходит из меню, по введённому значению ищется файл в папке с проектом.
                    MapClear();
                    ArraySet(ArrayRead(Input));
                    Output += LoaderText();
                }
            }
            Output = "";
        }
        static string LoaderText()
        {
            return "\n---------\nВведите имя файла.\nesc – выход.";
        }

        //Функция, в которой проходит игра.
        static void Game()
        {
            Defeat = false;
            Victory = false;
            //В цикле значение playerExisting меняется на true, если найден символ игрока. В противном случае, игра закончится.
            bool playerExisting = false;
            //Если ввести команду walkthrough, то ниже карты выведется ещё одна карта, показывающая путь к концу лабиринта.
            bool showWalkthrough = false;
            HealthPoints = 4;

            //_arrayWork присваивает копию _array. Массив _arrayMoves копирует _arrayWork в процессе цикла.
            //Игра просчитывает действия элементов массива _arrayMoves, и изменяет в результате этих действий _arrayWork. Это нужно для того, чтобы нельзя было повлиять на действия символов во время хода.
            char[,] _arrayWork = ArrayCopy(_array);
            char[,] _arrayMoves;

            Output += GameText(_arrayWork, showWalkthrough);
            while (!Defeat && !Victory)
            {
                playerExisting = false;

                SceneWrite();

                if (Input == "esc") Defeat = true;
                if (Input == "walkthrough") showWalkthrough = !showWalkthrough;

                _arrayMoves = ArrayCopy(_arrayWork);
                for (int _indexY = 0; _indexY < 9; _indexY++)
                {
                    for (int _indexX = 0; _indexX < 9; _indexX++)
                    {
                        //Символом игрока считается объект, имеющий функцию PlayerAction в качестве значения делегата Action.
                        if (_arrayMoves[_indexX, _indexY] == Symbols.Find(x => x.Action == Symbol.PlayerAction).SymbolChar) playerExisting = true;
                        //Выбор соответствующего символа в объектах Symbol и последующее действие соответствующего объекта Symbol.
                        foreach (Symbol _symbol in Symbols)
                        {
                            if (_arrayMoves[_indexX, _indexY] == _symbol.SymbolChar) _symbol.Move(_arrayWork, _indexX, _indexY);
                        }
                    }
                }
                //Если отсутствует символ игрока или единицы здоровья равны 0, засчитывается поражение.
                if (HealthPoints <= 0)
                {
                    Defeat = true;
                }
                if (!playerExisting)
                {
                    Defeat = true;
                }
                Output += GameText(_arrayWork, showWalkthrough);
            }
            Output = "";
            if (!playerExisting) Output += "Персонаж потерян.";
            if (Defeat) Output += "Вы проиграли.";
            if (Victory) Output += "Победа.";
        }
        static string GameText(char[,] _array, bool _showWalkthrough)
        {
            string _output;
            _output = "\n---------\n" + HealthPoints + "HP ";
            for (int i = 0; i < HealthPoints; i++) _output += '█';
            for (int i = 0; i < 4 - HealthPoints; i++) _output += ' ';
            _output += "\n---------\n" + MapString(_array);
            if (_showWalkthrough) _output += "\n---------\n" + Walkthrough();
            return _output;
        }

        //Функция, в которой редактируется _array.
        static void Editor()
        {
            //Символ, который вносится в массив. По умолчанию это пустой символ.
            char selectedSymbol = Symbols[0].SymbolChar;
            bool sceneEnd = false;
            Output = EditorText();

            while (!sceneEnd)
            {
                SceneWrite();

                switch (Input)
                {
                    case "esc":
                        sceneEnd = true;
                        break;
                    case "clear":
                        MapClear();
                        Output = "Карта очищена.";
                        break;
                    case "save":
                        Save();
                        break;
                    case "walkthrough":
                        AddWalkThrough();
                        break;
                    //В случае default выполняется проверка введённой строки на соответствие каким-либо символом из объектов Symbol.
                    //При соответствии, пользователю предлагается выбрать место, на которое будет поставлен символ.
                    default:
                        if (Input != "") selectedSymbol = Input[0];
                        if (LanguageCheck(selectedSymbol)) SymbolPut(selectedSymbol);
                        else Output = "Такой команды или такого символа нет.";
                        break;
                }
                Output += EditorText();
            }
            Output = "";
        }
        static string EditorText()
        {
            string output;
            output = "\n---------\n" + MapString(_array) + "\n---------\n" + "Выберите команду или символ:  ";
            foreach (Symbol _symbol in Symbols)
            {
                output += _symbol.SymbolChar;
                if (Symbols.IndexOf(_symbol) != Symbols.Count - 1) output += ", ";
                else output += ".\n";
            }
            output += "clear: очистить карту\nsave: сохранить карту\nwalkthrough: редактор прохождений\nesc: выйти";
            return output;
        }

        //Меню сохранения карты.
        static void Save()
        {
            bool sceneEnd = false;
            Output = SaveText();

            while (!sceneEnd)
            {
                SceneWrite();

                //Для записи в файл используется объект класса StreamWriter. Построчно с помощью циклов for в него вносятся элементы _array, которые выводятся в файл функциями Write и WriteLine.
                if (Input == "esc") sceneEnd = true;
                if (!sceneEnd)
                {
                    try
                    {
                        StreamWriter streamWriter = new StreamWriter(Input);
                        for (int indexY = 0; indexY < _array.GetLength(1); indexY++)
                        {
                            string line = "";
                            for (int indexX = 0; indexX < _array.GetLength(0); indexX++)
                            {
                                line += _array[indexX, indexY];
                            }
                            if (indexY == _array.GetLength(1) - 1) streamWriter.Write(line);
                            else streamWriter.WriteLine(line);
                        }
                        streamWriter.Close();
                        Output += "Карта сохранена.";
                    }
                    catch
                    {
                        Output += "Неправильное имя файла.";
                    }
                }
                Output += SaveText();
            }
            Output = "";
        }
        static string SaveText()
        {
            return "\nВведите имя файла.\nesc – выход.";
        }

        //Меню добавления символа на карту.
        static void SymbolPut(char selectedSymbol)
        {
            //Координаты, на которых заменяется символ. Изменяются при командах w, a, s, d.
            int x = 0, y = 0;
            bool actionCommited = false;
            Output += SymbolPutText(x, y);
            while (!actionCommited)
            {
                SceneWrite();
                switch (Input)
                {
                    case "w":
                        y--;
                        if (y < 0)
                        {
                            y++;
                            Output = "Граница карты.";
                        }
                        break;
                    case "a":
                        x--;
                        if (x < 0)
                        {
                            x++;
                            Output = "Граница карты.";
                        }
                        break;
                    case "s":
                        y++;
                        if (y >= _array.GetLength(1))
                        {
                            y--;
                            Output = "Граница карты.";
                        }
                        break;
                    case "d":
                        x++;
                        if (x >= _array.GetLength(0))
                        {
                            x--;
                            Output = "Граница карты.";
                        }
                        break;
                    case "esc":
                        Output = "Действие прервано.";
                        actionCommited = true;
                        break;
                    case "enter":
                        Output = "Вы поставили " + selectedSymbol + ".";
                        _array[x, y] = selectedSymbol;
                        break;
                }
                Output += SymbolPutText(x, y);
            }
            Output = "";
        }
        //В месте входных координат отображается символ 8.
        static string SymbolPutText(int x, int y)
        {
            string output;
            output = "\n";
            for (int indexY = 0; indexY < 9; indexY++)
            {
                for (int indexX = 0; indexX < 9; indexX++)
                {
                    if (indexX == x && indexY == y) output += 8;
                    else output += _array[indexX, indexY];
                }
                output += '\n';
            }
            output += "wasd: управление\nenter: поставить символ\nesc: прервать действие";
            return output;
        }

        //Меню добавления символа на карту прохождения.
        static void AddWalkThrough()
        {
            int x = 0;
            int y = 0;
            bool Adding = true;
            char[,] _arrayWalk = new char[_array.GetLength(0), _array.GetLength(1)];
            for (int i = 0; i < _arrayWalk.Length; i++)
            {
                _arrayWalk[i - (_arrayWalk.GetLength(0) * i / _arrayWalk.GetLength(1)), i / _arrayWalk.GetLength(1)] = ' ';
            }
            Output = AddWalkThroughText(x, y, _arrayWalk);

            while (Adding)
            {
                SceneWrite();
                switch (Input)
                {
                    case "w":
                        y--;
                        if (y < 0)
                        {
                            y++;
                            Output = "Граница карты.";
                        }
                        break;
                    case "a":
                        x--;
                        if (x < 0)
                        {
                            x++;
                            Output = "Граница карты.";
                        }
                        break;
                    case "s":
                        y++;
                        if (y >= _array.GetLength(1))
                        {
                            y--;
                            Output = "Граница карты.";
                        }
                        break;
                    case "d":
                        x++;
                        if (x >= _array.GetLength(0))
                        {
                            x--;
                            Output = "Граница карты.";
                        }
                        break;
                    case "esc":
                        Output = "Действие прервано.";
                        Adding = false;
                        break;
                    case "enter":
                        Output = "Вы поставили точку.";
                        _arrayWalk[x, y] = '°';
                        break;
                    case "del":
                        Output = "Вы удалили точку.";
                        _arrayWalk[x, y] = ' ';
                        break;
                    case "save":
                        //В папке с картами создаётся папка с решениями лабиринтов, куда записывается файл с именем его карты.
                        StreamWriter sw = new StreamWriter(@"walkthroughs\" + arrayName);
                        for (int _indexY = 0; _indexY < _arrayWalk.GetLength(1); _indexY++)
                        {
                            string line = "";
                            for (int _indexX = 0; _indexX < _arrayWalk.GetLength(0); _indexX++)
                            {
                                line += _arrayWalk[_indexX, _indexY];
                            }
                            if (_indexY == _array.GetLength(1) - 1) sw.Write(line);
                            else sw.WriteLine(line);
                        }
                        sw.Close();
                        Output = "Карта сохранена.";
                        break;
                }
                Output += AddWalkThroughText(x, y, _arrayWalk);
            }
            Output = "";
        }
        static string AddWalkThroughText(int x, int y, char[,] _arrayWalk)
        {
            string _output;
            _output = "\n";
            for (int _indexY = 0; _indexY < 9; _indexY++)
            {
                for (int _indexX = 0; _indexX < 9; _indexX++)
                {
                    if (_indexX == x && _indexY == y) _output += 8;
                    else _output += _arrayWalk[_indexX, _indexY];
                }
                _output += '\n';
            }
            for (int i = 0; i < _array.GetLength(0); i++) _output += '-';
            _output += '\n' + MapString(_array) + "\nwasd: управление\nenter: поставить точку\ndel: удалить точку:\nsave: сохранить прохождение\nesc: закрыть редактор прохождений";
            return _output;
        }
    }
}