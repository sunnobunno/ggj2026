using Godot;
using System;
using static MainRaycaster.RaycasterDelegateTypes;

namespace MainRaycaster
{
    /// <summary>
    /// This singleton script casts a ray from a given main camera to either a point
    /// in the center of the screen or the mouse's position in the world.
    /// <para>This is intended to work in tandem with another script, such
    /// as <c>ClickHandler</c>. This script only invokes a delegate whenever the ray
    /// hits an object within the clickables mask layer given. It does <b>not</b> account for
    /// the user providing a click input.
    /// </para>
    /// </summary>
    public partial class Raycaster : Node3D, IRaycaster
    {
        public static Raycaster Instance;
        
        public RaycasterDelegate OnNodeHit { get; set; }
        
        [Export] Camera3D mainCamera;
        [Export] bool castToCenterScreen = false;
        [Export(PropertyHint.Layers3DPhysics)] private uint castableMask;
        [ExportGroup("Optional")]
        [Export] Node planeNode;

        PhysicsDirectSpaceState3D world;

        public static bool RaycastFromMainCameraDirection { get; private set; }
        public static PhysicsDirectSpaceState3D World { get; private set; }

#nullable enable

        public override void _Ready()
        {
            Instance = this;
            world = GetWorld3D().DirectSpaceState;
            World = world;
        }

        public override void _Process(double delta)
        {
            world = GetWorld3D().DirectSpaceState;
            World = world;

            CastMouseRayFromMainCamera();
        }


        private void CastMouseRayFromMainCamera()
        {
            // Adapt this to have a toggle between free mouse and fixed to center point

            Vector3 cameraOrigin;
            Vector3 cameraNormal;
            
            // Determine if casting to mouse position or center screen
            if (castToCenterScreen)
            {
                cameraOrigin = mainCamera.GlobalPosition;
                cameraNormal = -mainCamera.GlobalBasis.Z;
            }
            else
            {
                var mousePosition = GetViewport().GetMousePosition();
                cameraOrigin = mainCamera.ProjectRayOrigin(mousePosition);
                cameraNormal = mainCamera.ProjectRayNormal(mousePosition);
            }
            

            var rayQuery = PhysicsRayQueryParameters3D.Create(cameraOrigin, cameraOrigin + cameraNormal * 100f);
            rayQuery.CollisionMask = castableMask;
            rayQuery.CollideWithAreas = true;
            rayQuery.HitBackFaces = true;

            var collision = world.IntersectRay(rayQuery);
            if (collision.Count > 0)
            {
                var obj = (Node)collision["collider"];
                var position = (Vector3)collision["position"];
                OnNodeHit?.Invoke(obj.GetInstanceId(), position);
            }
        }

        public static void ForceCallCastRay()
        {
            Instance.CastMouseRayFromMainCamera();
        }
    }
}

