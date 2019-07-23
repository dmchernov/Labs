using System;
using System.Collections.Generic;
using System.Linq;

namespace Izh_08_tasks.Polynomials
{
    public class Polynomial
    {
        private double[] coefficients;
        private uint[] exponents;

        public Polynomial(double[] coefficients, uint[] exponents)
        {
            if (coefficients.Length == 0)
            {
                throw new ArgumentException("Не заданы коэффициенты терм многочлена.");
            }

            if (exponents.Length == 0)
            {
                throw new ArgumentException("Не заданы степени переменных многочлена.");
            }

            if (coefficients.Length != exponents.Length)
            {
                throw new ArgumentException("Количество указанных степеней не соответствует количеству терм в многочлене.");
            }

            this.coefficients = coefficients;
            this.exponents = exponents;

            Array.Sort(this.exponents, this.coefficients);
        }

        public struct PolynomialDivisionResult
        {
            public Polynomial quotient;
            public Polynomial remainder;
        }

        /// <summary>
        /// Степень полинома.
        /// </summary>
        public uint Degree
        {
            get { return this.exponents.Max(); }
        }

        public static Polynomial operator +(Polynomial a, Polynomial b)
        {
            List<double> newCoeff = a.coefficients.ToList();
            newCoeff.AddRange(b.coefficients.ToList());

            List<uint> newExp = a.exponents.ToList();
            newExp.AddRange(b.exponents.ToList());

            return Simplify(new Polynomial(newCoeff.ToArray(), newExp.ToArray()));
        }

        public static Polynomial operator *(Polynomial a, Polynomial b)
        {
            List<double> newCoef = new List<double>();
            List<uint> newExp = new List<uint>();

            for (int i = 0; i < a.coefficients.Length; i++)
            {
                for (int j = 0; j < b.coefficients.Length; j++)
                {
                    newCoef.Add(a.coefficients[i] * b.coefficients[j]);
                    newExp.Add(a.exponents[i] + b.exponents[j]);
                }
            }

            return Simplify(new Polynomial(newCoef.ToArray(), newExp.ToArray()));
        }

        public static Polynomial operator -(Polynomial a, Polynomial b)
        {
            for (int i = b.coefficients.Length - 1; i >= 0; i--)
            {
                b.coefficients[i] *= -1;
            }

            return a + b;
        }

        public static PolynomialDivisionResult operator /(Polynomial a, Polynomial b)
        {
            if (IsZero(b))
            {
                throw new DivideByZeroException();
            }

            PolynomialDivisionResult w = default(PolynomialDivisionResult);

            Polynomial q = new Polynomial(new double[] { 0 }, new uint[] { 0 });
            Polynomial r = a;
            Polynomial t;

            while (!IsZero(r) && (r.Degree >= b.Degree))
            {
                t = new Polynomial(new double[] { (r.coefficients[r.coefficients.Length - 1] / b.coefficients[b.coefficients.Length - 1]) }, new uint[] { (r.exponents[r.exponents.Length - 1] - b.exponents[b.exponents.Length - 1]) });
                q = q + t;
                r = r - (t * b);
            }

            w.quotient = q;
            w.remainder = r;
            return w;
        }

        /// <summary>
        /// Рассчитывает хэш.
        /// </summary>
        /// <returns>Хэш.</returns>
        public override int GetHashCode()
        {
            return coefficients.GetHashCode() ^ exponents.GetHashCode();
        }

        /// <summary>
        /// Метод определяет являются ли полиномы одинаковыми.
        /// </summary>
        /// <param name="obj">Полином для сравнения.</param>
        /// <returns>true -- если полиномы одинаковые, иначе -- false.</returns>
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            Polynomial comparePol = (Polynomial)obj;

            if (this.Degree != comparePol.Degree)
            {
                return false;
            }

            comparePol = Simplify(comparePol);
            Polynomial initialPol = Simplify(this);

            for (int i = 0; i < this.coefficients.Length; i++)
            {
                if ((initialPol.coefficients[i] != comparePol.coefficients[i]) || (initialPol.exponents[i] != comparePol.exponents[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Формирует строковое представление многочлена.
        /// </summary>
        /// <returns>Результирующая строка.</returns>
        public override string ToString()
        {
            string result = string.Empty;

            for (int i = this.coefficients.Length - 1; i >= 0 ; i--)
            {
                if (this.coefficients[i] == 0)
                {
                    continue;
                }

                if (this.coefficients[i] > 0 && result != string.Empty)
                {
                    result += "+";
                }

                if (this.exponents[i] == 0)
                {
                    result += string.Format("{0:F1}", this.coefficients[i]);
                }
                else if (this.exponents[i] == 1)
                {
                    result += string.Format("{0:F1}*x", this.coefficients[i]);
                }
                else
                {
                    result += string.Format("{0:F1}*x^{1}", this.coefficients[i], this.exponents[i].ToString());
                }
            }

            return result;
        }

        /// <summary>
        /// Метод упрощает полином.
        /// </summary>
        /// <param name="polynomial">Исходный полином.</param>
        /// <returns>Упрощённый полином.</returns>
        public static Polynomial Simplify(Polynomial polynomial)
        {
            double[] coef = polynomial.coefficients;
            uint[] exp = polynomial.exponents;
            uint maxExp = exp[exp.Length - 1];
            double tmpCoef = 0.0;
            List<double> newCoef = new List<double>();
            List<uint> newExp = new List<uint>();

            for (int i = exp.Length - 1; i >= 0; i--)
            {
                if (exp[i] < maxExp)
                {
                    if (tmpCoef != 0.0)
                    {
                        newExp.Add(maxExp);
                        newCoef.Add(Math.Round(tmpCoef, 10));
                    }

                    maxExp = exp[i];
                    tmpCoef = 0.0;
                }

                tmpCoef += coef[i];
            }

            newExp.Add(maxExp);
            newCoef.Add(Math.Round(tmpCoef, 10));

            return new Polynomial(newCoef.ToArray(), newExp.ToArray());
        }

        private static bool IsZero(Polynomial p)
        {
            return p.coefficients.Length == 1 && p.coefficients[0] == 0;
        }
    }
}
