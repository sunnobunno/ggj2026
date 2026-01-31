using Godot;
using System;

public partial class NPCtextbubble : Label3D
{
    [Export] string npcText;
    


    public override void _Ready()
    {
        Text = npcText;
        AnimateText();
    }

    public override void _Process(double delta)
    {
        
    }

    private void AnimateText()
    {
        var initialPosition = Vector3.Zero;
        var initialTransparency = 0f;

        Position = initialPosition;
        Transparency = initialTransparency;
        
        var finalPosition = new Vector3(0f, 1f, 0f);

        var tween = GetTree().CreateTween();
        tween.SetTrans(Tween.TransitionType.Linear);
        tween.TweenProperty(this, "position", finalPosition, 5f);
        tween.TweenCallback(Callable.From(AnimateText));

        var tween2 = GetTree().CreateTween();
        tween2.SetTrans(Tween.TransitionType.Expo);
        tween2.SetEase(Tween.EaseType.In);
        tween2.TweenProperty(this, "transparency", 1f, 4f);
    }
}
