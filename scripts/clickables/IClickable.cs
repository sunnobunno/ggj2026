using Godot;
using System;

namespace Clickables
{
    public interface IClickable
    {
        bool IsActive { get; set; }

        void LeftClick(Vector3? position);

        void LeftRelease(Vector3? position);

        void RightClick(Vector3? position);

        void RightRelease(Vector3? position);
    }
}

