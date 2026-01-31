using Clickables;
using Godot;
using System;

public partial class MaskDoorController : Node3D, IClickable
{
    bool isActive = true;
    public bool IsActive { get => isActive; set => isActive = value; }

    Vector3 finalRotation;
    Vector3 initialRotation;

    bool isOpen = false;

    public override void _Ready()
    {
        finalRotation = new Vector3(0f, Mathf.DegToRad(-85f), 0f);
        initialRotation = Vector3.Zero;
    }

    public override void _Process(double delta)
    {
        CheckIfWearingMask();
    }

    private void OpenDoor()
    {
        if (isOpen) return;
        isOpen = true;
        var tween = GetTree().CreateTween();
        tween.SetTrans(Tween.TransitionType.Spring);
        tween.SetEase(Tween.EaseType.Out);
        tween.TweenProperty(this, "rotation", finalRotation, 1f);
    }

    private void CloseDoor()
    {
        if (!isOpen) return;
        isOpen = false;
        var tween = GetTree().CreateTween();
        tween.SetTrans(Tween.TransitionType.Spring);
        tween.SetEase(Tween.EaseType.Out);
        tween.TweenProperty(this, "rotation", initialRotation, 1f);
    }

    private void CheckIfWearingMask()
    {
        var currentMask = Inventory.Instance.Mask;
        if (currentMask == Inventory.EquippedMask.GasMask || currentMask == Inventory.EquippedMask.None)
        {
            CloseDoor();
        }
        else OpenDoor();
    }



    public void LeftClick(Vector3? position)
    {

    }

    public void LeftRelease(Vector3? position)
    {
        
    }

    public void RightClick(Vector3? position)
    {
        
    }

    public void RightRelease(Vector3? position)
    {
        
    }
}
