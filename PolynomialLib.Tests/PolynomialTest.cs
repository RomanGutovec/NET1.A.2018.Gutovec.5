using System;
using NUnit.Framework;

namespace PolynomialLib.Tests
{
    [TestFixture]
    public class PolynomialTest
    {
        [TestCase(new double[] { 5.0 }, ExpectedResult = 1)]
        [TestCase(new double[] { 0, 0 }, ExpectedResult = 2)]
        [TestCase(new double[] { 2.5 }, ExpectedResult = 1)]
        [TestCase(new double[] { 3.875, -15.358 }, ExpectedResult = 2)]
        [TestCase(new double[] { 2.5, 4.5, 7.5 }, ExpectedResult = 3)]
        public int LengthPropertyTest(double[] coefficients)
        {
            return new Polynomial(coefficients).GetLength;
        }

        [Test]
        public void ConstructorTest()
        => Assert.Throws<NullReferenceException>(() => new Polynomial(null));

        [TestCase(new double[] { 1.3569 }, 0, ExpectedResult = 1.3569)]
        [TestCase(new double[] { 2.84, 3.15 }, 1, ExpectedResult = 3.15)]
        [TestCase(new double[] { 7.86, 15.789, -98.352 }, 2, ExpectedResult = -98.352)]
        [TestCase(new double[] { -17.895, 28.675, 3, 7 }, 3, ExpectedResult = 7)]
        [TestCase(new double[] { -2.5, 4.5, 7.5, -8 }, 0, ExpectedResult = -2.5)]
        public double IndexerTest_ReturnsValueOfCoefficientByIndex(double[] coefficients, int index)
        {
            Polynomial testPolynomial = new Polynomial(coefficients);
            return testPolynomial[index];
        }

        [TestCase(new double[] { 1.3569 }, 2)]
        [TestCase(new double[] { 2.84, 3.15 }, -1)]
        [TestCase(new double[] { 7.86, 15.789, -98.352 }, 5)]
        [TestCase(new double[] { -17.895, 28.675, 3.5, 7.0 }, -23)]
        public void IndexerTest_InvalidIndex_ArgumentOutOfRangeException(double[] coefficients, int index)
        {
            Polynomial testPolynomial = new Polynomial(coefficients);
            Assert.Throws<ArgumentOutOfRangeException>(() => testPolynomial[index].GetType());
        }

        [TestCase(new double[] { 1.3 }, new double[] { 1.3 })]
        [TestCase(new double[] { 1, 3 }, new double[] { 1 })]
        [TestCase(new double[] { 7, -2, 3.4 }, new double[] { 9, 12 })]
        [TestCase(new double[] { -5, -8, -6 }, new double[] { -5, -2 })]
        [TestCase(new double[] { -3, 4, 0, 15 }, new double[] { 1, 0, 0 })]
        public void OperatorPlus_ReturnsValueOfCoefficientByIndex(double[] coefficientsFirst, double[] coefficientsSecond)
        {
            Polynomial testPolynomialFirst = new Polynomial(coefficientsFirst);
            Polynomial testPolynomialSecond = new Polynomial(coefficientsSecond);
            Polynomial actual = new Polynomial();
            actual = testPolynomialFirst + testPolynomialSecond;

            double[] expected = new double[testPolynomialFirst.GetLength];
            for (int i = 0; i < testPolynomialSecond.GetLength; i++)
            {
                expected[i] = testPolynomialFirst[i] + testPolynomialSecond[i];
            }

            for (int i = testPolynomialSecond.GetLength; i < testPolynomialFirst.GetLength; i++)
            {
                expected[i] = testPolynomialFirst[i];
            }

            CollectionAssert.AreEqual(expected, actual.Coefficients);
        }

        [TestCase(new double[] { 7.3 }, new double[] { -1.3 })]
        [TestCase(new double[] { 17, 3 }, new double[] { 3.6 })]
        [TestCase(new double[] { 17, -2, 3.4 }, new double[] { 9, 12 })]
        [TestCase(new double[] { -15, -18, -6 }, new double[] { -0.5, -2 })]
        [TestCase(new double[] { -3, 4, 0, 15 }, new double[] { 1.3, 0, 0 })]
        public void OperatorMinus_ReturnsValueOfCoefficientByIndex(double[] coefficientsFirst, double[] coefficientsSecond)
        {
            Polynomial testPolynomialFirst = new Polynomial(coefficientsFirst);
            Polynomial testPolynomialSecond = new Polynomial(coefficientsSecond);
            Polynomial actual = new Polynomial();
            actual = testPolynomialFirst - testPolynomialSecond;

            double[] expected = new double[testPolynomialFirst.GetLength];
            for (int i = 0; i < testPolynomialSecond.GetLength; i++)
            {
                expected[i] = testPolynomialFirst[i] - testPolynomialSecond[i];
            }

            for (int i = testPolynomialSecond.GetLength; i < testPolynomialFirst.GetLength; i++)
            {
                expected[i] = testPolynomialFirst[i];
            }

            CollectionAssert.AreEqual(expected, actual.Coefficients);
        }

