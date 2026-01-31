using Godot;
using System;
using static PhysicsComponents.RigidBodyMovementBuffers;
using InputControllers;
using MainRaycaster;

namespace PhysicsComponents
{


    /// <summary>
    /// This component script when given a parent rigidbody, sends a delegate containing a
    /// force to keep the parent RB off the ground with a dampened spring effect.
    /// <para>The following parameters are used to calcualte the force:</para>
    /// <code>
    /// TargetRestingHeight
    /// HoverSpringStrength
    /// HoverSpringDamping
    /// JumpStrength
    /// </code>
    /// </summary>
    public partial class HoverComponent : Node3D, IPhysicsComponent
    {
        public ForceDelegate LinearForce;
        public ForceDelegate LinearImpulse;

        [Export] float targetRestingHeight = 1.0f;
        [Export] float hoverSpringStrength = 100f;
        [Export] float hoverSpringDamping = 10f;
        [Export] float jumpStrength = 7f;
        [Export] bool debug = false;

        private float groundDistance;
        private bool jumpPressed = false;
        private Node3D parentNode;
        private CollisionObject3D parentCO;
        private RigidBody3D parentRB;

        private bool errorState = false;

        public float TargetRestingHeight { get { return targetRestingHeight; } set { targetRestingHeight = value; } }
        public float HoverSpringStrength { get { return hoverSpringStrength; } set { hoverSpringStrength = value; } }
        public float HoverSpringDamping { get { return hoverSpringDamping; } set { hoverSpringDamping = value; } }
        public float JumpStrength { get { return jumpStrength; } set { jumpStrength = value; } }


        public bool IsOnGround
        {
            get
            {
                if (groundDistance > targetRestingHeight) return false;
                else return true;
            }
        }


        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            KeyboardManager.JumpPressed += Jump;
        }

        public void Initiatlize(Node3D parent, float? targetRestingHeight = null,
            float? hoverSpringStrength = null, float? hoverSpringDamping = null,
            float? jumpStrength = null)
        {
            
            
            if (parent as RigidBody3D == null || parent as CollisionObject3D == null)
            {
                DU.Log([parent.Name, " is not of correct type"]);
                errorState = true;
                return;
            }

            this.targetRestingHeight = targetRestingHeight ?? this.targetRestingHeight;
            this.hoverSpringStrength = hoverSpringStrength ?? this.hoverSpringStrength;
            this.hoverSpringDamping = hoverSpringDamping ?? this.hoverSpringDamping;
            this.jumpStrength = jumpStrength ?? this.jumpStrength;

            parentNode = parent;
            parentRB = parent as RigidBody3D;
            parentCO = parent as CollisionObject3D;
        }


        public void CustomProcess(double delta)
        {
            if (errorState)
            {
                DU.Log("Initialization not done correctly");
            }
            
            CastGroundRay();
            Hover();
        }

        private void CastGroundRay()
        {
            
            
            var rayQuery = PhysicsRayQueryParameters3D.Create(GlobalPosition,
                GlobalPosition + new Vector3(0f, -10f, 0f));
            rayQuery.Exclude = new Godot.Collections.Array<Rid> { parentCO.GetRid() };
            var collision = Raycaster.World.IntersectRay(rayQuery);

            if (collision.Count > 0)
            {
                Vector3 collisionPosition = (Vector3)collision["position"];
                Vector3 distance = (GlobalPosition - collisionPosition);
                groundDistance = distance.Length();
            }

            //DebugDraw3D.DrawPoints([(Vector3)collision["position"]]);
            //DU.Log(collision.Count);
        }


        private void Hover()
        {
            if (!IsOnGround) return;

            var targetSpringHeight = CalculateDesiredSpringHeight(
                targetRestingHeight,
                hoverSpringStrength);

            var springOffset = targetSpringHeight - groundDistance;

            var force = CalculateSpringForce(hoverSpringStrength,
                hoverSpringDamping,
                springOffset,
                parentRB.LinearVelocity,
                Vector3.Up);

            //DU.Log(force);

            LinearForce?.Invoke(force);
            if (debug) DU.Log(force);
        }

        private Vector3 CalculateSpringForce(float springStrength, float dampStrength,
            float offset, Vector3 linearVelocity, Vector3 springDirection)
        {
            var velocity = linearVelocity.Dot(springDirection.Normalized());
            velocity = springDirection.Normalized().Dot(linearVelocity);

            var spring = springStrength * offset;
            var damp = dampStrength * velocity;

            var force = spring - damp;
            var forceVector = springDirection * force;

            //GD.Print(forceVector, " ", velocity, " ", offset, " ", GlobalPosition);

            return forceVector;
        }

        private float CalculateDesiredSpringHeight(float targetRestingHeight, float springStrength)
        {
            //var gravityStrength = (float)(ProjectSettings.GetSetting("physics/3d/default_gravity"));

            //var restingOffset = gravityStrength / springStrength;
            //var targetSpringHeight = targetRestingHeight + restingOffset;

            //DU.Log([targetRestingHeight, springStrength, gravityStrength]);

            //GD.Print(restingOffset, " ", groundDistance);

            //return targetSpringHeight;
            return targetRestingHeight;
        }


        //private void CaptureJumpInput()
        //{
        //	var jumpPressed = false;

        //       if (Input.IsActionJustPressed("Jump"))
        //       {
        //           jumpPressed = true;
        //       }

        //	this.jumpPressed = jumpPressed;
        //   }

        private void Jump()
        {
            if (IsOnGround)
            {
                var direction = Vector3.Up;
                var jumpStrength = this.jumpStrength;
                var jumpForce = direction * jumpStrength;

                LinearImpulse?.Invoke(jumpForce);
            }
        }
    }
}

