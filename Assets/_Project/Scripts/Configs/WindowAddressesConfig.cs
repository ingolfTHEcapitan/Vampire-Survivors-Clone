using System;
using System.Collections.Generic;
using _Project.Scripts.Infrastructure.AssetManagement;
using _Project.Scripts.UI.HUD;
using _Project.Scripts.UI.Windows;
using _Project.Scripts.UI.Windows.GameOver;
using _Project.Scripts.UI.Windows.LoadingCurtain;
using _Project.Scripts.UI.Windows.MainMenu;
using _Project.Scripts.UI.Windows.Settings;
using UnityEngine;

namespace _Project.Scripts.Configs
{
    public class WindowAddressesConfig
    {
        private readonly Dictionary<Type, string> _windowAddresses = new Dictionary<Type, string>()
        {
            {typeof(HeadUpDisplayView), AssetAddress.HeadUpDisplay},
            {typeof(GameOverWindowView), AssetAddress.GameOverWindow},
            {typeof(LoadingCurtainView), AssetAddress.LoadingCurtain},
            {typeof(MainMenuWindowView), AssetAddress.MainMenuWindow},
            {typeof(SettingsWindowView), AssetAddress.SettingsWindow},
        };

        public string GetAssetAddress<TWindow>() where TWindow : Component, IWindow
        {
            Type viewType = typeof(TWindow);
            
            if (_windowAddresses.TryGetValue(viewType, out string assetAddress))
                return assetAddress;
                
            throw new KeyNotFoundException("[WindowAddressesConfig] No asset address registered for window : " + viewType.Name);
        }
    }
}