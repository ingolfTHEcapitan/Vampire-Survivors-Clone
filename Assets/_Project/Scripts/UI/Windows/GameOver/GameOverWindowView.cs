using System;
using _Project.Scripts.UI.Common.WindowAnimations;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Windows.GameOver
{
    public class GameOverWindowView : MonoBehaviour, IWindow
    {
        public event Action OnWindowDestroy;
        public event Action OnExitButtonClicked;
        
        [SerializeField] private WindowAnimationBase _windowAnimation;
        [Space]
        [SerializeField] private GameObject _windowContent;
        [SerializeField] private Button _exitButton;

        public void Initialize()
        {
            _windowContent.SetActive(false);
            _exitButton.onClick.AddListener(InvokeOnExitButtonClicked);
        }
        
        private void OnDestroy()
        {
            OnWindowDestroy?.Invoke();
            _exitButton.onClick.RemoveListener(InvokeOnExitButtonClicked);
        }
        
        public async UniTask OpenAsync()
        {
            _windowContent.SetActive(true);
            await _windowAnimation.AnimateOpenAsync();
        }
        
        public async UniTask CloseAsync()
        {
            await _windowAnimation.AnimateCloseAsync();
            _windowContent.SetActive(false);
        }
        
        private void InvokeOnExitButtonClicked() => 
            OnExitButtonClicked?.Invoke();
    }
}