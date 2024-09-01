namespace TowerDefence.Resources.Modifiers
{
    public class ClampModifier : Modifier<int, int>
    {
        protected override int Modify(int value)
        {
            return Listener.Manager.GetResource(Listener.Type).ClampAmount();
        }
    }
}