using System;

namespace TowerDefence.Resources.Objects.DamageTriggers.MouseUtils
{
    /// <summary>
    /// Represents methods as if they're defined in <see cref="MouseButton"/>
    /// </summary>
    public static class MouseButtonExtensions
    {
        /// <summary>
        /// Converts a <see cref="MouseButton"/> into an integer
        /// </summary>
        /// <param name="mouseButton">The mouse button.</param>
        /// <returns>The Unity-specific integer value of a mouse button.</returns>
        /// <exception cref="ArgumentOutOfRangeException">When passed an invalid mouse button</exception>
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