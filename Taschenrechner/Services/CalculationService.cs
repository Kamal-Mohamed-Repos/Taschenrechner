using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taschenrechner.Models;

namespace Taschenrechner.Services
{
    public class CalculationService
    {
        private readonly CalculatorModel _calculator;

        public CalculationService()
        {
            _calculator = new CalculatorModel();
        }

        public double EvaluateExpression(string expression)
        {
            // Ersetzung für trigonometrische und Logarithmus-Funktionen
            expression = ReplaceAdvancedFunctions(expression);

            // Auswertung des Ausdrucks mit DataTable Compute
            try
            {
                object result = new DataTable().Compute(expression, null);
                return Convert.ToDouble(result);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Fehler bei der Auswertung des Ausdrucks: {ex.Message}");
            }
        }

        private string ReplaceAdvancedFunctions(string expression)
        {
            // Diese Methode ersetzt Funktionen wie sin(), cos(), etc. mit deren Ergebnissen
            // Hier ist eine vereinfachte Version - in der Praxis würde ein richtiger Parser benötigt

            // Beispiel: sin(30) durch den berechneten Wert ersetzen
            return expression
                .Replace("sin(", "Math.Sin(")
                .Replace("cos(", "Math.Cos(")
                .Replace("tan(", "Math.Tan(")
                .Replace("sqrt(", "Math.Sqrt(")
                .Replace("log(", "Math.Log10(")
                .Replace("ln(", "Math.Log(");
        }

        // Zusätzliche Methoden für komplexere Auswertungen
        public double CalculateWithMemory(string expression, double memoryValue)
        {
            // Ersetzt "M" oder "Memory" mit dem gespeicherten Wert
            expression = expression.Replace("M", memoryValue.ToString())
                .Replace("Memory", memoryValue.ToString());

            return EvaluateExpression(expression);
        }
    }
}
