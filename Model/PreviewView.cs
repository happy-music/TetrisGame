using CommunityToolkit.Mvvm.ComponentModel;
using TetrisGame.Mode;

namespace TetrisGame.Model
{
    internal partial class PreviewView : ObservableObject
    {
        [ObservableProperty]
        private int[,] _data = new int[4, 4];

        public Unit? NextUnit { get; set; }

        public Unit GetNextUnit()
        {
            Unit _unit = NextUnit ?? GenerateUnit();
            NextUnit= GenerateUnit();
            Data = NextUnit.Data;
            return _unit;
        }

        private static Unit GenerateUnit()
        {
            Unit _unit ;
            switch (Random.Shared.Next(0, 4))
            {
                case 0:
                    _unit = new LUnit(); break;
                case 1:
                    _unit = new LineUnit(); break;
                case 2:
                    _unit = new TUnit(); break;
                case 3:
                    _unit = new TianUnit(); break;
                default:
                    _unit = new YUnit(); break;
            }
            return _unit;
        }
    }
}