        [TestCase(new double[] { 7.3 }, new double[] { -1.3 })]
        [TestCase(new double[] { 17, 3 }, new double[] { 3.6 })]
        [TestCase(new double[] { 17, -2, 3.4 }, new double[] { 9, 12 })]
        [TestCase(new double[] { -15, -18, -6 }, new double[] { -0.5, -2 })]
        [TestCase(new double[] { -3, 4, 0, 15 }, new double[] { 1.3, 0, 0 })]
        public void OperatorMultiply_ReturnsValueOfCoefficientByIndex(double[] coefficientsFirst, double[] coefficientsSecond)
        {
            Polynomial testPolynomialFirst = new Polynomial(coefficientsFirst);
            Polynomial testPolynomialSecond = new Polynomial(coefficientsSecond);
            Polynomial actual = new Polynomial();
            actual = testPolynomialFirst * testPolynomialSecond;

            double[] expected = new double[testPolynomialFirst.GetLength];
            for (int i = 0; i < testPolynomialSecond.GetLength; i++)
            {
                expected[i] = testPolynomialFirst[i] * testPolynomialSecond[i];
            }

            for (int i = testPolynomialSecond.GetLength; i < testPolynomialFirst.GetLength; i++)
            {
                expected[i] = testPolynomialFirst[i];
            }

            CollectionAssert.AreEqual(expected, actual.Coefficients);
        }

        [TestCase(new double[] { 3.1, 1.3, 2.2 }, 2)]
        [TestCase(new double[] { 1.1, 4.2 }, 3)]
        [TestCase(new double[] { 2.5, -3, 4.1 }, -2)]
        public void OperatorMultiplyWithNumber_ReturnsValueOfCoefficientByIndex(double[] coefficientsFirst, int number)
        {
            Polynomial testPolynomial = new Polynomial(coefficientsFirst);

            Polynomial actual = new Polynomial();
            actual = testPolynomial * number;

            double[] expected = new double[testPolynomial.GetLength];
            for (int i = 0; i < testPolynomial.GetLength; i++)
            {
                expected[i] = testPolynomial[i] * number;
            }

            CollectionAssert.AreEqual(expected, actual.Coefficients);
        }

        [TestCase(new double[] { 2.5, 3.6, 4.2 }, new double[] { 2.5, 3.6, 4.2 })]
        [TestCase(new double[] { 1.1, 4.2 }, new double[] { 1.1, 4.2 })]
        [TestCase(new double[] { 2.5, -3, 4.1 }, new double[] { 2.5, -3, 4.1 })]
        public void GetHashCodeOverridedMethod_TwoEqualsArrays_EqualsValue(double[] coefficientsFirst, double[] coefficientsSecond)
        {
            Polynomial testPolynomialFirst = new Polynomial(coefficientsFirst);
            Polynomial testPolynomialSecond = new Polynomial(coefficientsSecond);

            Assert.AreEqual(testPolynomialFirst.GetHashCode(), testPolynomialSecond.GetHashCode());
        }

        [TestCase(new double[] { 7.3, 3.9 }, ExpectedResult = "7.3*X^0+3.9*X^1")]
        [TestCase(new double[] { 1.2, 15.6, -17.8 }, ExpectedResult = "1.2*X^0+15.6*X^1-17.8*X^2")]
        public string ToStringMethodTest(double[] coefficients)
        {
            Polynomial testTosrting = new Polynomial(coefficients);
            return testTosrting.ToString();
        }

        [TestCase(new double[] { 7.3 }, new double[] { 7.3 }, ExpectedResult = true)]
        [TestCase(new double[] { 17, 3 }, new double[] { 17.3 }, ExpectedResult = false)]
        [TestCase(new double[] { 1.5, 4, 5 }, new double[] { 1.5, 5, 4 }, ExpectedResult = false)]
        [TestCase(new double[] { 17, -2, 3.4 }, new double[] { 17, -2, 3.4 }, ExpectedResult = true)]
        public bool OperatorEquals_ReturnsTrueOrFalse(double[] coefficientsFirst, double[] coefficientsSecond)
        {
            Polynomial testPolynomialFirst = new Polynomial(coefficientsFirst);
            Polynomial testPolynomialSecond = new Polynomial(coefficientsSecond);

            return testPolynomialFirst.Equals(testPolynomialSecond);
        }
    }
}
