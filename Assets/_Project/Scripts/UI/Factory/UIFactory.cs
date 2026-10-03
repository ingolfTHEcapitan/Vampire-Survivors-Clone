using System;
using System.Collections.Generic;
using System.Data;
using _Project.Scripts.Infrastructure.AssetManagement;
using _Project.Scripts.UI.HUD;
using _Project.Scripts.UI.Windows;
using _Project.Scripts.UI.Windows.GameOver;
using _Project.Scripts.UI.Windows.LoadingCurtain;
using _Project.Scripts.UI.Windows.MainMenu;
using _Project.Scripts.UI.Windows.Settings;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IInstantiator _container;
        private readonly IAssetProvider _assetProvider;
        
        private readonly Dictionary<Type, IWindow> _windowViews = new Dictionary<Type, IWindow>();
        
        public UIFactory(IInstantiator container, IAssetProvider assetProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
        }
        
        public async UniTask<HeadUpDisplayView> CreateHudViewAsync(Transform uiParent) => 
            await CreateWindowViewAsync<HeadUpDisplayView>(AssetAddress.HeadUpDisplay, uiParent);

        public async UniTask<GameOverWindowView> CreateGameOverWindowViewAsync(Transform uiParent) => 
            await CreateWindowViewAsync<GameOverWindowView>(AssetAddress.GameOverWindow, uiParent);

        public async UniTask<LoadingCurtainView> CreateLoadingCurtainViewAsync() => 
            await CreateWindowViewAsync<LoadingCurtainView>(AssetAddress.LoadingCurtain, isGlobal: true);

        public async UniTask<MainMenuWindowView> CreateMainMenuWindowViewAsync(Transform uiParent) => 
            await CreateWindowViewAsync<MainMenuWindowView>(AssetAddress.MainMenuWindow, uiParent);
        
        public async UniTask<SettingsWindowView> CreateSettingsViewAsync(Transform uiParent) => 
            await CreateWindowViewAsync<SettingsWindowView>(AssetAddress.SettingsWindow, uiParent);
        
        public async UniTask<Sprite> LoadSpriteAsync(string assetAddress) => 
            await _assetProvider.LoadAsync<Sprite>(assetAddress);
        
        public TWindow GetWindowView<TWindow>() where TWindow : Component, IWindow
        {
            Type viewType = typeof(TWindow);
            
            if (_windowViews.TryGetValue(viewType, out IWindow windowView))
                return (TWindow)windowView;
            
            throw new InvalidConstraintException(
                $"{viewType.Name} requested before creation. Ensure it created before presenter resolution");
        }
        
        private async UniTask<TWindow> CreateWindowViewAsync<TWindow>(string assetAddress, Transform uiParent = null, bool isGlobal = false) where TWindow : Component, IWindow
        {
            Type viewType = typeof(TWindow);
            
            if (_windowViews.TryGetValue(viewType, out IWindow windowView))
                return (TWindow)windowView;
            
            TWindow view = await CreateViewAsync<TWindow>(assetAddress, uiParent, isGlobal);
            
            _windowViews[viewType] = view;
            view.OnWindowDestroy += DestroyView;

            return view;

            void DestroyView()
            {
                view.OnWindowDestroy -= DestroyView;
                _windowViews.Remove(viewType);
            }
        }
        
        private async UniTask<TView> CreateViewAsync<TView>(string assetAddress, Transform uiParent = null, bool isGlobal = false) where TView : Component
        {
            GameObject prefab = await _assetProvider.LoadAsync<GameObject>(assetAddress, isGlobal);
            TView view = _container.InstantiatePrefabForComponent<TView>(prefab, uiParent);
            return view;
        }
    }
}