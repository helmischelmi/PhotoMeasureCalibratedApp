using Microsoft.Xaml.Behaviors;
using System.Windows.Input;
using System.Windows;

namespace PhotoMeasureCalibrated.View;

public class MouseMoveBehavior : Behavior<UIElement>
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(MouseMoveBehavior), new PropertyMetadata(null));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.MouseMove += OnMouseMove;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.MouseMove -= OnMouseMove;
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        if (Command != null && Command.CanExecute(e))
        {
            Command.Execute(e);
        }
    }
}