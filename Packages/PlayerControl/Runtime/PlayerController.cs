using System;
using System.Runtime.CompilerServices;
using Unity.TinyCharacterController.Check;
using Unity.TinyCharacterController.Control;
using Unity.TinyCharacterController.Interfaces.Components;
using Unity.TinyCharacterController.Interfaces.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControl
{
    /// <summary>
    /// Player controller.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(ITransform))]
    [RequireComponent(typeof(IWarp))]
    [RequireComponent(typeof(MoveControl))]
    [RequireComponent(typeof(JumpControl))]
    [RequireComponent(typeof(GroundCheck))]
    [RequireComponent(typeof(TpsCameraControl))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private PlayerInput _playerInput;
        [SerializeField]
        private MoveControl _moveControl;
        [SerializeField]
        private JumpControl _jumpControl;
        [SerializeField]
        private GroundCheck _groundCheck;
        [SerializeField]
        private TpsCameraControl _cameraControl;
        private ITransform _transform;
        private IWarp _warp;

        /// <summary>
        /// <see cref="Animator"/> component of the player.
        /// </summary>
        public Animator Animator => _animator;

        /// <summary>
        /// <see cref="PlayerInput"/> component of the player.
        /// </summary>
        public PlayerInput PlayerInput => _playerInput;

        /// <summary>
        /// Whether the player can perform a double jump.
        /// </summary>
        public bool CanDoubleJump { get; set; } = true;

        /// <summary>
        /// Whether the player is performing a double jump.
        /// </summary>
        public bool IsDoubleJump
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => CanDoubleJump && _jumpControl.AerialJumpCount >= 1;
        }

        /// <summary>
        /// The current speed of the player.
        /// </summary>
        public float CurrentSpeed
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _moveControl.CurrentSpeed;
        }

        /// <summary>
        /// Whether the player is on the ground.
        /// </summary>
        public bool IsOnGround
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _groundCheck.IsOnGround;
        }

        /// <summary>
        /// The local direction of the player.
        /// </summary>
        public Vector3 LocalDirection
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _moveControl.LocalDirection;
        }

        /// <summary>
        /// The world position of the player.
        /// </summary>
        public Vector3 WorldPosition
        {
            get => (_transform ??= GetComponent<ITransform>()).Position;
            set => (_warp ??= GetComponent<IWarp>()).Warp(value);
        }

        /// <summary>
        /// The world rotation of the player.
        /// </summary>
        public Quaternion WorldRotation
        {
            get => (_transform ??= GetComponent<ITransform>()).Rotation;
            set => (_warp ??= GetComponent<IWarp>()).Warp(value);
        }

        /// <summary>
        /// Whether the player can move.
        /// <remarks>
        /// Jumping and looking around are still allowed.
        /// </remarks>
        /// </summary>
        public bool UnlockMove { get; set; } = true;

        private void Start()
        {
            _playerInput.onActionTriggered += OnActionTriggered;
            _jumpControl.OnJump.AddListener(OnJump);
        }

        private void Update()
        {
            Animator.SetFloat(Constants.Hash.Speed, CurrentSpeed);
            Animator.SetBool(Constants.Hash.IsGround, IsOnGround);

            var currentDirection = LocalDirection;
            var deltaTime = Time.deltaTime;
            const float dampTime = 0.1f;
            Animator.SetFloat(Constants.Hash.Forward, currentDirection.z, dampTime, deltaTime);
            Animator.SetFloat(Constants.Hash.SideStep, currentDirection.x, dampTime, deltaTime);
        }

        private void OnActionTriggered(InputAction.CallbackContext context)
        {
            Debug.Log(context.action.name);
            switch (context.action.name)
            {
                case Constants.Action.Move when context.phase is InputActionPhase.Performed or InputActionPhase.Canceled && UnlockMove:
                    _moveControl.Move(context.ReadValue<Vector2>());
                    break;
                case Constants.Action.Look when context.phase is InputActionPhase.Performed:
                    _cameraControl.RotateCamera(context.ReadValue<Vector2>());
                    break;
                case Constants.Action.Jump when context.phase is InputActionPhase.Started:
                    _jumpControl.Jump();
                    break;
                case Constants.Action.Sprint when context.phase is InputActionPhase.Performed:
                    _moveControl.MoveSpeed = 4.0f;
                    break;
                case Constants.Action.Sprint when context.phase is InputActionPhase.Canceled:
                    _moveControl.MoveSpeed = 1.2f;
                    break;
            }
        }

        private void OnJump() => Animator.Play(IsDoubleJump ? Constants.Hash.DoubleJump : Constants.Hash.JumpStart);
    }
}