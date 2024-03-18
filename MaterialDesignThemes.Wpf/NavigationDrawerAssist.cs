using MahApps.Metro.IconPacks;

namespace MaterialDesignThemes.Wpf;

public static class NavigationDrawerAssist
{
    private static readonly CornerRadius DefaultCornerRadius = new CornerRadius(2.0);

    #region CornerRadius
    /// <summary>
    /// Controls the corner radius of the selection box.
    /// </summary>
    public static readonly DependencyProperty CornerRadiusProperty
        = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(NavigationDrawerAssist), new PropertyMetadata(DefaultCornerRadius));

    public static CornerRadius GetCornerRadius(DependencyObject element)
        => (CornerRadius)element.GetValue(CornerRadiusProperty);
    public static void SetCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);
    #endregion

    #region UnselectedIcon
    public static PackIconMaterialKind GetUnselectedIcon(DependencyObject element)
        => (PackIconMaterialKind)element.GetValue(UnselectedIconProperty);
    public static void SetUnselectedIcon(DependencyObject element, PackIconMaterialKind value)
        => element.SetValue(UnselectedIconProperty, value);

    public static readonly DependencyProperty UnselectedIconProperty =
        DependencyProperty.RegisterAttached("UnselectedIcon", typeof(PackIconMaterialKind), typeof(NavigationDrawerAssist), new PropertyMetadata(PackIconMaterialKind.None));
    #endregion

    #region SelectedIcon
    public static PackIconMaterialKind GetSelectedIcon(DependencyObject element)
        => (PackIconMaterialKind)element.GetValue(SelectedIconProperty);
    public static void SetSelectedIcon(DependencyObject element, PackIconMaterialKind value)
        => element.SetValue(SelectedIconProperty, value);

    public static readonly DependencyProperty SelectedIconProperty =
        DependencyProperty.RegisterAttached("SelectedIcon", typeof(PackIconMaterialKind), typeof(NavigationDrawerAssist), new PropertyMetadata(PackIconMaterialKind.None));
    #endregion

    #region IconSize
    public static int GetIconSize(DependencyObject element)
        => (int)element.GetValue(IconSizeProperty);
    public static void SetIconSize(DependencyObject element, int value)
        => element.SetValue(IconSizeProperty, value);

    public static readonly DependencyProperty IconSizeProperty =
        DependencyProperty.RegisterAttached("IconSize", typeof(int), typeof(NavigationDrawerAssist), new PropertyMetadata(24));
    #endregion
}
