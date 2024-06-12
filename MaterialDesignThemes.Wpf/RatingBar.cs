using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

/// <summary>
/// A custom control implementing a rating bar.
/// The icon aka content may be set as a DataTemplate via the ButtonContentTemplate property.
/// </summary>
public class RatingBar : Control
{
    public static readonly RoutedCommand SelectRatingCommand = new();

    public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
        nameof(Min), typeof(int), typeof(RatingBar), new PropertyMetadata(1, MinPropertyChangedCallback, MinPropertyCoerceValueCallback));

    public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
        nameof(Max), typeof(int), typeof(RatingBar), new PropertyMetadata(5, MaxPropertyChangedCallback, MaxPropertyCoerceValueCallback));

    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value), typeof(double), typeof(RatingBar), new PropertyMetadata(0.0, ValuePropertyChangedCallback));

    public static readonly DependencyProperty ValueItemContainerButtonStyleProperty = DependencyProperty.Register(
        nameof(ValueItemContainerButtonStyle), typeof(Style), typeof(RatingBar), new PropertyMetadata(default(Style)));

    public static readonly DependencyProperty ValueItemTemplateProperty = DependencyProperty.Register(
        nameof(ValueItemTemplate), typeof(DataTemplate), typeof(RatingBar), new PropertyMetadata(default(DataTemplate)));

    public static readonly DependencyProperty ValueItemTemplateSelectorProperty = DependencyProperty.Register(
        nameof(ValueItemTemplateSelector), typeof(DataTemplateSelector), typeof(RatingBar), new PropertyMetadata(default(DataTemplateSelector)));

    public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
        nameof(IsReadOnly), typeof(bool), typeof(RatingBar), new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty AllowDeselectProperty = DependencyProperty.Register(
        nameof(AllowDeselect), typeof(bool), typeof(RatingBar), new PropertyMetadata(default(bool)));

    public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
        nameof(Value),
        RoutingStrategy.Bubble,
        typeof(RoutedPropertyChangedEventHandler<double>),
        typeof(RatingBar));

    private readonly ObservableCollection<RatingBarButton> _ratingButtonsInternal = [];

    private bool _hasAppliedTemplate;

    static RatingBar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(RatingBar), new FrameworkPropertyMetadata(typeof(RatingBar)));
    }

    public RatingBar()
    {
        CommandBindings.Add(new CommandBinding(SelectRatingCommand, SelectItemHandler));
        RatingButtons = new ReadOnlyObservableCollection<RatingBarButton>(_ratingButtonsInternal);
    }

    public event RoutedPropertyChangedEventHandler<double> ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }

    public int Min
    {
        get => (int)GetValue(MinProperty);
        set => SetValue(MinProperty, value);
    }

    public int Max
    {
        get => (int)GetValue(MaxProperty);
        set => SetValue(MaxProperty, value);
    }

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public ReadOnlyObservableCollection<RatingBarButton> RatingButtons { get; }

    public Style? ValueItemContainerButtonStyle
    {
        get => (Style?)GetValue(ValueItemContainerButtonStyleProperty);
        set => SetValue(ValueItemContainerButtonStyleProperty, value);
    }

    public DataTemplate? ValueItemTemplate
    {
        get => (DataTemplate?)GetValue(ValueItemTemplateProperty);
        set => SetValue(ValueItemTemplateProperty, value);
    }

    public DataTemplateSelector? ValueItemTemplateSelector
    {
        get => (DataTemplateSelector?)GetValue(ValueItemTemplateSelectorProperty);
        set => SetValue(ValueItemTemplateSelectorProperty, value);
    }

    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    public bool AllowDeselect
    {
        get => (bool)GetValue(AllowDeselectProperty);
        set => SetValue(AllowDeselectProperty, value);
    }

    public override void OnApplyTemplate()
    {
        _hasAppliedTemplate = true;

        RebuildButtons();

        base.OnApplyTemplate();
    }

    private static void MinPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var ratingBar = (RatingBar)dependencyObject;
        ratingBar.CoerceValue(ValueProperty);
        ratingBar.RebuildButtons();
    }

    private static object MinPropertyCoerceValueCallback(DependencyObject d, object baseValue)
    {
        var ratingBar = (RatingBar)d;
        return Math.Min((int)baseValue, ratingBar.Max);
    }

    private static void MaxPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var ratingBar = (RatingBar)dependencyObject;
        ratingBar.CoerceValue(ValueProperty);
        ratingBar.RebuildButtons();
    }

    private static object MaxPropertyCoerceValueCallback(DependencyObject d, object baseValue)
    {
        var ratingBar = (RatingBar)d;
        return Math.Max((int)baseValue, ratingBar.Min);
    }

    private static void ValuePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var ratingBar = (RatingBar)dependencyObject;
        OnValueChanged(ratingBar, e);
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var instance = (RatingBar)d;
        var args = new RoutedPropertyChangedEventArgs<double>((double)e.OldValue, (double)e.NewValue) { RoutedEvent = ValueChangedEvent };
        instance.RaiseEvent(args);
    }

    private void SelectItemHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
    {
        if (executedRoutedEventArgs.Parameter is int parameter && !IsReadOnly)
        {
            int value = (int)Value;
            Value = value == parameter && AllowDeselect ? 0 : parameter;
        }
    }

    private void RebuildButtons()
    {
        if (!_hasAppliedTemplate)
        {
            return;
        }

        _ratingButtonsInternal.Clear();

        int start = Min;
        if (start is 0)
        {
            start = 1;
        }

        int max = Max;
        var valueItemTemplate = ValueItemTemplate;
        var valueItemTemplateSelector = ValueItemTemplateSelector;
        var valueItemContainerButtonStyle = ValueItemContainerButtonStyle;

        for (int i = start; i <= max; i++)
        {
            var ratingBarButton = new RatingBarButton
            {
                Content = i,
                ContentTemplate = valueItemTemplate,
                ContentTemplateSelector = valueItemTemplateSelector,
                Style = valueItemContainerButtonStyle,
                Value = i,
            };
            _ratingButtonsInternal.Add(ratingBarButton);
        }
    }

    internal class TextBlockForegroundConverter : IMultiValueConverter
    {
        private const byte SemiTransparentAlpha = 0x42; // ~26% opacity

        public static TextBlockForegroundConverter Instance { get; } = new();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values is [SolidColorBrush brush, double value, int buttonValue])
            {
                if (value >= buttonValue)
                {
                    return brush;
                }

                var originalColor = brush.Color;
                var semiTransparentColor = Color.FromArgb(SemiTransparentAlpha, originalColor.R, originalColor.G, originalColor.B);

                var semiTransparentBrush = new SolidColorBrush(semiTransparentColor);
                if (semiTransparentBrush.CanFreeze)
                {
                    semiTransparentBrush.Freeze();
                }

                return semiTransparentBrush;
            }

            // This should never happen (returning actual brush to avoid the compilers squiggly line warning)
            return Brushes.Transparent;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
