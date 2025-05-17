using Taschenrechner.Models;

namespace TaschenrechnerTests
{
    public class Tests
    {
        private CalculatorModel _calculator;
        [SetUp]
        public void Setup()
        {
            _calculator = new CalculatorModel();
        }
        [Test]
        public void Add_TwoNumbers_ReturnsCorrectSum()
        {
            // Arrange
            double a = 5;
            double b = 3;
            double expected = 8;

            // Act
            double result = _calculator.Add(a, b);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Subtract_TwoNumbers_ReturnsCorrectDifference()
        {
            // Arrange
            double a = 5;
            double b = 3;
            double expected = 2;

            // Act
            double result = _calculator.Subtract(a, b);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Multiply_TwoNumbers_ReturnsCorrectProduct()
        {
            // Arrange
            double a = 5;
            double b = 3;
            double expected = 15;

            // Act
            double result = _calculator.Multiply(a, b);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Divide_TwoNumbers_ReturnsCorrectQuotient()
        {
            // Arrange
            double a = 6;
            double b = 3;
            double expected = 2;

            // Act
            double result = _calculator.Divide(a, b);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Divide_ByZero_ThrowsException()
        {
            // Arrange
            double a = 6;
            double b = 0;

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => _calculator.Divide(a, b));
        }

        [Test]
        public void Sin_ValidAngle_ReturnsCorrectValue()
        {
            // Arrange
            double angleInRadians = Math.PI / 2; // 90 degrees
            double expected = 1.0;

            // Act
            double result = _calculator.Sin(angleInRadians);

            // Assert
            Assert.AreEqual(expected, result, 0.0000001); // Using delta for floating point comparison
        }

        [Test]
        public void SquareRoot_NegativeNumber_ThrowsException()
        {
            // Arrange
            double value = -4;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculator.SquareRoot(value));
        }

        [Test]
        public void Log10_ValidValue_ReturnsCorrectValue()
        {
            // Arrange
            double value = 100;
            double expected = 2.0;

            // Act
            double result = _calculator.Log10(value);

            // Assert
            Assert.AreEqual(expected, result, 0.0000001); // Using delta for floating point comparison
        }

        [Test]
        public void Factorial_ValidValue_ReturnsCorrectValue()
        {
            // Arrange
            double value = 5;
            double expected = 120;

            // Act
            double result = _calculator.Factorial(value);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}