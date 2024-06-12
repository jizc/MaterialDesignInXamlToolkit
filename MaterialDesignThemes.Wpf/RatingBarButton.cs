namespace MaterialDesignThemes.Wpf;

public class RatingBarButton : ButtonBase
{
    public static readonly DependencyProperty ValueProperty;

    private static readonly DependencyPropertyKey ValuePropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(Value),
            typeof(int),
            typeof(RatingBarButton),
            new PropertyMetadata(default(int)));

    static RatingBarButton()
    {
        ValueProperty = ValuePropertyKey.DependencyProperty;
        DefaultStyleKeyProperty.OverrideMetadata(typeof(RatingBarButton), new FrameworkPropertyMetadata(typeof(RatingBarButton)));
    }

    public int Value
    {
        get => (int)GetValue(ValueProperty);
        internal set => SetValue(ValuePropertyKey, value);
    }
}
