using TetrisGame.Model;

namespace TetrisGame.Mode
{
   internal class YUnit : Unit
    {
        public YUnit() : base(3)
        {
        }

        public override int[,] GetFlipPosition(int flipState)
        {
            int[,] ints = new int[4, 4];
            switch (flipState)
            {
                case 0:
                    ints[0, 1] = ColorCode;
                    ints[1, 1] = ColorCode;
                    ints[1, 2] = ColorCode;
                    ints[2, 2] = ColorCode;
                    break;
                case 1:
                    ints[1, 1] = ColorCode;
                    ints[1, 2] = ColorCode;
                    ints[2, 0] = ColorCode;
                    ints[2, 1] = ColorCode;
                    break;
                case 2:
                    ints[0, 0] = ColorCode;
                    ints[1, 0] = ColorCode;
                    ints[1, 1] = ColorCode;
                    ints[2, 1] = ColorCode;
                    break;
                case 3:
                    ints[0, 1] = ColorCode;
                    ints[0, 2] = ColorCode;
                    ints[1, 0] = ColorCode;
                    ints[1, 1] = ColorCode;
                    break;
            }
            return ints;
        }
    }
}
