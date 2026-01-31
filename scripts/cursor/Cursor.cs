using Clickables;
using Godot;
using MainRaycaster;
using System;
using System.Collections.Generic;

public partial class Cursor : Control
{
    [Export] Texture2D[] animationFrames;
    [Export] MeshInstance2D meshInstance;
    [Export] float speed = 2f;
    float t = 0f;
    bool _visible = false;
    
    public override void _Ready()
    {
        Raycaster.Instance.OnNodeHit += OnNodeHit;
    }

    public override void _Process(double delta)
    {
        AnimateCursor(delta);
        meshInstance.Visible = _visible;
        _visible = false;
    }

    private void OnNodeHit(ulong _instanceID, Vector3 position)
    {
        var instanceID = (ulong)_instanceID;
        var node = InstanceFromId(instanceID);
        if (node is IClickable clickable)
        {
            _visible = true;
        }
    }

    private void AnimateCursor(double delta)
    {
        
        if (!_visible)
        {
            t = 0f;
            return;
        }

        t += (float)delta * speed;

        var frameIndex = (int)(t % 2f);
        var frame = animationFrames[frameIndex];

        meshInstance.Texture = frame;
    }
}
