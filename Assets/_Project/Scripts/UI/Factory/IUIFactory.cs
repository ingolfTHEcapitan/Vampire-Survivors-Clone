using _Project.Scripts.UI.HUD;
using _Project.Scripts.UI.Windows;
using _Project.Scripts.UI.Windows.GameOver;
using _Project.Scripts.UI.Windows.LoadingCurtain;
using _Project.Scripts.UI.Windows.MainMenu;
using _Project.Scripts.UI.Windows.Settings;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.UI.Factory
{
    public interface IUIFactory
    {
        UniTask<HeadUpDisplayView> CreateHudViewAsync(Transform uiParent);
        UniTask<GameOverWindowView> CreateGameOverWindowViewAsync(Transform uiParent);
        UniTask<LoadingCurtainView> CreateLoadingCurtainViewAsync();
        UniTask<MainMenuWindowView> CreateMainMenuWindowViewAsync(Transform uiParent);
        UniTask<SettingsWindowView> CreateSettingsViewAsync(Transform uiParent);
        UniTask<Sprite> LoadSpriteAsync(string assetAddress);
        TWindow GetWindowView<TWindow>() where TWindow : Component, IWindow;
    }
}