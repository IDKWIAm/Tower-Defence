using System;

namespace TowerDefence.Resources.Objects.DamageTriggers.MouseUtils
{
    public static class MouseButtonExtensions
    {
        public static int ToInt(this MouseButton mouseButton)
        {
            return mouseButton switch
                {
                    MouseButton.Left => 0,
                    MouseButton.Right => 1,
                    MouseButton.Middle => 2,
                    _ => throw new ArgumentOutOfRangeException(nameof(mouseButton), mouseButton, null)
                };
        }
    }
}