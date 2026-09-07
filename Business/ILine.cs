namespace Business
{
    /// <summary>
    /// Objet représentant une ligne de note
    /// </summary>
    public interface ILine
    {
        int Id { get; }

        DateTime EffectiveOn { get; }
    }
}
