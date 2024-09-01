namespace TowerDefence.Resources.Modifiers
{
    public class ToStringModifier : Modifier<int, string>
    {
        protected override string Modify(int value)
        {
            return value.ToString();
        }
    }
}