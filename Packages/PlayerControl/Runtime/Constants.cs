using UnityEngine;

namespace PlayerControl
{
    /// <summary>
    /// Constant values
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Animation hash constants.
        /// </summary>
        public static class Hash
        {
            /// <summary>
            /// "Speed" animation hash.
            /// </summary>
            public static readonly int Speed = Animator.StringToHash(nameof(Speed));

            /// <summary>
            /// "IsGround" animation hash.
            /// </summary>
            public static readonly int IsGround = Animator.StringToHash(nameof(IsGround));

            /// <summary>
            /// "JumpStart" animation hash.
            /// </summary>
            public static readonly int JumpStart = Animator.StringToHash(nameof(JumpStart));

            /// <summary>
            /// "DoubleJump" animation hash.
            /// </summary>
            public static readonly int DoubleJump = Animator.StringToHash(nameof(DoubleJump));

            /// <summary>
            /// "Forward" animation hash.
            /// </summary>
            public static readonly int Forward = Animator.StringToHash(nameof(Forward));

            /// <summary>
            /// "SideStep" animation hash.
            /// </summary>
            public static readonly int SideStep = Animator.StringToHash(nameof(SideStep));
        }

        /// <summary>
        /// Action names.
        /// </summary>
        public static class Action
        {
            /// <summary>
            /// "Move" action name.
            /// </summary>
            public const string Move = nameof(Move);

            /// <summary>
            /// "Look" action name.
            /// </summary>
            public const string Look = nameof(Look);

            /// <summary>
            /// "Jump" action name.
            /// </summary>
            public const string Jump = nameof(Jump);

            /// <summary>
            /// "Sprint" action name.
            /// </summary>
            public const string Sprint = nameof(Sprint);
        }
    }
}