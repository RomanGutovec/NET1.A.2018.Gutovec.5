using System;
using NUnit.Framework;

namespace PolinomialLib.Tests
{
    [TestFixture]
    public class PolinomialTest
    {
        [TestCase(new double[] { 5.0 }, ExpectedResult = 1)]
        [TestCase(new double[] { 0, 0 }, ExpectedResult = 2)]
        [TestCase(new double[] { 2.5 }, ExpectedResult = 1)]
        [TestCase(new double[] { 3.875, -15.358 }, ExpectedResult = 2)]
        [TestCase(new double[] { 2.5, 4.5, 7.5 }, ExpectedResult = 3)]
        public int LengthPropertyTest(double[] coefficients)
        {
            return new Polinomial(coefficients).GetLength;
        }

        [Test]
        public void ConstructorTest()
        => Assert.Throws<NullReferenceException>(() => new Polinomial(null));

        [TestCase(new double[] { 1.3569 }, 0, ExpectedResult = 1.3569)]
        [TestCase(new double[] { 2.84, 3.15 }, 1, ExpectedResult = 3.15)]
        [TestCase(new double[] { 7.86, 15.789, -98.352 }, 2, ExpectedResult = -98.352)]
        [TestCase(new double[] { -17.895, 28.675, 3, 7 }, 3, ExpectedResult = 7)]
        [TestCase(new double[] { -2.5, 4.5, 7.5, -8 }, 0, ExpectedResult = -2.5)]
        public double IndexerTest_ReturnsValueOfCoefficientByIndex(double[] coefficients, int index)
        {
            Polinomial testPolinomial = new Polinomial(coefficients);
            return testPolinomial[index];
        }

        [TestCase(new double[] { 1.3569 }, 2)]
        [TestCase(new double[] { 2.84, 3.15 }, -1)]
        [TestCase(new double[] { 7.86, 15.789, -98.352 }, 5)]
        [TestCase(new double[] { -17.895, 28.675, 3.5, 7.0 }, -23)]
        public void IndexerTest_InvalidIndex_ArgumentOutOfRangeException(double[] coefficients, int index)
        {
            Polinomial testPolinomial = new Polinomial(coefficients);
            Assert.Throws<ArgumentOutOfRangeException>(() => testPolinomial[index].GetType());
        }

        [TestCase(new double[] { 1.3 }, new double[] { 1.3 })]
        [TestCase(new double[] { 1, 3 }, new double[] { 1 })]
        [TestCase(new double[] { 7, -2, 3.4 }, new double[] { 9, 12 })]
        [TestCase(new double[] { -5, -8, -6 }, new double[] { -5, -2 })]
        [TestCase(new double[] { -3, 4, 0, 15 }, new double[] { 1, 0, 0 })]
        public void OperatorPlus_ReturnsValueOfCoefficientByIndex(double[] coefficientsFirst, double[] coefficientsSecond)
        {
            Polinomial testPolinomialFirst = new Polinomial(coefficientsFirst);
            Polinomial testPolinomialSecond = new Polinomial(coefficientsSecond);
            Polinomial actual = new Polinomial();
            actual = testPolinomialFirst + testPolinomialSecond;

            double[] expected = new double[testPolinomialFirst.GetLength];
            for (int i = 0; i < testPolinomialSecond.GetLength; i++)
            {
                expected[i] = testPolinomialFirst[i] + testPolinomialSecond[i];
            }

            for (int i = testPolinomialSecond.GetLength; i < testPolinomialFirst.GetLength; i++)
            {
                expected[i] = testPolinomialFirst[i];
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
            Polinomial testPolinomialFirst = new Polinomial(coefficientsFirst);
            Polinomial testPolinomialSecond = new Polinomial(coefficientsSecond);
            Polinomial actual = new Polinomial();
            actual = testPolinomialFirst - testPolinomialSecond;

            double[] expected = new double[testPolinomialFirst.GetLength];
            for (int i = 0; i < testPolinomialSecond.GetLength; i++)
            {
                expected[i] = testPolinomialFirst[i] - testPolinomialSecond[i];
            }

            for (int i = testPolinomialSecond.GetLength; i < testPolinomialFirst.GetLength; i++)
            {
                expected[i] = testPolinomialFirst[i];
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
            Polinomial testPolinomialFirst = new Polinomial(coefficientsFirst);
            Polinomial testPolinomialSecond = new Polinomial(coefficientsSecond);
            Polinomial actual = new Polinomial();
            actual = testPolinomialFirst * testPolinomialSecond;

            double[] expected = new double[testPolinomialFirst.GetLength];
            for (int i = 0; i < testPolinomialSecond.GetLength; i++)
            {
                expected[i] = testPolinomialFirst[i] * testPolinomialSecond[i];
            }

            for (int i = testPolinomialSecond.GetLength; i < testPolinomialFirst.GetLength; i++)
            {
                expected[i] = testPolinomialFirst[i];
            }

            CollectionAssert.AreEqual(expected, actual.Coefficients);
        }

        [TestCase(new double[] { 3.1, 1.3, 2.2 }, 2)]
        [TestCase(new double[] { 1.1, 4.2 }, 3)]
        [TestCase(new double[] { 2.5, -3, 4.1 }, -2)]
        public void OperatorMultiplyWithNumber_ReturnsValueOfCoefficientByIndex(double[] coefficientsFirst, int number)
        {
            Polinomial testPolinomial = new Polinomial(coefficientsFirst);

            Polinomial actual = new Polinomial();
            actual = testPolinomial * number;

            double[] expected = new double[testPolinomial.GetLength];
            for (int i = 0; i < testPolinomial.GetLength; i++)
            {
                expected[i] = testPolinomial[i] * number;
            }

            CollectionAssert.AreEqual(expected, actual.Coefficients);
        }

        [TestCase(new double[] { 2.5, 3.6, 4.2 }, new double[] { 2.5, 3.6, 4.2 })]
        [TestCase(new double[] { 1.1, 4.2 }, new double[] { 1.1, 4.2 })]
        [TestCase(new double[] { 2.5, -3, 4.1 }, new double[] { 2.5, -3, 4.1 })]
        public void GetHashCodeOverridedMethod_TwoEqualsArrays_EqualsValue(double[] coefficientsFirst, double[] coefficientsSecond)
        {
            Polinomial testPolinomialFirst = new Polinomial(coefficientsFirst);
            Polinomial testPolinomialSecond = new Polinomial(coefficientsSecond);

            Assert.AreEqual(testPolinomialFirst.GetHashCode(), testPolinomialSecond.GetHashCode());
        }

        [TestCase(new double[] { 7.3, 3.9 }, ExpectedResult = "7.3*X^0+3.9*X^1")]
        [TestCase(new double[] { 1.2, 15.6, -17.8 }, ExpectedResult = "1.2*X^0+15.6*X^1-17.8*X^2")]
        public string ToStringMethodTest(double[] coefficients)
        {
            Polinomial testTosrting = new Polinomial(coefficients);
            return testTosrting.ToString();
        }
    }
}
