using DevExpress.Maui.Charts;

namespace Main.ViewModels
{
    /// <summary>
    /// Useless since year
    /// </summary>
    public class CustomColorizer : ICustomPointColorizer
    {
        Color ICustomPointColorizer.GetColor(ColoredPointInfo info)
        {
            switch (info.DateTimeArgument.Month)
            {
                case 12:
                case 1:
                case 2:
                default:
                    return Color.FromArgb("#5982db");
                case 3:
                case 4:
                case 5:
                    return Color.FromArgb("#755dd9");
                case 6:
                case 7:
                case 8:
                    return Color.FromArgb("#9b57d3");
                case 9:
                case 10:
                case 11:
                    return Color.FromArgb("#92278f");
            }
        }
        public ILegendItemProvider? GetLegendItemProvider()
        {
            return null;
        }
    }

    /// <summary>
    /// Affichage des dépenses annuelles
    /// </summary>
    public class DataAnnualExpense
    {
        public DateTime Argument { get; set; }
        public double Value { get; set; }
    }

    /// <summary>
    /// View model du graphique des dépenses annuelles
    /// </summary>
    public class OverviewViewModel : BaseViewModel
    {
        public List<DataAnnualExpense> Data { get; }
        
        public OverviewViewModel()
        {
            Data = new List<DataAnnualExpense>();
            var years = Business.Product.All.Select(i => i.EffectiveOn.Year).Distinct();
            foreach(var year in years)
            {
                var total = Business.Product.All.Where(i => i.EffectiveOn.Year == year).Sum(i => i.TotalAmount);
                var item = new DataAnnualExpense() { Argument = new DateTime(year, 1, 1), Value = total };
                Data.Add(item);
            }
        }
    }
}