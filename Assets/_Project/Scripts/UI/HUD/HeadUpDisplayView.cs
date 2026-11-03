using System;
using _Project.Scripts.UI.Common.HealthBar;
using _Project.Scripts.UI.Windows;
using UnityEngine;

namespace _Project.Scripts.UI.HUD
{
    public class HeadUpDisplayView : MonoBehaviour, IWindow
    {
        public event Action OnWindowDestroy;
        
        [field:SerializeField] public HealthBarView HealthBarView { get; private set; }
        
        public void OnDestroy()
        {
            OnWindowDestroy?.Invoke();
        }
    }
}