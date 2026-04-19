using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;

namespace Taschenrechner.Views;

public partial class AboutWindow : Window
{
    public AboutWindow()
    {
        InitializeComponent();

        var assembly = Assembly.GetExecutingAssembly();
        var version = assembly.GetName().Version;
        var copyright = assembly
            .GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;

        VersionText.Text = $"Version {version?.Major}.{version?.Minor}.{version?.Build}";
        CopyrightText.Text = copyright ?? "© 2026 Kamal Mohamed";
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
        e.Handled = true;
    }
}