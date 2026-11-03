using _Project.Scripts.Tools;
using _Project.Scripts.UI.Windows.MainMenu;
using _Project.Scripts.UI.Windows.Online;
using _Project.Scripts.UI.Windows.Settings;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.MainMenu
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private Transform _uiParent;
        
        public override void InstallBindings()
        {
            BindSettingsWindow();
            BindOnlineWindow();
            BindMainMenuWindow();
            BindMainMenuBootstrapper();
        }
        
        private void BindSettingsWindow()
        {
            Container.BindWindow<SettingsWindowView>();
            Container.BindInterfacesAndSelfTo<SettingsWindowModel>().AsSingle();
            Container.Bind<SettingsWindowPresenter>().AsSingle();
        }

        private void BindOnlineWindow()
        {
            Container.BindWindow<OnlineWindowView>();
            Container.Bind<OnlineWindowModel>().AsSingle();
            Container.Bind<OnlineWindowPresenter>().AsSingle();
        }

        private void BindMainMenuWindow()
        {
            Container.BindWindow<MainMenuWindowView>();
            Container.BindInterfacesAndSelfTo<MainMenuWindowModel>().AsSingle();
            Container.Bind<MainMenuWindowPresenter>().AsSingle();
        }

        private void BindMainMenuBootstrapper() => 
            Container.BindInterfacesAndSelfTo<MainMenuBootstrapper>().AsSingle().WithArguments(_uiParent);
        
    }
}