using Godot;
using System;

namespace ClickHandler
{
    public interface IClickHandler
    {
        ClickHandlerDelegateTypes.ClickHandlerDelegate OnLeftClick { get; set; }
        ClickHandlerDelegateTypes.ClickHandlerDelegate OnRightClick { get; set; }
        ClickHandlerDelegateTypes.ClickHandlerDelegate OnLeftRelease { get; set; }
        ClickHandlerDelegateTypes.ClickHandlerDelegate OnRightRelease { get; set; }
        ClickHandlerDelegateTypes.ClickHandlerDelegate OnWheelUp { get; set; }
        ClickHandlerDelegateTypes.ClickHandlerDelegate OnWheelDown { get; set; }
    }
}

