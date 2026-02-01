using Clickables;
using Godot;
using System;

public partial class DoorController : Node3D, IClickable
{
	bool isActive = true;
	public bool IsActive { get => isActive; set => isActive = value; }

	public override void _Process(double delta)
	{
		
	}

	private void OpenDoor()
	{
		var finalRotation = new Vector3(0f, Mathf.DegToRad(-85f), 0f);
		
		var tween = GetTree().CreateTween();
		tween.SetTrans(Tween.TransitionType.Spring);
		tween.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(this, "rotation", finalRotation, 1f);
	}

	private void CloseDoor()
	{
		var finalRotation = new Vector3(0f, Mathf.DegToRad(0f), 0f);

		var tween = GetTree().CreateTween();
		tween.SetTrans(Tween.TransitionType.Spring);
		tween.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(this, "rotation", finalRotation, 1f);
	}





	public void LeftClick(Vector3? position)
	{
		if (Inventory.Instance.HasKey)
		{
			OpenDoor();
			Inventory.Instance.HasKey = false;
		}
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
