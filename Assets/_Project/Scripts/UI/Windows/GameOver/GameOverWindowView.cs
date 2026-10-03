using System;
using _Project.Scripts.UI.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.UI.Windows.GameOver
{
    public class GameOverWindowView : MonoBehaviour, IWindow
    {
        public event Action OnWindowDestroy;
        
        [SerializeField] private WindowPopupAnimation _windowAnimation;
        [Space]
        [SerializeField] private GameObject _windowContent;

        public void Initialize()
        {
            _windowContent.SetActive(false);
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
    }
}