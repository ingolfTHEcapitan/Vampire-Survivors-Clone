using _Project.Scripts.Logic.Common;
using _Project.Scripts.Logic.Player;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Common.HealthBar;
using _Project.Scripts.UI.HUD;
using _Project.Scripts.UI.Windows.GameOver;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.Game
{
    public class GameUIInitializer
    {
        private readonly WindowFactory _windowFactory;
        private readonly Transform _uiParent;
        private readonly HealthBarFactory _healthBarFactory;
        private readonly LazyInject<HeadUpDisplayPresenter> _lazyHudPresenter;
        private readonly LazyInject<GameOverWindowPresenter> _lazyGameOverWindowPresenter;
        private readonly LazyInject<PlayerContainer> _lazyPlayer;

        public GameUIInitializer(WindowFactory windowFactory, Transform uiParent, HealthBarFactory healthBarFactory, 
            LazyInject<HeadUpDisplayPresenter> lazyHudPresenter, LazyInject<GameOverWindowPresenter> lazyGameOverWindowPresenter, 
            LazyInject<PlayerContainer> lazyPlayer)
        {
            _windowFactory = windowFactory;
            _uiParent = uiParent;
            _lazyGameOverWindowPresenter = lazyGameOverWindowPresenter;
            _lazyHudPresenter = lazyHudPresenter;
            _healthBarFactory = healthBarFactory;
            _lazyPlayer = lazyPlayer;
        }
        
        public async UniTask InitUIAsync()
        {
            HeadUpDisplayView hudView = await _windowFactory.CreateAsync<HeadUpDisplayView>(_uiParent);
            _lazyHudPresenter.Value.Initialize();

            CreatePlayerHealthBar(hudView, _lazyPlayer.Value.Health);

            await _windowFactory.CreateAsync<GameOverWindowView>(_uiParent);
            _lazyGameOverWindowPresenter.Value.Initialize();
        }

        private void CreatePlayerHealthBar(HeadUpDisplayView hud, Health playerHealthModel) => 
            _healthBarFactory.Create(hud.HealthBarView, playerHealthModel, hideOnZeroHealth: false);
        
    }
}