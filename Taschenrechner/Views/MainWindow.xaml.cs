using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Taschenrechner.ViewModels;

namespace Taschenrechner;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void InfoButton_Click(object sender, RoutedEventArgs e)
    {
        var aboutWindow = new Views.AboutWindow();
        aboutWindow.Owner = this;
        aboutWindow.ShowDialog();
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        // Beispiel: Zifferntasten und Operatoren abfangen
        if (e.Key >= Key.D0 && e.Key <= Key.D9)
        {
            string digit = (e.Key - Key.D0).ToString();
            (DataContext as MainViewModel)?.OnKeyInput(digit);
        }
        else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
        {
            string digit = (e.Key - Key.NumPad0).ToString();
            (DataContext as MainViewModel)?.OnKeyInput(digit);
        }
        else if (e.Key == Key.Add || e.Key == Key.OemPlus)
        {
            (DataContext as MainViewModel)?.OnKeyInput("+");
        }
        else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
        {
            (DataContext as MainViewModel)?.OnKeyInput("-");
        }
        else if (e.Key == Key.Multiply)
        {
            (DataContext as MainViewModel)?.OnKeyInput("*");
        }
        else if (e.Key == Key.Divide)
        {
            (DataContext as MainViewModel)?.OnKeyInput("/");
        }
        else if (e.Key == Key.Enter || e.Key == Key.Return)
        {
            (DataContext as MainViewModel)?.OnKeyInput("=");
        }
        // Optional: "="-Taste auf dem Nummernblock
        else if (e.Key == Key.OemPlus && (Keyboard.Modifiers & ModifierKeys.Shift) == 0)
        {
            (DataContext as MainViewModel)?.OnKeyInput("=");
        }
        else if (e.Key == Key.Back)
        {
            (DataContext as MainViewModel)?.OnKeyInput("Back");
        }
    }
}