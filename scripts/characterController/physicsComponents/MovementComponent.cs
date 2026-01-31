using Godot;
using System;
using static PhysicsComponents.RigidBodyMovementBuffers;

namespace PhysicsComponents
{
    
    /// <summary>
    /// This component script recieves a direction vector3 via <c>RecieveCurrentVel</c>
    /// and then calls its delegate <c>LinearForce</c> with a corresponding force.
    /// </summary>
    public partial class MovementComponent : Node, IPhysicsComponent
    {
        public PositionalForceDelegate PositionalLinearForce;
        public PositionalForceDelegate PositionalAngularForce;
        public ForceDelegate LinearForce;
        public ForceDelegate AngularForce;

        [Export] float maxSpeed = 4f;
        [Export] float acceleration = 9.5f;
        [Export] bool debug = false;

        private bool errorState = false;

        private Vector3 direction;
        private bool inputReceived = false;

        private RigidBody3D parentRB;

        public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }
        public float Acceleration { get { return acceleration; } set { acceleration = value; } }

        public override void _Ready()
        {
            direction = Vector3.Zero;
        }

        public void Initialize(RigidBody3D parent)
        {
            if (parent as RigidBody3D == null)
            {
                DU.Log("Initialization failed");
                errorState = true;
                return;
            }
            
            parentRB = parent;
        }

        public void CustomProcess(double delta)
        {
            if (errorState)
            {
                DU.Log("error state");
                return;
            }
            

            Move((float)delta);

            inputReceived = false;
        }

        public void RecieveProperties(float maxSpeed, float acceleration)
        {
            MaxSpeed = maxSpeed;
            Acceleration = acceleration;
        }

        public void RecieveInput(Vector3 direction)
        {
            var currentDirFlattened = VU.ProjectVectorToNormalPlane(direction, Vector3.Up);
            this.direction = currentDirFlattened.Normalized();
            if (direction.IsZeroApprox()) this.direction = Vector3.Zero;
            inputReceived = true;
        }



        private void Move(float delta)
        {
            // get target velocity
            // calculate change in velocity from current velocity

            if (!inputReceived)
            {
                direction = Vector3.Zero;
            }

            
            var targetVel = direction * MaxSpeed;


            var currentPlanarVelocity = VU.ProjectVectorToNormalPlane(parentRB.LinearVelocity, Vector3.Up);
            var deltaVel = targetVel - currentPlanarVelocity;
            var deltaVelDir = deltaVel.Normalized();
            var accel = (deltaVel * Acceleration);
            var force = parentRB.Mass * accel;

            //DU.Log(["targetVel: ", targetVel, "force", force]);

            //if (debug) DU.Log([force, direction]);
            //if (debug) DebugDraw3D.DrawRay(parentRB.GlobalPosition, direction, 1f);
            //DU.Log(direction);

            LinearForce?.Invoke(force);
        }
    }
}

