using FFImageLoading.Helpers;

namespace Main.ViewModels;


/// <summary>
/// Gestion des produits de maintenance
/// </summary>
internal class EditProductViewModel : EditWorkViewModel
{
    /// <summary>
    /// Produit utilisé
    /// </summary>
    public string ProductName
    {
        get => _productName;
        set
        {
            if (_productName != value)
            {
                _productName = value;
                NotifyPropertyChanged(nameof(ProductName));
            }
        }
    }
    private string _productName = "";

    /// <summary>
    /// Quantité de produit utilisé
    /// </summary>
    public string Quantity
    {
        get => _quantity;
        set
        {
            if (_quantity != value)
            {
                _quantity = value;
                NotifyPropertyChanged(nameof(Quantity));
            }
        }
    }
    private string _quantity = "";

    /// <summary>
    /// Prix unitaire
    /// </summary>
    public string UnitPrice
    {
        get => _unitPrice;
        set
        {
            if (_unitPrice != value)
            {
                _unitPrice = value;
                NotifyPropertyChanged(nameof(UnitPrice));
            }
        }
    }
    private string _unitPrice = "";

    /// <summary>
    /// Initialisation de la page 
    /// </summary>
    public override void Init(int key)
    {
        _key = key;
        Init(Business.Line.GetLine(_key));
    }

    /// <summary>
    /// Initialisation de la page 
    /// </summary>
    protected override void Init(Business.ILine? item)
    {
        base.Init(item);
        if (item is Business.Product product)
        {
            ProductName = product.ProductName;
            Quantity = product.Quantity.ToString();
            UnitPrice = product.UnitPrice.ToString();
        }
    }

    /// <summary>
    /// Sauvegarde des données
    /// </summary>
    public async override void OnSave()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Desc))
            {
                throw new Exception("Une description doit être précisée.");
            }

            if (string.IsNullOrWhiteSpace(ProductName))
            {
                throw new Exception("Un produit doit être précisée.");
            }

            if (SelectedInvoice == null)
            {
                throw new Exception("Sélection d'une facture.");
            }

            if (!double.TryParse(UnitPrice.Replace(".", ","), out double unitprice))
            {
                throw new Exception("Prix unitaire invalide.");
            }

            if (!double.TryParse(Quantity.Replace(".", ","), out double quantity))
            {
                throw new Exception("Quantité invalide.");
            }

            var line = Business.Line.GetLine(_key);
            if (line is Business.Product item)
            {
                item.Update(EffectiveOn, Desc, Images, SelectedInvoice, ProductName, quantity, unitprice);
            }
            else
            {
                Business.Product.Create(EffectiveOn, Desc, Images, SelectedInvoice, ProductName, quantity, unitprice);
            }
            await Shell.Current.GoToAsync(".."); // Retour à la page précédente
        }
        catch (Exception ex)
        {
            await ServiceHelper.GetService<IAlertService>()!.ShowAlertAsync(ex);
        }
    }

    /// <summary>
    /// Suppression de la ligne
    /// </summary>
    public async override void OnDelete()
    {
        var item = Business.Line.GetLine(_key);
        if (item is Business.Product product)
        {
            product.Delete();
        }
        await Shell.Current.GoToAsync(".."); // Retour à la page précédente
    }
}
