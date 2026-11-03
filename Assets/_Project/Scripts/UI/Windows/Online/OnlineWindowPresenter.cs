using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.UI.Windows.Online
{
    public class OnlineWindowPresenter
    {
        private readonly OnlineWindowView _view;
        private readonly OnlineWindowModel _model;

        public OnlineWindowPresenter(OnlineWindowView view, OnlineWindowModel model)
        {
            _view = view;
            _model = model;
        }
        
        public void Initialize()
        {
            _view.Initialize();
            _model.Initialize();
            _view.OnBackButtonClicked += Close;
        }

        public async UniTask OpenAsync()
        {
            await _view.OpenAsync();
        }

        private async void Close()
        {
            try
            {
                await _view.CloseAsync();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}