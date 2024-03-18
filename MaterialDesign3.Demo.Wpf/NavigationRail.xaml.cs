using MahApps.Metro.IconPacks;
using MaterialDesign3Demo.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo;

public partial class NavigationRail
{

    public List<SampleItem> SampleList { get; set; }

    public NavigationRail()
    {
        InitializeComponent();
        DataContext = this;

        SampleList = new()
        {
            new SampleItem
            {
                Title = "Payment",
                SelectedIcon = PackIconMaterialKind.CreditCard,
                UnselectedIcon = PackIconMaterialKind.CreditCardOutline,
                Notification = 1
            },
            new SampleItem
            {
                Title = "Home",
                SelectedIcon = PackIconMaterialKind.Home,
                UnselectedIcon = PackIconMaterialKind.HomeOutline,
            },
            new SampleItem
            {
                Title = "Special",
                SelectedIcon = PackIconMaterialKind.Star,
                UnselectedIcon = PackIconMaterialKind.StarOutline,
            },
            new SampleItem
            {
                Title = "Shared",
                SelectedIcon = PackIconMaterialKind.AccountMultiple,
                UnselectedIcon = PackIconMaterialKind.AccountMultipleOutline,
            },
            new SampleItem
            {
                Title = "Files",
                SelectedIcon = PackIconMaterialKind.Folder,
                UnselectedIcon = PackIconMaterialKind.FolderOutline,
            },
            new SampleItem
            {
                Title = "Library",
                SelectedIcon = PackIconMaterialKind.Bookshelf,
                UnselectedIcon = PackIconMaterialKind.Bookshelf,
            },
        };
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        => SampleList[0].Notification = SampleList[0].Notification is null ? 1 : null;

    private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        => SampleList[0].Notification = SampleList[0].Notification is null ? "123+" : null;
}
