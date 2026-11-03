using _Project.Scripts.Configs;
using _Project.Scripts.Configs.Spawners;
using _Project.Scripts.Infrastructure.AssetManagement;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Audio;
using _Project.Scripts.Services.PlayerInput;
using _Project.Scripts.Services.SaveLoad;
using _Project.Scripts.Services.SaveLoad.LocalSave;
using _Project.Scripts.Services.SceneLoader;
using _Project.Scripts.Tools;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Common;
using _Project.Scripts.UI.Windows.LoadingCurtain;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Infrastructure.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        [Header("Audio")] 
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private PlayerSpawnerConfig _playerSpawnerConfig;
        [FormerlySerializedAs("_localSaveServiceConfig")] [SerializeField] private LocalSaveTypeConfig localSaveTypeConfig;
        
        public override void InstallBindings()
        {
            BindConfigs();
            BindServices();
            BindLoadingCurtain();
            BindProjectBootstrapper();
        }

        private void BindConfigs()
        {
            Container.Bind<WindowAddressesConfig>().AsSingle();
            Container.Bind<PlayerSpawnerConfig>().FromInstance(_playerSpawnerConfig).AsSingle();
            Container.Bind<LocalSaveTypeConfig>().FromInstance(localSaveTypeConfig).AsSingle();
        }
        
        private void BindServices()
        {
            Container.Bind<CursorController>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoaderService>().AsSingle();
            Container.BindInterfacesAndSelfTo<NetworkAccessibilityService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProgressService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<DesktopInputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<WindowsRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<WindowFactory>().AsSingle();

            Container.Bind<LocalSaveTypeProvider>().AsSingle();
            Container.Bind<ISaveLoadService>().FromMethod(GetLocalSaveInstance).AsCached();
            
            Container.BindInterfacesAndSelfTo<AudioService>().AsSingle().WithArguments(_musicAudioSource);
        }

        private void BindLoadingCurtain()
        {
            Container.BindWindow<LoadingCurtainView>();
            Container.Bind<LoadingCurtainPresenter>().AsSingle();
        }

        private void BindProjectBootstrapper() => 
            Container.BindInterfacesAndSelfTo<ProjectBootstrapper>().AsSingle();

        private ISaveLoadService GetLocalSaveInstance(InjectContext context) => 
            context.Container.Resolve<LocalSaveTypeProvider>().GetInstance();
    }
}