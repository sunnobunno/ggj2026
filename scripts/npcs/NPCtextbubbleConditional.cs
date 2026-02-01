using Godot;
using System;

public partial class NPCtextbubbleConditional : Label3D
{
    [Export] string npcText;
    [Export] string gasMaskText;
    [Export] string moustacheGlassesText;
    [Export] string birdMaskText;
    [Export] string swirlyText;
    [Export] string horseText;
    [Export] string swimText;
    [Export] string pirateText;

    float randSpeed;

    public override void _Ready()
    {
        var rng = new RandomNumberGenerator();
        randSpeed = rng.Randf() + 3f;
        
        Text = npcText;
        AnimateText();
    }

    public override void _Process(double delta)
    {
        SwitchText();
    }

    private void SwitchText()
    {
        var currentMask = Inventory.Instance.Mask;

        switch(currentMask)
        {
            case Inventory.EquippedMask.None:
                Text = npcText;
                break;
            case Inventory.EquippedMask.GasMask:
                if (gasMaskText == null)
                {
                    Text = npcText;
                    break;
                }
                Text = gasMaskText;
                break;
            case Inventory.EquippedMask.MustacheGlasses:
                if (moustacheGlassesText == null)
                {
                    Text = npcText;
                    break;
                }
                Text = moustacheGlassesText;
                break;
            case Inventory.EquippedMask.BirdMask:
                if (birdMaskText == null)
                {
                    Text = npcText;
                    break;
                }
                Text = birdMaskText;
                break;
            case Inventory.EquippedMask.Swirly:
                if (swirlyText == null)
                {
                    Text = npcText;
                    break;
                }
                Text = swirlyText;
                break;
            case Inventory.EquippedMask.Horse:
                if (horseText == null)
                {
                    Text = npcText;
                    break;
                }
                Text = horseText;
                break;
            case Inventory.EquippedMask.Swim:
                if (swimText == null)
                {
                    Text = npcText;
                    break;
                }
                Text = swimText;
                break;
            case Inventory.EquippedMask.Pirate:
                if (pirateText == null)
                {
                    Text = npcText;
                    break;
                }
                Text = pirateText;
                break;
            default:
                Text = npcText;
                break;
        }
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
        tween.TweenProperty(this, "position", finalPosition, randSpeed + 1f);
        tween.TweenCallback(Callable.From(AnimateText));

        var tween2 = GetTree().CreateTween();
        tween2.SetTrans(Tween.TransitionType.Expo);
        tween2.SetEase(Tween.EaseType.In);
        tween2.TweenProperty(this, "transparency", 1f, randSpeed);
    }
}
