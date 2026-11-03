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
        
        [Header("Components")]
        [SerializeField] private Health _health;

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
            _health.OnTakeDamage += PlayDamageSound;
            _health.OnTakeHeal += PlayHealSound;
        }

        private void OnDestroy()
        {
            _health.OnZeroHealth -= PlayDeathSound;
            _health.OnTakeDamage -= PlayDamageSound;
            _health.OnTakeHeal -= PlayHealSound;
        }
        
        private void PlayDeathSound() => 
            _audioService.PlayDelayed(_deathSound, _playerAudioSource, DeathSoundDelay);

        private void PlayDamageSound() => 
            _audioService.PlayOneShotRandom(_takeDamageSounds, _playerAudioSource);

        private void PlayHealSound() => 
            _audioService.PlayOneShot(_healSound, _playerAudioSource);
    }
}