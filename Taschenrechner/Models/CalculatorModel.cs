using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taschenrechner.Models
{
 public class CalculatorModel
    {
        // Grundlegende Operationen
        public double Add(double a, double b) => a + b;
        public double Subtract(double a, double b) => a - b;
        public double Multiply(double a, double b) => a * b;

        public double Divide(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Division durch Null ist nicht erlaubt.");
            return a / b;
        }

        // Erweiterte Operationen
        public double Power(double baseNum, double exponent) => Math.Pow(baseNum, exponent);
        public double SquareRoot(double value)
        {
            if (value < 0)
                throw new ArgumentException("Keine reelle Wurzel für negative Zahlen.");
            return Math.Sqrt(value);
        }

        // Trigonometrische Funktionen (im Bogenmaß)
        public double Sin(double angle) => Math.Sin(angle);
        public double Cos(double angle) => Math.Cos(angle);
        public double Tan(double angle)
        {
            if (Math.Cos(angle) == 0)
                throw new ArgumentException("Tangens ist an diesem Punkt nicht definiert.");
            return Math.Tan(angle);
        }

        // Logarithmische Funktionen
        public double Log10(double value)
        {
            if (value <= 0)
                throw new ArgumentException("Logarithmus ist nur für positive Zahlen definiert.");
            return Math.Log10(value);
        }

        public double Ln(double value)
        {
            if (value <= 0)
                throw new ArgumentException("Natürlicher Logarithmus ist nur für positive Zahlen definiert.");
            return Math.Log(value);
        }

        public double Log(double baseNum, double value)
        {
            if (value <= 0 || baseNum <= 0 || baseNum == 1)
                throw new ArgumentException("Logarithmus-Parameter ungültig.");
            return Math.Log(value, baseNum);
        }

        // Weitere nützliche Funktionen
        public double Factorial(double n)
        {
            if (n < 0 || n != Math.Floor(n))
                throw new ArgumentException("Fakultät ist nur für nicht-negative ganze Zahlen definiert.");

            if (n == 0 || n == 1)
                return 1;

            double result = 1;
            for (int i = 2; i <= n; i++)
                result *= i;

            return result;
        }

        public double Percent(double value) => value / 100;
    }
}
