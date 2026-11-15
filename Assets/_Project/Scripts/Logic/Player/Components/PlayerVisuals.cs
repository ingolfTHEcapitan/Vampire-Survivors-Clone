using System;
using System.Collections.Generic;
using _Project.Scripts.Logic.Common;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Audio;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Logic.Player.Components
{
    public class PlayerVisuals : MonoBehaviour
    {
        private const float DeathSoundDelay = 0.2f;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int TakeHit = Animator.StringToHash("TakeHit");
        private static readonly int Died = Animator.StringToHash("Died");
        
        [Header("Components")]
        [SerializeField] private Animator _animator;
        [SerializeField] private Health _health;
        [SerializeField] private PlayerMovement _movement;

        [Header("Audio")]
        [SerializeField] private AudioSource _playerAudioSource;

        [SerializeField] private AudioClip _deathSound;
        [SerializeField] private AudioClip _healSound;
        [SerializeField] private List<AudioClip> _takeDamageSounds;
        
        private ISoundService _audioService;
        private EffectsService _effectsService;

        [Inject]
        private void Construct(ISoundService audioService, EffectsService effectsService)
        {
            _audioService = audioService;
            _effectsService = effectsService;
        }
        
        public void Initialize()
        {
            _health.OnZeroHealth += PlayDeathSound;
            _health.OnZeroHealth += PlayDeathAnimation;
            _health.OnTakeDamage += PlayDamageSound;
            _health.OnTakeDamage += PlayDamageAnimation;
            _health.OnTakeHeal += PlayHealSound;
        }

        private void OnDestroy()
        {
            _health.OnZeroHealth -= PlayDeathSound;
            _health.OnZeroHealth -= PlayDeathAnimation;
            _health.OnTakeDamage -= PlayDamageSound;
            _health.OnTakeDamage -= PlayDamageAnimation;
            _health.OnTakeHeal -= PlayHealSound;
        }
        
        private void Update() => 
            _animator.SetFloat(Speed, _movement.Velocity.magnitude);

        private void PlayDamageAnimation() => 
            _animator.SetTrigger(TakeHit);

        private void PlayDeathAnimation() => 
            _animator.SetTrigger(Died);

        private void PlayDeathSound() => 
            _audioService.PlayDelayed(_deathSound, _playerAudioSource, DeathSoundDelay);

        private void PlayDamageSound() => 
            _audioService.PlayOneShotRandom(_takeDamageSounds, _playerAudioSource);

        private void PlayHealSound() => 
            _audioService.PlayOneShot(_healSound, _playerAudioSource);
    }
}