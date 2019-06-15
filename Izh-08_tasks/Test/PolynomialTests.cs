using NUnit.Framework;
using System;
using Izh_08_tasks.Polynomials;

namespace Izh_08_tasks.Test
{
    [TestFixture]
    public class PolynomialTests
    {
        [SetCulture("en-us")]
        [TestCase(new double[] { 2.0, -1.5 }, new uint[] { 1, 0 }, ExpectedResult = "2.0*x-1.5")]
        [TestCase(new double[] { 2.0, -1.5, 6.0 }, new uint[] { 2, 1, 0 }, ExpectedResult = "2.0*x^2-1.5*x+6.0")]
        public string Polynomial_ToString_Test(double[] coef, uint[] exp)
        {
            Polynomial pol = new Polynomial(coef, exp);
            return pol.ToString();
        }

        static object[] EqualTestData = new object[]
        {
            new object[]
            {
            new Polynomial(new double[] { 2.0, -1.5, -6.0 }, new uint[] { 2, 1, 0 }),
            new Polynomial(new double[] { 2.0, -1.5, -6.0 }, new uint[] { 2, 1, 0 }),
            true,
            },
            new object[]
            {
            new Polynomial(new double[] { 2.0, -1.5, 6.0 }, new uint[] { 2, 1, 0 }),
            new Polynomial(new double[] { 3.0, -1.5, -6.0 }, new uint[] { 2, 1, 0 }),
            false,
            },
            new object[]
            {
            new Polynomial(new double[] { 2.0, -1.5, 6.0 }, new uint[] { 2, 1, 0 }),
            new object(),
            false,
            },
        };

        [TestCaseSource("EqualTestData")]
        public void Polynomial_Equal_Test(Polynomial a, object b, bool expectedResult)
        {
            Assert.That(a.Equals(b) == expectedResult);
        }

        static object[] SimplifyTestData = new object[]
            {
                new object[]
                {
                new Polynomial(new double[] { 2.0, 3.0, -1.0, 2.0, -3.0, -1.0 }, new uint[] { 2, 1, 0, 2, 1, 0 }),
                new Polynomial(new double[] { 4.0, -2.0 }, new uint[] { 2, 0 }),
                },
            };

        [TestCaseSource("SimplifyTestData")]
        public void Polynomial_Simplify_Test(Polynomial a, Polynomial b)
        {
            Assert.That(Polynomial.Simplify(a).Equals(b));
        }

        static object[] AdditionTestData = new object[]
            {
                new object[]
                {
                new Polynomial(new double[] { 5.5, 4.0, 2.0 }, new uint[] { 2, 1, 0 }),
                new Polynomial(new double[] { 1.0, -7.0, -3.2 }, new uint[] { 2, 1, 0 }),
                new Polynomial(new double[] { 6.5, -3.0, -1.2 }, new uint[] { 2, 1, 0 }),
                },
            };

        [TestCaseSource("AdditionTestData")]
        public void Polynomial_Addition_Test(Polynomial a, Polynomial b, Polynomial expectedResult)
        {
            Polynomial result = a + b;
            Assert.That(result.Equals(expectedResult));
        }

        static object[] SubtractionTestData = new object[]
    {
                new object[]
                {
                new Polynomial(new double[] { 5.5, 4.0, 2.0 }, new uint[] { 2, 1, 0 }),
                new Polynomial(new double[] { 1.0, -7.0, -3.2 }, new uint[] { 2, 1, 0 }),
                new Polynomial(new double[] { 4.5, 11.0, 5.2 }, new uint[] { 2, 1, 0 }),
                },
    };

        [TestCaseSource("SubtractionTestData")]
        public void Polynomial_Subtraction_Test(Polynomial a, Polynomial b, Polynomial expectedResult)
        {
            Polynomial result = a - b;
            Assert.That(result.Equals(expectedResult));
        }

        static object[] MultiplyTestData = new object[]
            {
                new object[]
                {
                new Polynomial(new double[] { 5.5, 4.0, 2.0 }, new uint[] { 2, 1, 0 }),
                new Polynomial(new double[] { 1.0, -7.0, -3.2 }, new uint[] { 2, 1, 0 }),
                new Polynomial(new double[] { 5.5, -34.5, -43.6, -26.8, -6.4 }, new uint[] { 4, 3, 2, 1, 0 }),
                },
            };

        [TestCaseSource("MultiplyTestData")]
        public void Polynomial_Multiply_Test(Polynomial a, Polynomial b, Polynomial expectedResult)
        {
            Polynomial result = a * b;
            Assert.That(result.Equals(expectedResult));
        }
    }
}
