using ClickHandler;
using Godot;
using System;

namespace Clickables
{
    /// <summary>
    /// This singleton script acts as an intermediate layer connecting the input of a
    /// <c>ClickHandler</c> to the rest of the game world. It recieves delegate
    /// calls from the ClickHandler and calls interface members on game world
    /// objects. It also contains static delegates useful for other classes to
    /// determine if a mouse button was clicked or released.
    /// <para>Available delegates:</para>
    /// <code>
    /// OnLeftClick
    /// OnLeftRelease
    /// OnRightClick
    /// OnRightRelease
    /// </code>
    /// 
    /// 
    /// </summary>
    public partial class ClickablesBridge : Node
    {
        public delegate void ClickablesBridgeDelegate();
        public static ClickablesBridgeDelegate OnLeftClick;
        public static ClickablesBridgeDelegate OnLeftRelease;
        public static ClickablesBridgeDelegate OnRightClick;
        public static ClickablesBridgeDelegate OnRightRelease;
        public static ClickablesBridgeDelegate OnWheelUp;
        public static ClickablesBridgeDelegate OnWheelDown;
        
        
        [Export] Node _clickHandler;

        IClickHandler clickHandler;
        
        public override void _Ready()
        {
            clickHandler = (IClickHandler)_clickHandler;
            clickHandler.OnLeftClick += ReceiveLeftClick;
            clickHandler.OnLeftRelease += ReceiveLeftRelease;
            clickHandler.OnRightClick += ReceiveRightClick;
            clickHandler.OnRightRelease += ReceiveRightRelease;
            clickHandler.OnWheelUp += ReceiveWheelUp;
            clickHandler.OnWheelDown += ReceiveWheelDown;
        }


        private void ReceiveLeftClick(ulong? _instanceID, Vector3? position)
        {
            
            OnLeftClick?.Invoke();
            if (_instanceID == null) return;

            //DU.Log(["Received left click", _instanceID]);

            var instanceID = (ulong)_instanceID;
            var node = InstanceFromId(instanceID);
            if (node is IClickable clickable)
            {
                clickable.LeftClick(position);
            }
        }

        private void ReceiveLeftRelease(ulong? _instanceID, Vector3? position)
        {
            OnLeftRelease?.Invoke();
            if (_instanceID == null) return;

            var instanceID = (ulong)_instanceID;
            var node = InstanceFromId(instanceID);
            if (node is IClickable clickable)
            {
                clickable.LeftRelease(position);
            }
        }

        private void ReceiveRightClick(ulong? _instanceID, Vector3? position)
        {
            OnRightClick?.Invoke();
            if (_instanceID == null) return;

            var instanceID = (ulong)_instanceID;
            var node = InstanceFromId(instanceID);
            if (node is IClickable clickable)
            {
                clickable.RightClick(position);
            }
        }

        private void ReceiveRightRelease(ulong? _instanceID, Vector3? position)
        {
            OnRightRelease?.Invoke();
            if (_instanceID == null) return;

            var instanceID = (ulong)_instanceID;
            var node = InstanceFromId(instanceID);
            if (node is IClickable clickable)
            {
                clickable.RightRelease(position);
            }
        }

        private void ReceiveWheelUp(ulong? _instanceID, Vector3? position)
        {
            OnWheelUp?.Invoke();
        }

        private void ReceiveWheelDown(ulong? _instanceID, Vector3? position)
        {
            OnWheelDown?.Invoke();
        }
    }
}

