using System;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Audio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI.Windows.MainMenu
{
    public class MainMenuWindowView : MonoBehaviour, IWindow
    {
        public event Action OnWindowDestroy;
        public event Action OnPlayButtonClicked;
        public event Action OnOnlineButtonClicked;
        public event Action OnSettingsButtonClicked;
        public event Action OnExitButtonClicked;
        
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _onlineButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;
        
        [Header("Audio")]
        [SerializeField] private AudioClip _backgroundMusic;
        
        private IMusicService _audioService;
        
        [Inject] 
        private void Construct(IMusicService audioService) => 
            _audioService = audioService;
        
        public void Initialize()
        {
            _playButton.onClick.AddListener(InvokeOnPlayButtonClicked);
            _onlineButton.onClick.AddListener(InvokeOnOnlineButtonClicked);
            _settingsButton.onClick.AddListener(InvokeOnSettingsButtonClicked);
            _exitButton.onClick.AddListener(InvokeOnExitButtonClicked);
        }
        
        private void OnDestroy()
        {
            OnWindowDestroy?.Invoke();
            _playButton.onClick.RemoveListener(InvokeOnPlayButtonClicked);
            _onlineButton.onClick.RemoveListener(InvokeOnOnlineButtonClicked);
            _settingsButton.onClick.RemoveListener(InvokeOnSettingsButtonClicked);
            _exitButton.onClick.RemoveListener(InvokeOnExitButtonClicked);
        }
        
        public void PlayBackgroundMusic() => 
            _audioService.PlayMusic(_backgroundMusic);

        private void InvokeOnPlayButtonClicked() => 
            OnPlayButtonClicked?.Invoke();

        private void InvokeOnSettingsButtonClicked() => 
            OnSettingsButtonClicked?.Invoke();
        
        private void InvokeOnExitButtonClicked() => 
            OnExitButtonClicked?.Invoke();
        
        private void InvokeOnOnlineButtonClicked() => 
            OnOnlineButtonClicked?.Invoke();
    }
}