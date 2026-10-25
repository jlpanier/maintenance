namespace Business
{
    /// <summary>
    /// Objet représentant une ligne de note
    /// </summary>
    public interface ILine
    {
        int Id { get; }

        string Name { get; }

        DateTime EffectiveOn { get; }

        int MaintenanceObjectId { get; }

        double TotalAmount { get; }

    }
}
