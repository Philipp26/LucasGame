using System;
using System.Linq;

namespace Lucas
{
    class Creep
    {
        public static Tuple<int, int> Creepy 
            = new Tuple<int, int>(3, 3);

        static Tuple <int,int> [] CalculatingAreaAboutPlayer()
        {
            int[] d = { -1, 0, 1 };

            var x = Player.CurrentPlayerPosition.Item1;
            var y = Player.CurrentPlayerPosition.Item2;

            var array = d
                .SelectMany(dx => d.Select(dy => new Tuple <int, int> (x + dx, y + dy)))
                .Where(tuple => !tuple.Equals(Player.CurrentPlayerPosition))
                .ToArray();

            return array;
        }

        public static Tuple<int, int> CalculatingCoordinateCreeps()
        {
            var arr = CalculatingAreaAboutPlayer();

            int randomX = 0, randomY = 0;

            randomX = randomX.SelectRandomXCoordinate(Creepy.Item1, arr);
            randomY = randomY.SelectRandomYCoordinate(Creepy.Item2, arr);

            Creepy = new Tuple<int, int>(randomX, randomY);

            return Creepy;
        }

        public static Tuple<int, int> CreepTrack()
        {
            var x = Creepy.Item1;
            var y = Creepy.Item2;

            var tuple = Creepy;
            switch (new Random().Next(0, 4))
            {
                case 0:
                    y--;
                    break;
                case 1:
                    x--;
                    break;
                case 2:
                    x++;
                    break;
                case 3:
                    y++;
                    break;
            }

            tuple = new Tuple<int, int>(x, y);

            if (Program.CheckedOutOfRange(tuple, Settings.strArray))
            {
                Creepy = tuple;
                return Creepy;
            }
            else return Creepy;
        }

    }
}
