using Godot;
using System;

namespace InputControllers
{
    /// <summary>
    /// <para>DEPRECIATED. Use <c>ClickHandler</c> instead.</para>
    /// This singleton script recieves inputs from mouse buttons and the mouse wheel and
    /// invokes a number of delegates to be used by other scripts.
    /// <para>Available delegates:</para>
    /// <code>
    /// ClickedSignal ()
    /// ReleasedSignal ()
    /// RightClickedSignal ()
    /// RightReleasedSignal ()
    /// WheelUpSignal ()
    /// WheelDownSignal ()
    /// </code>
    /// </summary>
    public partial class MouseButtonController : Node
    {
        public delegate void MouseSignal();
        public static MouseSignal ClickedSignal;
        public static MouseSignal ReleasedSignal;
        public static MouseSignal RightClickedSignal;
        public static MouseSignal RightReleasedSignal;

        public static MouseSignal WheelUpSignal;
        public static MouseSignal WheelDownSignal;





        private bool isInputCaptured = false;


        public static Vector2 MouseScreenPosition { get; private set; }


        public override void _Process(double delta)
        {

            ClickSignals();
        }

        private void ClickSignals()
        {
            if (Input.IsActionJustPressed("Click"))
            {
                ClickedSignal?.Invoke();
            }

            if (Input.IsActionJustReleased("Click"))
            {
                ReleasedSignal?.Invoke();
            }

            if (Input.IsActionJustPressed("RightClick"))
            {
                RightClickedSignal?.Invoke();
            }

            if (Input.IsActionJustReleased("RightClick"))
            {
                RightReleasedSignal?.Invoke();
            }
        }

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventMouseButton)
            {
                InputEventMouseButton emb = (InputEventMouseButton)@event;
                if (emb.ButtonIndex == MouseButton.WheelUp)
                {
                    WheelUpSignal?.Invoke();
                }
                else if (emb.ButtonIndex == MouseButton.WheelDown)
                {
                    WheelDownSignal?.Invoke();
                }
            }
        }
    }
}