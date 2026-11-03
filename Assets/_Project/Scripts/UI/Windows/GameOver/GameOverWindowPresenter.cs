using System;
using _Project.Scripts.UI.Common;
using UnityEngine;

namespace _Project.Scripts.UI.Windows.GameOver
{
    public class GameOverWindowPresenter
    {
        private readonly GameOverWindowView _view;
        private readonly GameOverWindowModel _model;
        private readonly CursorController _cursorController;

        public GameOverWindowPresenter(GameOverWindowView view, GameOverWindowModel model, CursorController cursorController)
        {
            _model = model;
            _view = view;
            _cursorController = cursorController;
        }

        public void Initialize()
        {
            _view.Initialize();
            _model.Initialize();

            _view.OnWindowDestroy += CleanUp;
            _view.OnLoadSaveButtonClicked += LoadSave;
            _model.OnPlayerDied += Open;
        }

        private void CleanUp()
        {
            _view.OnWindowDestroy -= CleanUp;
            _view.OnLoadSaveButtonClicked -= LoadSave;
            _model.OnPlayerDied -= Open;
            _model.Dispose();
        }

        private async void LoadSave()
        {
            try
            {
                await _view.CloseAsync();
                _model.SetPaused(false);
                _cursorController.SetCursorVisible(true);
                
                _cursorController.SetCursorVisible(false);
                await _model.ReloadSceneAsync();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        
        private async void Open()
        {
            try
            {
                _model.SetPaused(true);
                await _view.OpenAsync();
                _cursorController.SetCursorVisible(true);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}