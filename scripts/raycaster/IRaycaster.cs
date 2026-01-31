using Godot;
using System;

namespace MainRaycaster
{
    public interface IRaycaster
    {
        RaycasterDelegateTypes.RaycasterDelegate OnNodeHit { get; set; }
    }
}

