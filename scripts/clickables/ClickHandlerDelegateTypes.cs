using Godot;
using System;

namespace ClickHandler
{
    public class ClickHandlerDelegateTypes
    {
        public delegate void ClickHandlerDelegate(ulong? instanceID, Vector3? position);
    }
}

