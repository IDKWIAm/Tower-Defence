namespace TowerDefence.Resources.Modifiers
{
    /// <summary>
    /// Transforms the resource value into a fraction from 0.0 to 1.0.
    /// </summary>
    public class PercentModifier : Modifier<int, float>
    {
        protected override float Modify(int value)
        {
            return (float)value / Listener.Manager.GetResource(Listener.Type).MaxAmount;
        }
    }
}