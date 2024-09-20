namespace TowerDefence.Resources.Modifiers
{
    /// <summary>
    /// Clamps a resource value between its bounds.
    /// </summary>
    public class ClampModifier : Modifier<int, int>
    {
        protected override int Modify(int value)
        {
            return Listener.Manager.GetResource(Listener.Type).ClampAmount();
        }
    }
}