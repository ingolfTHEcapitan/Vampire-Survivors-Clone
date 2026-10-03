using System;
using _Project.Scripts.Services.GamePause;
using _Project.Scripts.Services.SceneLoader;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.UI.Windows.GameOver
{
    public class GameOverWindowModel: IDisposable
    {
        private readonly IGamePauseService _pauseService;
        private readonly ISceneLoaderService _sceneLoader;
        
        public GameOverWindowModel(IGamePauseService pauseService, ISceneLoaderService sceneLoader)
        {
            _pauseService = pauseService;
            _sceneLoader = sceneLoader;
        }
        
        public void Initialize()
        {
           
        }

        public void Dispose()
        {
           
        }
        
        public void SetPaused(bool paused) => 
            _pauseService.SetPaused(paused);

        public async UniTask ReloadSceneAsync()
        {
            await _sceneLoader.ReloadAsync();
        }
    }
}