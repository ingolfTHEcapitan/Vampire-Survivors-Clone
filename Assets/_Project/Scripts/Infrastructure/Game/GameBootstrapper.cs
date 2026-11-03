using System;
using _Project.Scripts.Logic.Spawners;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Audio;
using _Project.Scripts.UI.Windows.LoadingCurtain;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.Game
{
    public class GameBootstrapper : IInitializable
    {
        private readonly EffectsService _effectsService;
        private readonly PlayerSpawner _playerSpawner;
        private readonly GameUIInitializer _uiInitializer;
        private readonly LoadingCurtainPresenter _loadingCurtainPresenter;
        private readonly IMusicService _audioService;
        private readonly AudioClip _gameplayMusic;
        
        public GameBootstrapper(EffectsService effectsService, PlayerSpawner playerSpawner, GameUIInitializer uiInitializer, 
            AudioClip gameplayMusic, LoadingCurtainPresenter loadingCurtainPresenter, IMusicService audioService)
        {
            _effectsService = effectsService;
            _playerSpawner = playerSpawner;
            _uiInitializer = uiInitializer;
            _loadingCurtainPresenter = loadingCurtainPresenter;
            _audioService = audioService;
            _gameplayMusic = gameplayMusic;
        }

        public void Initialize() => 
            InitializeAsync().Forget();

        private async UniTaskVoid InitializeAsync()
        {
            try
            {
                await _effectsService.WarmUpAsync();
                await _playerSpawner.SpawnAsync();
                await _uiInitializer.InitUIAsync();
                
                _audioService.PlayMusic(_gameplayMusic);
                _loadingCurtainPresenter.HideLoading();
            }
            catch (Exception e)
            {
                Debug.LogError($"[GAME BOOTSTRAPPER] init error: {e}");
                Debug.LogException(e);
            }
        }
    }
}