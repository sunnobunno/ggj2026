using Godot;
using InputControllers;
using System;
using PlayerControllers.FirstPerson;

namespace InputControllers
{
    /// <summary>
    /// This singleton script captures mouse movements and invokes corresponding delegates to
    /// be used by other scripts.
    /// <para>
    /// Mouse movements are captured and translated to corresponding Yaw and Pitch signals.
    /// This script also captures the mouse to the center
    /// of the screen via the <c>CaptureFirstPerson()</c> method.
    /// </para>
    /// <para>Available delegates:</para>
    /// <code>
    /// YawAxisSignal
    /// PitchAxisSignal
    /// </code>
    /// </summary>
    ///
    public partial class MouseMovementController : Node3D
    {
        public delegate void CameraGimbalDelegate(Vector3 rotation);
        public static CameraGimbalDelegate YawAxisSignal;
        public static CameraGimbalDelegate PitchAxisSignal;

        [Export] private bool captureMouse = true;


        private bool isInputCaptured = false;

        public static Vector2 MouseDelta { get; private set; }
        public static bool IsFirstPersonMouseCaptured { get; private set; }
        public static bool IsVirtualMouseActive { get; private set; }

        public override void _Ready()
        {
            CaptureFirstPerson();
        }

        public override void _Process(double delta)
        {
            if (captureMouse) CaptureFirstPerson();
            else ReleaseFirstPerson();
            
            MouseDelta = isInputCaptured ? MouseDelta : Vector2.Zero;
            isInputCaptured = false;
            UpdateVirtualMousePosition((float)delta);

            //DebugDraw3D.DrawPoints([VirtualMousePosition]);
        }


        private void UpdateVirtualMousePosition(float delta)
        {
            if (IsFirstPersonMouseCaptured)
            {
                var mouseDelta = MouseDelta;

                var yawDelta = -mouseDelta.X * FirstPersonController.Instance.FirstPersonLookSpeed * delta;
                var pitchDelta = -mouseDelta.Y * FirstPersonController.Instance.FirstPersonLookSpeed * delta;

                var yawAxisDelta = new Vector3(0f, yawDelta, 0f);
                var pitchAxisDelta = new Vector3(pitchDelta, 0f, 0f);

                //DU.Log([yawAxisDelta, mouseDelta]);

                YawAxisSignal?.Invoke(yawAxisDelta);
                PitchAxisSignal?.Invoke(pitchAxisDelta);
            }
        }

        public override void _Input(InputEvent @event)
        {
            isInputCaptured = true;


            if (@event is InputEventMouseMotion)
            {
                var mouseDelta = Vector2.Zero;
                var mouseMotion = @event as InputEventMouseMotion;
                mouseDelta = mouseMotion.ScreenRelative;
                MouseDelta = mouseDelta;
                //DU.Log(MouseDelta);
            }
        }


        public static void CaptureFirstPerson()
        {
            IsFirstPersonMouseCaptured = true;
            IsVirtualMouseActive = true;
            Input.MouseMode = Input.MouseModeEnum.Captured;
        }

        public static void ReleaseFirstPerson()
        {
            IsFirstPersonMouseCaptured = false;
            Input.MouseMode = Input.MouseModeEnum.Visible;
        }
    }
}

