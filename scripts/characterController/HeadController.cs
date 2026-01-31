using Godot;
using System;
using InputControllers;

namespace PlayerControllers.FirstPerson
{
    
    /// <summary>
    /// This singleton script captures yaw and pitch movements from the
    /// <c>MouseMovementController</c> and applies rotation to yaw and pitch
    /// gimbal children to this node.
    /// <para>
    /// This script also calculates an eased roll rotation to be used to simulate the head
    /// rolling left and right when moving side to side or rotating the yaw gimbal.
    /// </para>
    /// </summary>
    public partial class HeadController : Node3D
    {
        public static HeadController Instance;

        public delegate void YawDelegate(Vector3 delta);
        public static YawDelegate OnYawDeltaRadians;

        [Export] private Node3D yawGimbal;
        [Export] private Node3D pitchGimbal;
        [Export] private Node3D rollGimbal;
        [Export] private float maxRoll = 0.1f;
        [Export] private float rollSpringStr = 1f;
        [Export] private float rollSpringDamp = 0.1f;
        [Export] private float leftRightRollInfluence = 0.01f;
        [Export] private float mouseSensitivity = 0.5f;

        private float yawPreviousRotation = 0f;
        private Vector3 previousYawDirection;
        private float currentRollVel = 0f;

        private float previousYaw = 0f;
        private float rollRotationTarget = 0f;
        private float yawCounterReductionAmount = 1f;
        private float yawDelta = 0f;
        private float rollVel = 0f;
        private float rollVirtual = 0f;

        public Node3D YawGimbal { get { return yawGimbal; } }
        public static Vector3 HeadCurrentDirection { get { return -Instance.rollGimbal.GlobalBasis.Z; } }
        public static Vector3 HeadCurrentRight { get { return  -Instance.yawGimbal.GlobalBasis.X; } }
        public static Vector3 HeadCurrentUp { get { return  -Instance.yawGimbal.GlobalBasis.Y; } }
        public static Vector3 HeadCurrentPosition { get { return Instance.GlobalPosition; } }

        public override void _Ready()
        {
            Instance = this;

            MouseMovementController.YawAxisSignal += YawRotate;
            MouseMovementController.PitchAxisSignal += PitchRotate;
        }

        public override void _Process(double delta)
        {
            RollHead((float)delta);
        }

        private void YawRotate(Vector3 yawDelta)
        {
            yawDelta *= mouseSensitivity;
            yawGimbal.Rotation += yawDelta;
            this.yawDelta = yawDelta.Y;
            OnYawDeltaRadians?.Invoke(yawDelta);
        }

        private void PitchRotate(Vector3 pitchDelta)
        {
            pitchDelta *= mouseSensitivity;
            var newRotation = pitchGimbal.Rotation + pitchDelta;
            newRotation = new Vector3(Mathf.Clamp(newRotation.X, Mathf.DegToRad(-89f), Mathf.DegToRad(89f)), 0f, 0f);
            pitchGimbal.Rotation = newRotation;
        }

        private void RollHead(float delta)
        {
            var yawReduction = Mathf.Sign(rollRotationTarget) * yawCounterReductionAmount * delta;
            if (Mathf.Abs(yawReduction) > Mathf.Abs(rollRotationTarget)) yawReduction = rollRotationTarget;
            rollRotationTarget = rollRotationTarget - yawReduction;

            var rollTargetDelta = Mathf.Clamp(this.yawDelta + (-FirstPersonController.Instance.LocalDirection.X * leftRightRollInfluence), -0.1f, 0.1f);
            rollRotationTarget += rollTargetDelta;
            rollRotationTarget = Mathf.Clamp(rollRotationTarget, -maxRoll, maxRoll);

            var isSpring = true;

            if (isSpring)
            {
                var offset = -rollRotationTarget - rollVirtual;
                var spring = offset * rollSpringStr;
                var damp = rollVel * rollSpringDamp;
                var springDamp = spring - damp;
                var springVirtForce = springDamp;

                rollVel += springVirtForce;
                rollVirtual += rollVel;
            }

            var isExponential = false;

            if (isExponential)
            {
                var rollDifference = -rollRotationTarget - rollVirtual;
                var targetVelocity = rollDifference * 0.3f;
                var deltaVelocity = targetVelocity - rollVel;

                rollVel += deltaVelocity;
                rollVirtual += rollVel;
            }

            rollGimbal.Rotation = new Vector3(0f, 0f, rollVirtual);

            //DU.Log(rollVirtual);
            //DU.Log([yawRotationTarget, yawDelta]);
        }

        
    }
}

