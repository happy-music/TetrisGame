using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;
using TetrisGame.CustomControl;
using TetrisGame.Model;

namespace TetrisGame
{
    internal partial class TetrisViewModel : ObservableObject
    {
        private readonly int _row = 20;
        private readonly int _column = 12;
        //下降时间
        private TimeSpan _downTime;

        private CancellationTokenSource? _cts;
        private MainView _mainView;

        [ObservableProperty]
        private PreviewView _previewView;

        [ObservableProperty]
        private int[,] mainDatas;

        [ObservableProperty]
        private int score;

        public TetrisViewModel()
        {
            _previewView = new PreviewView();
            _downTime = TimeSpan.FromSeconds(0.3);
            Initial();
        }
    
        private void Initial()
        {
            Score = 0;
            _mainView = new MainView(_row, _column);
            MainDatas = new int[_row, _column];
        }


        [RelayCommand]
        private async Task Start(TetrisControl tetrisControl)
        {
            try
            {
                _cts = new CancellationTokenSource();

                while (!_cts.IsCancellationRequested)
                {
                    if (_mainView.CurrentUnit is null)
                    {
                        _mainView.CurrentUnit = PreviewView.GetNextUnit();
                        MainDatas = _mainView.GetMainViewData();
                    }

                    await Task.Delay(_downTime, _cts.Token);

                    var canDown = _mainView.Down();

                    if (!canDown)
                    {
                        _mainView.Data = _mainView.GetMainViewData();
                        MainDatas = _mainView.Data;

                        //检查并消除
                        (int count,List<int> eliminateRows) = _mainView.CheckAndEliminate();
                        Score += count;
                        tetrisControl.EliminateAnimation(eliminateRows); 

                        if (_mainView.CheckGameOver())
                            GameOver();

                        await Task.Delay(1000, _cts.Token);
                        _mainView.CurrentUnit = null;
                    }
                    else
                    {
                        MainDatas = _mainView.GetMainViewData();
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // ignored
            }
        }

        [RelayCommand]
        private void Suspent()
        {
            _cts?.Cancel();
        }

        [RelayCommand]
        private void Stop()
        {
            Suspent();
            Initial();
        }

        [RelayCommand]
        private void KeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right: _mainView.Move(true); break;
                case Key.Left: _mainView.Move(false); break;
                case Key.Down: _mainView.Down(); break;
                case Key.Up: _mainView.Flip(); break;
                default: return;
            }
            MainDatas = _mainView.GetMainViewData();
        }

        private void GameOver()
        {
            MessageBox.Show($"Game Over,Score:{Score}");
            Stop();
        }
    }
}
