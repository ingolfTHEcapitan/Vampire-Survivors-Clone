using System;
using _Project.Scripts.Logic.Player;
using _Project.Scripts.Logic.Player.Components;
using _Project.Scripts.Services.GamePause;
using _Project.Scripts.Services.SceneLoader;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.UI.Windows.GameOver
{
    public class GameOverWindowModel : IDisposable
    {
        public event Action OnPlayerDied;
        public event Action OnRewardedAdLoaded;
        
        private readonly IGamePauseService _pauseService;
        private readonly SceneLoaderService _sceneLoader;
        private readonly LazyInject<PlayerContainer> _lazyPlayer;
        
        private  PlayerDeath _playerDeath;
        private bool _reviveInThisSession;
        
        public GameOverWindowModel(IGamePauseService pauseService, SceneLoaderService sceneLoader, LazyInject<PlayerContainer> lazyPlayer)
        {
            _pauseService = pauseService;
            _sceneLoader = sceneLoader;
            _lazyPlayer = lazyPlayer;
        }
        
        public void Initialize()
        {
            _playerDeath = _lazyPlayer.Value.PlayerDeath;
            _playerDeath.OnDied += InvokeOnPlayerDied;
        }

        public void Dispose()
        {
            _playerDeath.OnDied -= InvokeOnPlayerDied;
        }
        
        public void SetPaused(bool paused) => 
            _pauseService.SetPaused(paused);

        public async UniTask ReloadSceneAsync()
        {
            _reviveInThisSession = false;
            await _sceneLoader.ReloadAsync();
        }
        
        private void InvokeOnPlayerDied() => 
            OnPlayerDied?.Invoke();
    }
}