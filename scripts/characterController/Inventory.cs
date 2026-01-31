using Godot;
using System;

public partial class Inventory : Node3D
{
    public enum EquippedMask
    {
        None,
        GasMask,
        BirdMask,
        MustacheGlasses
    }

    public static Inventory Instance;

    EquippedMask mask = EquippedMask.None;
    bool hasKey = false;

    public EquippedMask Mask { get => mask; set => mask = value; }
    public bool HasKey { get => hasKey; set => hasKey = value; }

    public override void _Ready()
    {
        Instance = this;
    }

    public override void _Process(double delta)
    {
        
    }
}
