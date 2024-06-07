namespace TetrisGame.Model
{
    public abstract class Unit
    {
        private int flipState = 0;
        protected int FlipState
        {
            get => flipState;
            set => flipState = value % 4;
        }

        public int ColorCode { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int[,] Data => GetFlipPosition(FlipState);
        protected Unit() : this(1) { }

        protected Unit(int colorCode)
        {
            X = 0;
            Y = 0;
            ColorCode = colorCode;
        }

        public virtual void Flip(bool rightFlip)
        {
            FlipState += rightFlip ? 1 : 3;
        }

        /// <summary>
        /// 获取在指定容器中位置 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public virtual int[,] GetContainPosition(int row, int column) => Foo(new int[row, column], FlipState, X, Y);

        /// <summary>
        /// 获取下降的位置
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public virtual int[,] GetDownPosition(int row, int column) => Foo(new int[row, column], FlipState, X + 1, Y);

        public virtual int[,] GetRightMovePosition(int row, int column) => Foo(new int[row, column], FlipState, X, Y + 1);

        public virtual int[,] GetLeftMovePosition(int row, int column) => Foo(new int[row, column], FlipState, X, Y - 1);

        /// <summary>
        /// 获取翻转后的位置
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public virtual int[,] GetFlipContainPosition(int row, int column) => Foo(new int[row, column], (FlipState + 1) % 4, X, Y);

        public abstract int[,] GetFlipPosition(int flipState);

        public int[,] Foo(int[,] mainContain, int flipState, int xOffset, int yOffset)
        {
            var unitContain = GetFlipPosition(flipState);
            for (int i = 0; i < unitContain.GetLength(0); i++)
            {
                for (int j = 0; j < unitContain.GetLength(1); j++)
                {
                    if (unitContain[i, j] != 0 && xOffset + i >= 0 && xOffset + i < mainContain.GetLength(0) && yOffset + j >= 0 && yOffset + j < mainContain.GetLength(1))
                    {
                        mainContain[xOffset + i, yOffset + j] = unitContain[i, j];
                    }
                }
            }
            return mainContain;
        }
    }
}
