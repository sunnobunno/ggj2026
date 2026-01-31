using Godot;
using System;

namespace InputControllers
{
    /// <summary>
    /// This singleton script receives inputs from the game engine triggered by the keyboard
    /// and invokes a number of delegates to be used by other scripts.
    /// <para>Available delegates:</para>
    /// <code>
    /// ForwardPressed ()
    /// BackwardPressed ()
    /// LeftPressed ()
    /// RightPressed ()
    /// EquipLeftHandPressed ()
    /// JumpPressed ()
    /// </code>
    /// </summary>
    public partial class KeyboardManager : Node
    {

        public delegate void KeyboardEvent();
        public static KeyboardEvent ForwardPressed;
        public static KeyboardEvent BackwardPressed;
        public static KeyboardEvent LeftPressed;
        public static KeyboardEvent RightPressed;

        public static KeyboardEvent JumpPressed;
        public static KeyboardEvent CrouchPressed;

        public override void _Process(double delta)
        {
            if (Input.IsActionPressed("Forward"))
                ForwardPressed?.Invoke();
            if (Input.IsActionPressed("Backward"))
                BackwardPressed?.Invoke();
            if (Input.IsActionPressed("Left"))
                LeftPressed?.Invoke();
            if (Input.IsActionPressed("Right"))
                RightPressed?.Invoke();


            if (Input.IsActionJustPressed("Jump"))
                JumpPressed?.Invoke();
            if (Input.IsActionJustPressed("Crouch"))
                CrouchPressed?.Invoke();
        }

        public override void _Input(InputEvent @event)
        {

        }
    }
}

