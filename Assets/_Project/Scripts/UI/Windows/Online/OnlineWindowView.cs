using System;
using _Project.Scripts.UI.Common.WindowAnimations;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Windows.Online
{
    public class OnlineWindowView : MonoBehaviour, IWindow
    {
        public event Action OnWindowDestroy;
        public event Action OnJoinButtonClicked;
        public event Action OnCreateButtonOnClicked;
        public event Action OnBackButtonClicked;
        
        [SerializeField] private WindowAnimationBase _windowAnimation;
        [Space]
        [SerializeField] private GameObject _windowContent;
        [SerializeField] private TMP_InputField _idInputField;
        [SerializeField] private Button _joinButton;
        [SerializeField] private Button _createButton;
        [SerializeField] private Button _backButton;
        
        public void Initialize()
        {
            _windowContent.SetActive(false);
            _joinButton.onClick.AddListener(InvokeOnJoinButtonClicked);
            _createButton.onClick.AddListener(InvokeOnCreateButtonOnClicked);
            _backButton.onClick.AddListener(InvokeOnBackButtonClicked);
        }

        private void OnDestroy()
        {
            OnWindowDestroy?.Invoke();
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

        private void InvokeOnJoinButtonClicked() => 
            OnJoinButtonClicked?.Invoke();

        private void InvokeOnCreateButtonOnClicked() => 
            OnCreateButtonOnClicked?.Invoke();

        private void InvokeOnBackButtonClicked() => 
            OnBackButtonClicked?.Invoke();
    }
}