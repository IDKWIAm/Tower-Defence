namespace TowerDefence.Resources.Modifiers
{
    public class PercentModifier : Modifier<int, float>
    {
        protected override float Modify(int value)
        {
            return (float)value / Listener.Manager.GetResource(Listener.Type).MaxAmount;
        }
    }
}