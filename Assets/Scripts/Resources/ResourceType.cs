using System;

namespace TowerDefence.Resources
{
    /// <summary>
    /// Represents all possible resource types.
    /// </summary>
    [Serializable]
    public enum ResourceType
    {
        Wood,
        ZipFile
    }

    /// <summary>
    /// Contains extension methods for <see cref="ResourceType"/>
    /// </summary>
    public static class ResourceTypeExtensions
    {
        /// <summary>
        /// gets name of enum constant
        /// </summary>
        /// <param name="resourceType">type</param>
        /// <returns>name of resource type</returns>
        /// <exception cref="ArgumentOutOfRangeException">resource type not added to switch</exception>
        public static string GetName(this ResourceType resourceType)
        {
            return resourceType switch
                {
                    ResourceType.Wood => "Wood",
                    _ => throw new ArgumentOutOfRangeException(nameof(resourceType),
                        $"Invalid resource type {Enum.GetName(typeof(ResourceType), resourceType)} (did you forget to add a switch case to ResourceTypeExtensions?)")
                };
        }
    }
}