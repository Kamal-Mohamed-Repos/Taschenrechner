using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Taschenrechner.Models;

namespace Taschenrechner.Helpers
{
    public class MathExpressionParser
    {
        private readonly CalculatorModel _calculator;

        public MathExpressionParser()
        {
            _calculator = new CalculatorModel();
        }

        public double Parse(string expression)
        {
            // Entfernt alle Leerzeichen
            expression = expression.Replace(" ", "");

            // Erkennt spezielle Funktionen und ersetzt sie
            expression = HandleSpecialFunctions(expression);

            // In einer vollständigen Implementierung würde hier ein Parser-Algorithmus stehen
            // Dies ist eine vereinfachte Version für die Demonstration

            return EvaluateExpression(expression);
        }

        private string HandleSpecialFunctions(string expression)
        {
            // Erkennt und ersetzt Funktionen wie sin, cos, etc. mit ihren Werten
            // Beispiel: Umwandeln von "sin(30)" in den berechneten Wert

            string pattern = @"(sin|cos|tan|sqrt|log10|ln)\(([^()]+)\)";
            Match match = Regex.Match(expression, pattern);

            while (match.Success)
            {
                string function = match.Groups[1].Value;
                string argument = match.Groups[2].Value;

                // Rekursive Auswertung des Arguments, falls es selbst Funktionen enthält
                double argValue = EvaluateExpression(argument);
                double result = 0;

                switch (function)
                {
                    case "sin":
                        result = _calculator.Sin(argValue);
                        break;
                    case "cos":
                        result = _calculator.Cos(argValue);
                        break;
                    case "tan":
                        result = _calculator.Tan(argValue);
                        break;
                    case "sqrt":
                        result = _calculator.SquareRoot(argValue);
                        break;
                    case "log10":
                        result = _calculator.Log10(argValue);
                        break;
                    case "ln":
                        result = _calculator.Ln(argValue);
                        break;
                }

                // Ersetzt den Funktionsaufruf durch das Ergebnis
                expression = expression.Replace(match.Value, result.ToString());
                match = Regex.Match(expression, pattern);
            }

            return expression;
        }

        private double EvaluateExpression(string expression)
        {
            // Hier würde ein einfacher Ausdruck ausgewertet werden
            // In der Praxis würde man einen vollständigen Parsing-Algorithmus verwenden

            try
            {
                return Convert.ToDouble(new System.Data.DataTable().Compute(expression, null));
            }
            catch
            {
                throw new ArgumentException("Ungültiger mathematischer Ausdruck");
            }
        }
    }
}
