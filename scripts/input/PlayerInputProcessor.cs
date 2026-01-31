using Godot;
using System;
using InputControllers;
using Clickables;

namespace InputControllers.firstPerson
{
    /// <summary>
    /// This script recieves input from <c>KeyboardManager</c> and translates it
    /// to local vector3 directions for use by a player controller. It also collects
    /// wheel up and down signals from the ClickablesBridge
    /// <para>Available delegates:</para>
    /// <code>
    /// DirectionSignal (Vector3 direciton)
    /// MouseWheelSignal (float x)
    /// </code>
    /// </summary>
    public partial class PlayerInputProcessor : Node
    {
        public delegate void DirectionInput(Vector3 direction);
        public static DirectionInput DirectionSignal;

        public delegate void MouseWheelInput(float x);
        public static MouseWheelInput MouseWheelSignal;

        private Vector3 localDirection;

        public static Vector3 LocalDirection { get; private set; }

        public override void _Ready()
        {
            localDirection = Vector3.Zero;

            KeyboardManager.ForwardPressed += OnForwardPressed;
            KeyboardManager.BackwardPressed += OnBackwardPressed;
            KeyboardManager.LeftPressed += OnLeftPressed;
            KeyboardManager.RightPressed += OnRightPressed;

            ClickablesBridge.OnWheelUp += OnMouseWheelUp;
            ClickablesBridge.OnWheelDown += OnMouseWheelDown;
        }

        public override void _Process(double delta)
        {
            localDirection = localDirection.Normalized();
            DirectionSignal?.Invoke(localDirection);
            LocalDirection = localDirection;
            
            localDirection = Vector3.Zero;
        }

        private void OnForwardPressed()
        {
            localDirection += Vector3.Forward;
        }

        private void OnBackwardPressed()
        {
            localDirection += Vector3.Back;
        }

        private void OnLeftPressed()
        {
            localDirection += Vector3.Left;
        }

        private void OnRightPressed()
        {
            localDirection += Vector3.Right;
        }


        private void OnMouseWheelUp()
        {
            MouseWheelSignal?.Invoke(1f);
        }

        private void OnMouseWheelDown()
        {
            MouseWheelSignal?.Invoke(-1f);
        }
    }
}

