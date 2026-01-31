using Godot;
using System;
using System.Collections.Generic;

namespace PhysicsComponents
{
    public partial class RigidBodyMovementBuffers : Resource
    {
        public delegate void PositionalForceDelegate(Vector3 force, Vector3 globalPosition);
        public delegate void ForceDelegate(Vector3? force);

        private List<(Vector3 force, Vector3 globalPosition)> positionalLinearForceBuffer;
        private List<(Vector3 force, Vector3 torque)> positionalAngularForceBuffer;
        private Vector3 linearForceBuffer;
        private Vector3 angularForceBuffer;
        private Vector3 linearImpulseBuffer;
        private Vector3 angularImpulseBuffer;

        private RigidBody3D rigidBody;

        public Vector3 LinearForceBuffer { get { return linearForceBuffer; } }
        public Vector3 AngularForceBuffer { get { return angularForceBuffer; } }
        public Vector3 LinearImpulseBuffer { get { return linearImpulseBuffer; } }
        public Vector3 AngularImpulseBuffer { get { return angularImpulseBuffer; } }


        public RigidBodyMovementBuffers(RigidBody3D rigidBody)
        {
            this.rigidBody = rigidBody;

            linearForceBuffer = Vector3.Zero;
            angularForceBuffer = Vector3.Zero;
            linearImpulseBuffer = Vector3.Zero;
            angularImpulseBuffer = Vector3.Zero;
            positionalLinearForceBuffer = new List<(Vector3 force, Vector3 globalPosition)>();
            positionalAngularForceBuffer = new List<(Vector3 force, Vector3 torque)>();
            
        }


        public void ApplyAllForceBuffers()
        {
            ApplyLinearForceBuffer();
            ApplyAngularForceBuffer();
            ApplyLinearImpulseBuffer();
            ApplyAngularImpulseBuffer();
            ApplyPositionalLinearForceBuffer();
            ApplyPositionalAngularForceBuffer();
            
        }

        public void ApplyLinearForceBuffer()
        {
            rigidBody.ApplyCentralForce(linearForceBuffer);
            linearForceBuffer = Vector3.Zero;
        }

        public void AddToLinearForceBuffer(Vector3? force)
        {
            linearForceBuffer += force ?? Vector3.Zero;
        }

        public void ApplyAngularForceBuffer()
        {
            rigidBody.ApplyTorque(angularForceBuffer);
            angularForceBuffer = Vector3.Zero;
        }

        public void AddToAngularForceBuffer(Vector3? force)
        {
            angularForceBuffer += force ?? Vector3.Zero;
        }

        public void AddToLinearImpulseBuffer(Vector3? force)
        {
            linearImpulseBuffer += force ?? Vector3.Zero;
        }

        public void ApplyLinearImpulseBuffer()
        {
            rigidBody.ApplyCentralImpulse(linearImpulseBuffer);
            linearImpulseBuffer = Vector3.Zero;
        }

        public void AddToAngularImpulseBuffer(Vector3 force)
        {
            angularImpulseBuffer += force;
        }

        public void ApplyAngularImpulseBuffer()
        {
            rigidBody.ApplyTorqueImpulse(angularImpulseBuffer);
            angularImpulseBuffer = Vector3.Zero;
        }

        public void AddToPositionalLinearForceBuffer(Vector3 force, Vector3 globalPosition)
        {
            if (positionalLinearForceBuffer == null) positionalLinearForceBuffer = new List<(Vector3 force, Vector3 globalPosition)>();

            (Vector3, Vector3) positionalForce = (force, globalPosition);
            positionalLinearForceBuffer.Add(positionalForce);
        }

        public void ApplyPositionalLinearForceBuffer()
        {
            foreach ((Vector3 force, Vector3 globalPosition) positionalForce in positionalLinearForceBuffer)
            {
                rigidBody.ApplyForce(positionalForce.force, positionalForce.globalPosition);
            }

            positionalLinearForceBuffer.Clear();
        }


        /// <summary>
        /// Currently no way to apply positional torque
        /// </summary>
        /// <param name="force"></param>
        /// <param name="globalPosition"></param>
        public void AddToPositionalAngularForceBuffer(Vector3 force, Vector3 globalPosition)
        {
            if (positionalAngularForceBuffer == null) positionalAngularForceBuffer = new List<(Vector3 force, Vector3 globalPosition)>();

            (Vector3, Vector3) positionalForce = (force, globalPosition);
            positionalAngularForceBuffer.Add(positionalForce);
        }

        /// <summary>
        /// Currently no way to apply positional torque
        /// </summary>
        public void ApplyPositionalAngularForceBuffer()
        {
            foreach ((Vector3 force, Vector3 globalPosition) positionalForce in positionalAngularForceBuffer)
            {
                
            }

            positionalAngularForceBuffer.Clear();
        }
    }
}
