using System;
using _Project.Scripts.Services.Audio;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.Windows.LoadingCurtain
{
    public class LoadingCurtainView : MonoBehaviour, IWindow
    {
        public event Action OnWindowDestroy;
        
        [SerializeField] private Animator _loadingBarAnimator;
        
        private IMusicService _audioService;

        [Inject]
        private void Construct(IMusicService audioService) => 
            _audioService = audioService;

        private void Awake() => 
            DontDestroyOnLoad(gameObject);

        private void OnDestroy() => 
            OnWindowDestroy?.Invoke();

        public void Open()
        {
            gameObject.SetActive(true);
            _loadingBarAnimator.enabled = true;
        }

        public void Close()
        {
            gameObject.SetActive(false);
            _loadingBarAnimator.enabled = false;
        }
    }
}