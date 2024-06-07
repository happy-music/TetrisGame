namespace TetrisGame.Model
{
    internal class TianUnit : Unit
    {
        public TianUnit() : base(3) { }

        public override int[,] GetFlipPosition(int flipState)
        {
            int[,] ints = new int[4, 4];
            switch (flipState)
            {
                 default:
                    ints[1, 1] = ColorCode;
                    ints[1, 2] = ColorCode;
                    ints[2, 1] = ColorCode;
                    ints[2, 2] = ColorCode;
                    break;
            }
            return ints;
        }
    }

}

