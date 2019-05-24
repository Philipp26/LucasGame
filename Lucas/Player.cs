using System;

namespace Lucas
{
    class Player
    {
        /// <summary>
        /// Кортеж для хранения координат начальной позиции игрока
        /// </summary>
        internal static Tuple <int, int> CurrentPlayerPosition
            = new Tuple <int, int> (2, 2);

        internal static ConsoleKey DirectionTrace;

        /// <summary>
        /// Кортеж для хранения координат следа от выстрела
        /// </summary>
        internal static Tuple <int, int> ShootTrace;

        /// <summary>
        /// Метод для изменения координат игрока в зависимости от нажатой клавиши
        /// </summary>
        /// <returns>Текущие координаты игрока</returns>
        public static Tuple <int, int> ControlPlayer()
        {
            var key = Console.ReadKey(false);
            Console.Write("\r");

            var x = CurrentPlayerPosition.Item1;
            var y = CurrentPlayerPosition.Item2;

            switch (key.Key)
            {
                case ConsoleKey.S:
                    x++;
                    break;
                case ConsoleKey.W:
                    x--;
                    break;
                case ConsoleKey.A:
                    y--;
                    break;
                case ConsoleKey.D:
                    y++;
                    break;
                case ConsoleKey.LeftArrow:
                    PlayerShootTraceHandler(ConsoleKey.LeftArrow);
                    break;
                case ConsoleKey.RightArrow:
                    PlayerShootTraceHandler(ConsoleKey.RightArrow);
                    break;
                case ConsoleKey.UpArrow:
                    PlayerShootTraceHandler(ConsoleKey.UpArrow);
                    break;
                case ConsoleKey.DownArrow:
                    PlayerShootTraceHandler(ConsoleKey.DownArrow);
                    break;
            }           

            CurrentPlayerPosition = new Tuple <int, int> (x, y);

            return CurrentPlayerPosition;
        }

        public static Tuple <int, int> PlayerShootTraceHandler(ConsoleKey key)
        {
            int x;
            int y;

            if (ShootTrace == null)
            {
                x = CurrentPlayerPosition.Item1;
                y = CurrentPlayerPosition.Item2;
            }

            else
            {
                x = ShootTrace.Item1;
                y = ShootTrace.Item2;
            }

            DirectionTrace = key;

            switch (DirectionTrace)
            {                
                case ConsoleKey.LeftArrow:
                    y--;
                    break;
                case ConsoleKey.RightArrow:
                    y++;
                    break;
                case ConsoleKey.UpArrow:
                    x--;
                    break;
                case ConsoleKey.DownArrow:
                    x++;
                    break;
            }

            ShootTrace = new Tuple <int, int> (x, y);
            return ShootTrace;            
        }
    }
}
