using _Project.Scripts.Services.GamePause;
using _Project.Scripts.Services.PlayerInput;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Logic.Player.Components
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [Header("Settings")]
        [SerializeField] private float _speed = 5f;

        private IGamePauseService _pauseService;
        private IInputService _inputService;
        private Vector2 _movementDirection;
        private Vector2 _movementInput;

        [Inject]
        private void Construct(IGamePauseService pauseService, IInputService inputService)
        {
            _pauseService = pauseService;
            _inputService = inputService;
        }

        private void Start()
        {
            _pauseService.OnPausableUpdate += PausableUpdate;
            _pauseService.OnPausableFixedUpdate += PausableFixedUpdate;
        }
        
        private void OnDestroy()
        {
            _pauseService.OnPausableUpdate -= PausableUpdate;
            _pauseService.OnPausableFixedUpdate -= PausableFixedUpdate;
        }

        private void PausableUpdate()
        {
            Debug.Log("Raw input " + _inputService.GetMovementAxis());
            Debug.Log("Norm input " + _inputService.GetMovementAxis().normalized);
            _movementInput = _inputService.GetMovementAxis();
        }

        private void PausableFixedUpdate() => 
            Move(_movementInput);

        private void Move(Vector2 axis)
        {
            _movementDirection = new Vector2(axis.x * _speed, axis.y * _speed);
            _rigidbody2D.MovePosition(_rigidbody2D.position +  _movementDirection * Time.fixedDeltaTime);
        }
    }
}