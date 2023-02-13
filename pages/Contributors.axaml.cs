using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Project1.pages;

public partial class Contributors : Window
{
    public Contributors()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}