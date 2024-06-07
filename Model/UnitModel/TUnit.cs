using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Model
{
    internal class TUnit : Unit
    {
        public TUnit() : base(2) { }

        public override int[,] GetFlipPosition(int flipState)
        {
            int[,] ints = new int[4, 4];
            switch (flipState)
            {
                case 0:
                    ints[0, 1] = ColorCode;
                    ints[1, 1] = ColorCode;
                    ints[1, 2] = ColorCode;
                    ints[2, 1] = ColorCode;
                    break;
                case 1:
                    ints[1, 0] = ColorCode;
                    ints[1, 1] = ColorCode;
                    ints[1, 2] = ColorCode;
                    ints[2, 1] = ColorCode;
                    break;
                case 2:
                    ints[0, 1] = ColorCode;
                    ints[1, 0] = ColorCode;
                    ints[1, 1] = ColorCode;
                    ints[2, 1] = ColorCode;
                    break;
                case 3:
                    ints[0, 1] = ColorCode;
                    ints[1, 0] = ColorCode;
                    ints[1, 1] = ColorCode;
                    ints[1, 2] = ColorCode;
                    break;
            }
            return ints;
        }
    }
}
