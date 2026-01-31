using Godot;
using System;

namespace MainRaycaster
{
    public class RaycasterDelegateTypes
    {
        public delegate void RaycasterDelegate(ulong instanceID, Vector3 position);
    }
}

