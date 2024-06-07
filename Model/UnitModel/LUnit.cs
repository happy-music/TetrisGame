namespace TetrisGame.Model
{
    internal class LUnit : Unit
    {
        public LUnit() : base(4) { }

        public override int[,] GetFlipPosition(int flipState)
        {
            int[,] ints = new int[4, 4];
            switch (flipState)
            {
                case 0:
                    ints[1, 1] = ColorCode;
                    ints[1, 2] = ColorCode;
                    ints[2, 2] = ColorCode;
                    ints[3, 2] = ColorCode;
                    break;
                case 1:
                    ints[1, 1] = ColorCode;
                    ints[2, 1] = ColorCode;
                    ints[2, 2] = ColorCode;
                    ints[2, 3] = ColorCode;
                    break;
                case 2:
                    ints[1, 2] = ColorCode;
                    ints[2, 2] = ColorCode;
                    ints[3, 2] = ColorCode;
                    ints[3, 3] = ColorCode;
                    break;
                case 3:
                    ints[2, 1] = ColorCode;
                    ints[2, 2] = ColorCode;
                    ints[2, 3] = ColorCode;
                    ints[3, 1] = ColorCode;
                    break;
            }
            return ints;
        }
    }
}

