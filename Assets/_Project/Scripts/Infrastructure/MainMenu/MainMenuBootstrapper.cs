using System;
using _Project.Scripts.Services;
using _Project.Scripts.Services.SaveLoad;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Windows.LoadingCurtain;
using _Project.Scripts.UI.Windows.MainMenu;
using _Project.Scripts.UI.Windows.Settings;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.MainMenu
{
    public class MainMenuBootstrapper : IInitializable
    {
        private readonly LoadingCurtainPresenter _loadingCurtainPresenter;
        private readonly ProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly WindowFactory _windowFactory;

        private readonly Transform _uiParent;
        private readonly LazyInject<SettingsWindowPresenter> _lazySettingsWindowPresenter;
        private readonly LazyInject<MainMenuWindowPresenter> _lazyMainMenuWindowPresenter;

        public MainMenuBootstrapper(LoadingCurtainPresenter loadingCurtainPresenter, ProgressService progressService,
            [Inject(Id = SaveType.Local)]ISaveLoadService saveLoadService, WindowFactory windowFactory, Transform uiParent,
            LazyInject<SettingsWindowPresenter> lazySettingsWindowPresenter, LazyInject<MainMenuWindowPresenter> lazyMainMenuWindowPresenter)
        {
            _lazySettingsWindowPresenter = lazySettingsWindowPresenter;
            _lazyMainMenuWindowPresenter = lazyMainMenuWindowPresenter;
            _loadingCurtainPresenter = loadingCurtainPresenter;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _windowFactory = windowFactory;
            _uiParent = uiParent;
        }

        public void Initialize() => 
            InitializeAsync().Forget();

        private async UniTaskVoid InitializeAsync()
        {
            try
            {
                _progressService.PlayerProgress = await _saveLoadService.LoadProgressAsync();
                
                await _windowFactory.CreateAsync<SettingsWindowView>(_uiParent);
                _lazySettingsWindowPresenter.Value.Initialize();
            
                await _windowFactory.CreateAsync<MainMenuWindowView>(_uiParent); 
                _lazyMainMenuWindowPresenter.Value.Initialize();
            
                _loadingCurtainPresenter.HideLoading();
            }
            catch (Exception e)
            {
                Debug.LogError($"[MAIN MENU BOOTSTRAPPER] init error: {e}");
                Debug.LogException(e);
            }
        }
    }
}