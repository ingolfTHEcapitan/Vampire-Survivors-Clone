using System;
using _Project.Scripts.Logic.Common;
using UnityEngine;

namespace _Project.Scripts.Logic.Player.Components
{
    public class PlayerDeath : MonoBehaviour
    {
        public event Action OnDied;

        [SerializeField] private Health _health;
        [SerializeField] private PlayerMovement _playerMovement;

        public bool IsDead { get; private set; }
        
        public void Initialize() => 
            _health.OnZeroHealth += PlayerDie;

        private void OnDestroy() => 
            _health.OnZeroHealth -= PlayerDie;
        
        private void PlayerDie()
        {
            if (!IsDead) 
                Die();
        }

        private void Die()
        {
            IsDead = true;
            EnablePlayerComponents(false);
            OnDied?.Invoke();
        }
        
        private void EnablePlayerComponents(bool enable)
        {
            _playerMovement.enabled = enable;
        }
    }
}