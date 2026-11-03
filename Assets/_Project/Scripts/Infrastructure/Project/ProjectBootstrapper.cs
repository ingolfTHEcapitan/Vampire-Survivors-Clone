using System;
using _Project.Scripts.Infrastructure.AssetManagement;
using _Project.Scripts.Services.SceneLoader;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Windows.LoadingCurtain;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.Project
{
    public class ProjectBootstrapper : IInitializable
    {
        private readonly IAssetProvider _assetProvider;
        private readonly SceneLoaderService _sceneLoader;
        private readonly WindowFactory _windowFactory;
        private readonly LazyInject<LoadingCurtainPresenter> _lazyLoadingWindowPresenter;


        public ProjectBootstrapper(LazyInject<LoadingCurtainPresenter> lazyLoadingWindowPresenter, 
            IAssetProvider assetProvider, WindowFactory windowFactory, SceneLoaderService sceneLoader)
        {
            _lazyLoadingWindowPresenter = lazyLoadingWindowPresenter;
            _assetProvider = assetProvider;
            _windowFactory = windowFactory;
            _sceneLoader = sceneLoader;
        }

        public void Initialize() => 
            InitializeAsync().Forget();

        private async UniTaskVoid InitializeAsync()
        {
            try
            {
                await _assetProvider.InitializeAsync();
                await _windowFactory.CreateAsync<LoadingCurtainView>();
                _lazyLoadingWindowPresenter.Value.ShowLoading();
            
                await _sceneLoader.LoadAsync(buildIndex: (int)SceneName.MainMenu);
            }
            catch (Exception e)
            {
                Debug.LogError($"[PROJECT BOOTSTRAPPER] init error: {e}");
                Debug.LogException(e);
            }
        }
    }
}