using System;

namespace Lucas
{
    class Program
    {
        public static void Main()
        {
            Menu();
        }

        static void ProgressBar()
        {
            for (int i = 0; i <= 100; i += 2)
            {
                Console.Write("\r{0}% ", i);
                System.Threading.Thread.Sleep(50);
            }

            Console.Write("\n");
        }
     
        public static void Menu()
        {
            ProgressBar();
            Console.Clear();

            while (true)
            {
                Console.WriteLine("   - = Lucas = -\n\n       Menu\n\n   a) Start Game\n   b) Highcores\n   c) Exit Game");
                var key = Console.ReadKey(false);
                Console.Write("\r");               
                switch (key.Key)
                {
                    case ConsoleKey.A:
                        Console.Clear();
                        ProgressBar();
                        Console.Clear();
                        Start();
                        break;
                    case ConsoleKey.B:
                        Console.Clear();
                        Console.WriteLine("");
                        break;
                    case ConsoleKey.C:
                        Console.Clear();
                        ProgressBar();
                        Environment.Exit(0);
                        break;                 
                }

                Console.WriteLine("Please try input again");
                Console.WriteLine("Press any key to continue...");                                
                Console.ReadKey();
                ProgressBar();
                Console.Clear();                
            }
        }

        public static void Start()
        {
            FirstFieldShow();

            while (true)
            {
                Output();

                System.Threading.Thread.Sleep(20);
            }
        }

        static void FirstFieldShow()
        {
            string[,] field = new string[Settings.xSize, Settings.ySize];

            field[Player.CurrentPlayerPosition.Item1, Player.CurrentPlayerPosition.Item2] = "L";
            field[Creep.Creepy.Item1, Creep.Creepy.Item2] = "P";

            CreateArray(field);
            Output(field);
        }

        /// <summary>
        /// Инициализация массива с записанными игровыми объектами
        /// </summary>
        /// <returns>Матрица игрового поля, с записаными игровыми объектами</returns>
        public static string[,] ArrayWriter()
        {
            if (UnitIsDead(Creep.Creepy, Player.CurrentPlayerPosition)) GameOver();
            string[,] field = new string[Settings.xSize, Settings.ySize];

            var tupleCreep = Creep.Creepy;
            var tuplePlayer = Player.CurrentPlayerPosition;

            field[tupleCreep.Item1, tupleCreep.Item2] = "P";

            if (Player.ShootTrace != null
                && CheckedOutOfRange(Player.ShootTrace, field)
                && !UnitIsDead(tupleCreep, Player.ShootTrace))
            {
                field[Player.ShootTrace.Item1, Player.ShootTrace.Item2] = "*";
                field[Player.CurrentPlayerPosition.Item1, Player.CurrentPlayerPosition.Item2] = "L";

                Player.ShootTrace = Player.PlayerShootTraceHandler(Player.DirectionTrace);

                CreateArray(field);
                Console.Clear();

                return field;
            }

            else if (UnitIsDead(tupleCreep, Player.ShootTrace))
            {
                field[Player.ShootTrace.Item1, Player.ShootTrace.Item2] = "";
                Player.ShootTrace = null;
                tupleCreep = null;
                tupleCreep = Creep.CalculatingCoordinateCreeps();
            }

            else Player.ShootTrace = null;

            if (tupleCreep != null)
            {
                tupleCreep = Creep.CreepTrack();
            }

            tuplePlayer = Player.ControlPlayer();

            if (CheckedOutOfRange(Player.CurrentPlayerPosition, field))
              
                field[tuplePlayer.Item1, tuplePlayer.Item2] = "L";      

            else GameOver();            

            CreateArray(field);

            Console.Clear();

            return field;
        }

        /// <summary>
        /// Создание игрового поля 
        /// </summary>
        /// <param name="field">Матрица игрового поля</param>
        public static void CreateArray(string[,] field)
        {
            for (int i = 0; i < field.GetLength(0); i++)
                for (int j = 0; j < field.GetLength(1); j++)
                    if (j == 0 || j == field.GetLength(1) - 1
                       || i == 0 || i == field.GetLength(0) - 1)

                        field[i, j] = "#";
        }

        /// <summary>
        /// Вывод массива на экран консоли
        /// </summary>
        public static void Output()
        {
            var arr = ArrayWriter();

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (String.IsNullOrEmpty(arr[i, j]))
                        Console.Write(" ");

                    else Console.Write(arr[i, j]);
                }

                Console.WriteLine("");
            }
            if (UnitIsDead(Creep.Creepy, Player.CurrentPlayerPosition)) GameOver();
        }

        public static void Output(string[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (String.IsNullOrEmpty(arr[i, j]))
                        Console.Write(" ");

                    else Console.Write(arr[i, j]);
                }

                Console.WriteLine("");
            }
        }

        /// <summary>
        /// Метод для проверки столкновения игрока со стенкой
        /// </summary>
        /// <param name="TuplePosition">Координаты игрока</param>
        /// <param name="field">Матрица игрового поля</param>
        /// <returns></returns>
        public static bool CheckedOutOfRange(Tuple<int, int> TuplePosition, string[,] field)
        {
            return TuplePosition.Item1 < field.GetLength(0) - 1
                   && TuplePosition.Item1 > 0
                   && TuplePosition.Item2 < field.GetLength(1) - 1
                   && TuplePosition.Item2 > 0;
        }


        public static bool UnitIsDead(Tuple<int, int> creep, Tuple<int, int> goal)
        {
            return goal == null ? false : (creep.Item1 == goal.Item1 && creep.Item2 == goal.Item2);
        }

        static void GameOver()
        {
            Player.CurrentPlayerPosition = new Tuple<int, int>(Settings.xSize / 2, Settings.ySize / 2);
            Console.WriteLine("\rLucas is DEAD!!!");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
            System.Threading.Thread.Sleep(100);
            Menu();
        }


    }
}
