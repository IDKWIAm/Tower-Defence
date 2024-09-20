namespace TowerDefence.Resources.Modifiers
{
    /// <summary>
    /// Converts an integer value to a string.
    /// </summary>
    public class ToStringModifier : Modifier<int, string>
    {
        protected override string Modify(int value)
        {
            return value.ToString();
        }
    }
}