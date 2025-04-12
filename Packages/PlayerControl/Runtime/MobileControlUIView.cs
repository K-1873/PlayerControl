using UnityEngine;
using UnityEngine.UI;

namespace PlayerControl
{
    /// <summary>
    /// Mobile control UI view class.
    /// </summary>
    public class MobileControlUIView : MonoBehaviour
    {
        [SerializeField]
        private MinimumVirtualJoyStick _joystick = null;

        [SerializeField]
        private MinimumHoldButton _sprintButton = null;

        [SerializeField]
        private Button _jumpButton = null;

        /// <summary>
        /// Gets the joystick.
        /// </summary>
        public ref readonly MinimumVirtualJoyStick Joystick => ref _joystick;

        /// <summary>
        /// Gets the sprint button.
        /// </summary>
        public ref readonly MinimumHoldButton SprintButton => ref _sprintButton;

        /// <summary>
        /// Gets the jump button.
        /// </summary>
        public ref readonly Button JumpButton => ref _jumpButton;
    }
}