using Godot;
using System;
using MainRaycaster;
using static ClickHandler.ClickHandlerDelegateTypes;

namespace ClickHandler
{
    /// <summary>
    /// This script subscribes to the generic IRaycaster.OnNodeHit delegate
    /// and processes it invoke another delegate if a mouse click occurs on
    /// the same frame. This mouse does not directly interface with any
    /// <c>IClickables</c> interfaces. Use a <c>ClickablesBridge</c> script
    /// to interface with any game objects.
    /// <para>Available delegates. NOTE this is not a singleton  script
    /// and these delegates are not static so
    /// you will need a direct reference to an instance of this class.</para>
    /// <code>
    /// OnLeftClick
    /// OnRightClick
    /// OnLeftRelease
    /// OnRightRelease
    /// OnWheelUp
    /// OnWheelDown
    /// </code>
    /// </summary>
    public partial class ClickHandler : Node, IClickHandler
    {
        public ClickHandlerDelegate OnLeftClick { get; set; }
        public ClickHandlerDelegate OnRightClick { get; set; }
        public ClickHandlerDelegate OnLeftRelease {  get; set; }
        public ClickHandlerDelegate OnRightRelease {  get; set; }
        public ClickHandlerDelegate OnWheelUp { get; set; }
        public ClickHandlerDelegate OnWheelDown { get; set; }

        [Export] Node _raycaster;
        IRaycaster raycaster;

        public override void _Ready()
        {
            raycaster = (IRaycaster)_raycaster;
            
            raycaster.OnNodeHit += RecieveNodeHit;
        }

        public override void _Process(double delta)
        {
            CheckClickInput();
        }

        private void RecieveNodeHit(ulong instanceID, Vector3 position)
        {
            CheckClickInput(instanceID, position);
        }

        // This is called every Process frame with a null value
        // And when a designated raycaster hits an object
        private void CheckClickInput(ulong? instanceID, Vector3? position)
        {
            if (Input.IsActionJustPressed("LeftClick"))
            {
                OnLeftClick?.Invoke(instanceID, position);
            }

            if (Input.IsActionJustPressed("RightClick"))
            {
                OnRightClick?.Invoke(instanceID, position);
            }

            if (Input.IsActionJustReleased("LeftClick"))
            {
                OnLeftRelease?.Invoke(instanceID, position);
            }

            if (Input.IsActionJustReleased("RightClick"))
            {
                OnRightRelease?.Invoke(instanceID, position);
            }
        }

        private void CheckClickInput()
        {
            CheckClickInput(null, null);
        }

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventMouseButton)
            {
                InputEventMouseButton emb = (InputEventMouseButton)@event;
                if (emb.ButtonIndex == MouseButton.WheelUp)
                {
                    OnWheelUp?.Invoke(null, null);
                }
                else if (emb.ButtonIndex == MouseButton.WheelDown)
                {
                    OnWheelDown?.Invoke(null, null);
                }
            }
        }
    }
}

