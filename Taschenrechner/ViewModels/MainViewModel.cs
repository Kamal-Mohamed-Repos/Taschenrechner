using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Taschenrechner.Models;
using Taschenrechner.Services;

namespace Taschenrechner.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly CalculatorModel _calculatorModel;
        private readonly CalculationService _calculationService;

        private string _displayText = "0";
        private string _expressionText = "";
        private double _memoryValue = 0;
        private bool _isRadianMode = true;
        private bool _isError = false;
        private string _input = "";
        private readonly char[] _operators = { '+', '-', '*', '/' };
        public MainViewModel()
        {
            _calculatorModel = new CalculatorModel();
            _calculationService = new CalculationService();

            // Commands initialisieren
            DigitCommand = new RelayCommand(OnDigitPressed);
            OperationCommand = new RelayCommand(OnOperationPressed);
            SpecialFunctionCommand = new RelayCommand(OnSpecialFunctionPressed);
            ClearCommand = new RelayCommand(param => Clear());
            ClearEntryCommand = new RelayCommand(param => ClearEntry());
            EqualsCommand = new RelayCommand(param => Calculate());
            BackspaceCommand = new RelayCommand(param => Backspace());
            MemoryStoreCommand = new RelayCommand(param => MemoryStore());
            MemoryRecallCommand = new RelayCommand(param => MemoryRecall());
            MemoryClearCommand = new RelayCommand(param => MemoryClear());
            ToggleAngleModeCommand = new RelayCommand(param => ToggleAngleMode());
        }

        public string DisplayText
        {
            get => _displayText;
            set => SetProperty(ref _displayText, value);
        }

        public string ExpressionText
        {
            get => _expressionText;
            set => SetProperty(ref _expressionText, value);
        }

        public bool IsRadianMode
        {
            get => _isRadianMode;
            set => SetProperty(ref _isRadianMode, value);
        }

        public string AngleModeText => IsRadianMode ? "RAD" : "DEG";

        // Commands
        public ICommand DigitCommand { get; }
        public ICommand OperationCommand { get; }
        public ICommand SpecialFunctionCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand ClearEntryCommand { get; }
        public ICommand EqualsCommand { get; }
        public ICommand BackspaceCommand { get; }
        public ICommand MemoryStoreCommand { get; }
        public ICommand MemoryRecallCommand { get; }
        public ICommand MemoryClearCommand { get; }
        public ICommand ToggleAngleModeCommand { get; }

        private void OnDigitPressed(object parameter)
        {
            if (_isError)
                Clear();

            string digit = parameter.ToString();

            if (DisplayText == "0" && digit != ".")
                DisplayText = digit;
            else if (DisplayText.Contains(".") && digit == ".")
                return; // Verhindert mehrere Dezimalpunkte
            else
                DisplayText += digit;

            ExpressionText += digit;
        }

        private void OnOperationPressed(object parameter)
        {
            if (_isError)
                Clear();

            string operation = parameter.ToString();
            ExpressionText += $" {operation} ";
            DisplayText = "0";
        }

        private void OnSpecialFunctionPressed(object parameter)
        {
            if (_isError)
                Clear();

            string function = parameter.ToString();

            try
            {
                double currentValue = Convert.ToDouble(DisplayText);
                double result = 0;

                switch (function)
                {
                    case "sqrt":
                        result = _calculatorModel.SquareRoot(currentValue);
                        ExpressionText = $"sqrt({currentValue})";
                        break;
                    case "sin":
                        double angleInRad = currentValue;
                        if (!IsRadianMode)
                            angleInRad = currentValue * Math.PI / 180;
                        result = _calculatorModel.Sin(angleInRad);
                        ExpressionText = $"sin({currentValue})";
                        break;
                    case "cos":
                        angleInRad = currentValue;
                        if (!IsRadianMode)
                            angleInRad = currentValue * Math.PI / 180;
                        result = _calculatorModel.Cos(angleInRad);
                        ExpressionText = $"cos({currentValue})";
                        break;
                    case "tan":
                        angleInRad = currentValue;
                        if (!IsRadianMode)
                            angleInRad = currentValue * Math.PI / 180;
                        result = _calculatorModel.Tan(angleInRad);
                        ExpressionText = $"tan({currentValue})";
                        break;
                    case "log10":
                        result = _calculatorModel.Log10(currentValue);
                        ExpressionText = $"log({currentValue})";
                        break;
                    case "ln":
                        result = _calculatorModel.Ln(currentValue);
                        ExpressionText = $"ln({currentValue})";
                        break;
                    case "x²":
                        result = _calculatorModel.Power(currentValue, 2);
                        ExpressionText = $"({currentValue})²";
                        break;
                    case "x³":
                        result = _calculatorModel.Power(currentValue, 3);
                        ExpressionText = $"({currentValue})³";
                        break;
                    case "1/x":
                        result = _calculatorModel.Divide(1, currentValue);
                        ExpressionText = $"1/({currentValue})";
                        break;
                    case "n!":
                        result = _calculatorModel.Factorial(currentValue);
                        ExpressionText = $"({currentValue})!";
                        break;
                    case "%":
                        result = _calculatorModel.Percent(currentValue);
                        ExpressionText = $"{currentValue}%";
                        break;
                }

                DisplayText = result.ToString();
            }
            catch (Exception ex)
            {
                DisplayText = "Fehler";
                ExpressionText = ex.Message;
                _isError = true;
            }
        }

        public void OnKeyInput(string input)
        {
            if (_operators.Contains(input[0]))
            {
                // Prüfen, ob das letzte Zeichen bereits ein Operator ist
                if (_input.Length == 0 || _operators.Contains(_input.Last()))
                    return; // Operator nicht erneut anhängen
            }
            // Hier die Eingabe wie bei Button-Click behandeln
            // Beispiel:
            switch (input)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    // Ziffer verarbeiten
                    DigitCommand.Execute(input);
                    break;
                case "+":
                case "-":
                case "*":
                case "/":
                    // Operator verarbeiten
                    OperationCommand.Execute(input);
                    break;
                case "=":
                    // Berechnung ausführen
                    EqualsCommand.Execute(null);
                    break;
                case "Back":
                    // Letztes Zeichen löschen
                    BackspaceCommand.Execute(null);
                    break;
                // Weitere Fälle nach Bedarf
            }
            _input += input;
        }

        private void Clear()
        {
            DisplayText = "0";
            ExpressionText = "";
            _isError = false;
        }

        private void ClearEntry()
        {
            if (_isError)
                Clear();
            else
                DisplayText = "0";
        }

        private void Calculate()
        {
            if (_isError)
                return;

            try
            {
                double result = _calculationService.EvaluateExpression(ExpressionText);
                DisplayText = result.ToString();
                ExpressionText += " = " + result;
                // Nach dem Berechnen: ExpressionText auf das Ergebnis setzen,
                // damit weitere Operationen mit dem Ergebnis möglich sind
                ExpressionText = result.ToString();
                _input = result.ToString();
            }
            catch (Exception ex)
            {
                DisplayText = "Fehler";
                ExpressionText = ex.Message;
                _isError = true;
            }
        }

        private void Backspace()
        {
            if (_isError)
            {
                Clear();
                return;
            }

            if (DisplayText.Length > 1)
                DisplayText = DisplayText.Substring(0, DisplayText.Length - 1);
            else
                DisplayText = "0";

            if (ExpressionText.Length > 0)
                ExpressionText = ExpressionText.Substring(0, ExpressionText.Length - 1);
        }

        private void MemoryStore()
        {
            if (!_isError)
                _memoryValue = Convert.ToDouble(DisplayText);
        }

        private void MemoryRecall()
        {
            DisplayText = _memoryValue.ToString();
            ExpressionText += _memoryValue.ToString();
        }

        private void MemoryClear()
        {
            _memoryValue = 0;
        }

        private void ToggleAngleMode()
        {
            IsRadianMode = !IsRadianMode;
            OnPropertyChanged(nameof(AngleModeText));
        }
    }
}
