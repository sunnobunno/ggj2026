using Godot;
using System;

public partial class InventoryUI : Node
{
    [Export] MeshInstance2D inventoryMesh;
    [Export] Texture2D[] keyFrames;
    [Export] float speed = 5f;

    float t = 0f;

    public override void _Ready()
    {
        inventoryMesh.Visible = false;
    }

    public override void _Process(double delta)
    {
        if (Inventory.Instance.HasKey)
        {
            inventoryMesh.Visible = true;
        }
        else inventoryMesh.Visible = false;

        AnimateKey(delta);
    }

    private void AnimateKey(double delta)
    {
        t += (float)delta * speed;

        var frameIndex = (int)t%2;
        var frame = keyFrames[frameIndex];

        inventoryMesh.Texture = frame;
    }
}
