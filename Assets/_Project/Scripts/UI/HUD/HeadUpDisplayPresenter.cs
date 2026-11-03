using _Project.Scripts.Services.PlayerInput;
using _Project.Scripts.UI.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.UI.HUD
{
    public class HeadUpDisplayPresenter
    {
        private readonly HeadUpDisplayModel _model;
        private readonly HeadUpDisplayView _view;

        public HeadUpDisplayPresenter(HeadUpDisplayModel model, HeadUpDisplayView view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize()
        {
            _view.OnWindowDestroy += CleanUp;
        }

        private void CleanUp()
        {
            _view.OnWindowDestroy -= CleanUp;
        }
        
    }
}