using System;
using System.Linq;

namespace Lucas
{
    static class ArrayExtensionClass
    {
        public static int XMinFromArray(this Tuple<int, int> [] arr)
        {
            return arr.Select(i => i.Item1).Min(j => j);
        }

        public static int XMaxFromArray(this Tuple<int, int> [] arr)
        {
            return arr.Select(i => i.Item1).Max(j => j);
        }

        public static int YMinFromArray(this Tuple <int, int> [] arr)
        {
            return arr.Select(i => i.Item2).Min(j => j);
        }

        public static int YMaxFromArray(this Tuple <int, int> [] arr)
        {
            return arr.Select(i => i.Item2).Max(j => j);
        }

        public static bool XMinRange(int attribute, params Tuple <int, int>[] arr)
        {
            return attribute > 1 && attribute < arr.XMinFromArray();
        }

        public static bool XMaxRange(int attribute, params Tuple<int, int>[] arr)
        {
            return attribute < Settings.xSize - 1 && attribute > arr.XMaxFromArray();
        }

        public static bool YMinRange(int attribute, params Tuple<int, int>[] arr)
        {
            return attribute > 1 && attribute < arr.YMinFromArray();
        }

        public static bool YMaxRange(int attribute, params Tuple<int, int>[] arr)
        {
            return attribute < Settings.xSize - 1 && attribute > arr.YMaxFromArray();
        }

        static int SelectRandomLocation()
        {
            return new Random().Next(0, 2);
        }

        public static int SelectRandomXCoordinate(this int coordinate, int attribute, Tuple<int, int>[] arr)
        {
            Random rnd = new Random();

            if (XMaxRange(attribute, arr))
                return rnd.Next(arr.XMaxFromArray(), Settings.xSize - 1);            

            else if (XMinRange(attribute, arr))
                return rnd.Next(1, arr.XMinFromArray());

            else return rnd.Next(1, Settings.xSize - 1);
        }

        public static int SelectRandomYCoordinate(this int coordinate, int attribute, Tuple<int, int>[] arr)
        {
            Random rnd = new Random();

            if (YMaxRange(attribute, arr))
                return rnd.Next(arr.YMaxFromArray(), Settings.xSize - 1);            

            else if (YMinRange(attribute, arr))
                return rnd.Next(1, arr.YMinFromArray());

            else return rnd.Next(1, Settings.xSize - 1);
        }
    }
}
