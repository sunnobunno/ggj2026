using Godot;
using System;
using PhysicsComponents;
using InputControllers.firstPerson;

namespace PlayerControllers.FirstPerson
{
    
    /// <summary>
    /// This singleton script connects to all the components of the first person controller.
    /// It hooks into delegates of each child component.
    /// </summary>
    public partial class FirstPersonController : RigidBody3D
    {
        public static FirstPersonController Instance;
        
        [Export] private float firstPersonLookSpeed = 1f;
        [Export] private float firstPersonLookSmoothing = 5f;
        [Export] private PlayerInputProcessor playerInput;
        [Export] private MovementComponent movementComponent;
        [Export] private HoverComponent hoverComponent;
        [Export] private HeadController head;
        [Export] private float targetRestingHeight = 1f;
        [Export] private float springStrength = 1f;
        [Export] private float springDamp = 0.1f;
        [Export] private float jumpStrength = 2f;
        [Export] private float maxSpeed = 2f;
        [Export] private float moveAccel = 1f;


        private RigidBodyMovementBuffers forceBuffers;


        public Vector3 LocalDirection { get { return PlayerInputProcessor.LocalDirection; } }
        public float FirstPersonLookSpeed { get { return firstPersonLookSpeed; } }
        public float FirstPersonLookSmoothing { get { return firstPersonLookSmoothing; } }
        public float TargetRestingHeight { get { return targetRestingHeight; } }
        public float SpringStrength { get { return springStrength; } }
        public float SpringDamp { get { return springDamp; } }
        public float JumpStrength { get { return jumpStrength; } }
        public float MaxSpeed { get { return maxSpeed; } }
        public float MoveAccel { get { return moveAccel; } }

        public override void _Ready()
        {
            Instance = this;

            forceBuffers = new RigidBodyMovementBuffers(this);

            movementComponent.Initialize(this);
            movementComponent.LinearForce += forceBuffers.AddToLinearForceBuffer;

            hoverComponent.Initiatlize(this);
            hoverComponent.LinearForce += forceBuffers.AddToLinearForceBuffer;
            hoverComponent.LinearImpulse += forceBuffers.AddToLinearImpulseBuffer;
        }

        public override void _PhysicsProcess(double delta)
        {
            movementComponent.RecieveInput(LocalDirection * head.YawGimbal.GlobalBasis.Inverse());
            movementComponent.RecieveProperties(MaxSpeed, MoveAccel);
            movementComponent.CustomProcess((float) delta);

            hoverComponent.CustomProcess((float) delta);

            forceBuffers.ApplyAllForceBuffers();
        }
    }
}

