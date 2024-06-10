using CommunityToolkit.Mvvm.ComponentModel;

namespace TetrisGame.Model
{
    internal class MainView
    {
        //缓冲行
        private readonly int bufferRow = 3;
        private readonly int _row;
        private readonly int _column;

        public int[,] Data { get; set; }

        public Unit CurrentUnit { get; set; }

        public MainView(int row, int column)
        {
            _row = row;
            _column = column;
            Data = new int[_row, _column];
        }

        public int[,] GetMainViewData()
        {
            var temporaryData = (int[,])Data.Clone();

            if (CurrentUnit is null)
            {
                return Data;
            }
            //上方预留缓冲区
            var unit = CurrentUnit.GetContainPosition(_row + bufferRow, _column);

            for (int i = 0; i < Data.GetLength(0); i++)
            {
                for (int j = 0; j < Data.GetLength(1); j++)
                {
                    if (unit[i + bufferRow, j] != 0)
                    {
                        temporaryData[i, j] = unit[i + bufferRow, j];
                    }
                }
            }

            return temporaryData;
        }

        /// <summary>
        /// 检查并消除
        /// </summary>
        /// <returns></returns>
        public (int count, List<int> eliminateRows) CheckAndEliminate()
        {
            List<int> eliminateRows = [];
            int eliminateCount = 0;
            int pointer = _row - 1;
            for (int i = _row - 1; i >= 0; i--)
            {
                if (CanRemove(i))
                {
                    eliminateRows.Add(i);
                    eliminateCount++;
                    continue;
                }

                if (i != pointer)
                {
                    for (int j = 0; j < _column; j++)
                    {
                        Data[pointer, j] = Data[i, j];
                    }
                }

                pointer--;
            }

            //清除多余行数据
            for (int i = pointer; i >= 0; i--)
            {
                for (int j = 0; j < _column; j++)
                {
                    Data[i, j] = 0;
                }
            }

            return (eliminateCount, eliminateRows);

            //检查该行是否可以消除
            bool CanRemove(int row)
            {
                for (int i = 0; i < _column; i++)
                {
                    if (Data[row, i] == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }


        /// <summary>
        /// 下降
        /// </summary>
        /// <returns></returns>
        public bool Down()
        {
            lock (this)
            {
                if (InLowerBoundary())
                    return false;

                var temporaryData = CurrentUnit.GetDownPosition(_row + bufferRow, _column);

                if (IsOverlap(temporaryData))
                    return false;

                CurrentUnit.X++;
                return true;
            }
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="rightMove">是否右移</param>
        /// <returns></returns>
        public bool Move(bool rightMove)
        {
            if (rightMove && !InRightBoundary())
            {
                return RightMove();
            }
            else if (!rightMove && !InLeftBoundary())
            {
                return LeftMove();
            }
            return false;
        }

        /// <summary>
        /// 翻转
        /// </summary>
        public bool Flip()
        {
            var temporaryData = CurrentUnit.GetFlipContainPosition(_row + bufferRow, _column);
            if (IsOverlap(temporaryData) || CheckOutOfBound(temporaryData))
                return false;

            CurrentUnit.Flip(true);

            return true;
        }

        /// <summary>
        /// 检查游戏是否结束
        /// </summary>
        /// <returns></returns>
        public bool CheckGameOver()
        {
            var temporaryData = CurrentUnit.GetContainPosition(_row + bufferRow, _column);
            return CheckOutOfBound(temporaryData);
        }

        private bool RightMove()
        {
            var temporaryData = CurrentUnit.GetRightMovePosition(_row + bufferRow, _column);
            if (IsOverlap(temporaryData)) return false;
            CurrentUnit.Y++;

            return true;
        }

        private bool LeftMove()
        {
            var temporaryData = CurrentUnit.GetLeftMovePosition(_row + bufferRow, _column);
            if (IsOverlap(temporaryData)) return false;
            CurrentUnit.Y--;

            return true;
        }

        /// <summary>
        /// 在底部
        /// </summary>
        /// <returns></returns>
        private bool InLowerBoundary()
        {
            var temporaryData = CurrentUnit.GetContainPosition(_row + bufferRow, _column);
            for (int i = 0; i < Data.GetLength(1); i++)
            {
                if (temporaryData[_row - 1 + bufferRow, i] != 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool InLeftBoundary()
        {
            var temporaryData = CurrentUnit.GetContainPosition(_row, _column);
            for (int i = 0; i < temporaryData.GetLength(0); i++)
            {
                if (temporaryData[i, 0] != 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool InRightBoundary()
        {
            var temporaryData = CurrentUnit.GetContainPosition(_row, _column);
            for (int i = 0; i < temporaryData.GetLength(0); i++)
            {
                if (temporaryData[i, _column - 1] != 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsOverlap(int[,] unit)
        {
            for (int i = 0; i < Data.GetLength(0); i++)
            {
                for (int j = 0; j < Data.GetLength(1); j++)
                {
                    if (unit[i + bufferRow, j] != 0 && Data[i, j] != 0)
                        return true;
                }
            }
            return false;
        }

        private bool CheckOutOfBound(int[,] unit)
        {
            //容器内方块
            int count = 0;
            for (int i = 0; i < Data.GetLength(0); i++)
            {
                for (int j = 0; j < Data.GetLength(1); j++)
                {
                    if (unit[i + bufferRow, j] != 0)
                        count++;
                }
            }
            return count < 4;
        }
    }
}
