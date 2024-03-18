using MahApps.Metro.IconPacks;

namespace MaterialDesign3Demo.Domain;

public class SampleItem : ViewModelBase
{
    public string? Title { get; set; }
    public PackIconMaterialKind SelectedIcon { get; set; }
    public PackIconMaterialKind UnselectedIcon { get; set; }
    private object? _notification = null;

    public object? Notification
    {
        get { return _notification; }
        set { SetProperty(ref _notification, value); }
    }

}
