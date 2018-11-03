using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PolynomialLib
{
    /// <summary>
    /// Class for work with polynomial members
    /// </summary>
    public sealed class Polynomial : ICloneable, IEquatable<Polynomial>
    {
        #region fields
        private static readonly double epsilon;

        private double[] coefficients;
        private int degree;
        #endregion

        #region constructors
        static Polynomial()
        {
            try
            {
                epsilon = double.Parse(System.Configuration.ConfigurationManager.AppSettings["epsilon"]);
            }
            catch (Exception)
            {
                epsilon = 0.0000001;
            }
        }

        /// <summary>
        /// Constructor for creating instance of polynomial member
        /// </summary>
        /// <param name="coefficients">Coefficients of variables starts from younger (x^0) by queue</param>
        public Polynomial(params double[] array)
        {
            if (ReferenceEquals(array, null))
            {
                throw new ArgumentNullException($"{nameof(coefficients)} haves null value");
            }

            if (array.Length < 1)
            {
                throw new ArgumentException($"{nameof(coefficients)} is empty");
            }

            coefficients = new double[array.Length];
            array.CopyTo(coefficients, 0);
            this.degree = coefficients.Length;
        }
        #endregion

        #region properties
        /// <summary>
        /// Coefficients of polynomial member
        /// <exception cref="NullReferenceException">Thrown when value are set like a null</exception>
        /// </summary>
        public double[] Coefficients
        {
            get { return coefficients; }
            private set { coefficients = value; }
        }

        /// <summary>
        /// Length of polynomial member
        /// </summary>
        public int Degree
        {
            get { return degree; }
            private set { }
        }
        #endregion

        #region indexator
        /// <summary>
        /// Returns coefficients by chosen index
        /// </summary>
        /// <param name="index">index for finding coefficient</param>
        /// <returns>Returns coefficient by chosen index</returns>
        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= coefficients.Length)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(index)} haves incorrect value");
                }

                return coefficients[index];
            }
        }
        #endregion

        #region operators
        public static Polynomial operator +(Polynomial first, Polynomial second)
        {
            if (ReferenceEquals(first, null))
            {
                throw new ArgumentNullException("First polynom haves null value");
            }

            if (ReferenceEquals(second, null))
            {
                throw new ArgumentNullException("Second polynom haves null value");
            }

            int resultLength = Math.Max(first.Degree, second.Degree);
            double[] result = new double[resultLength];
            for (int i = 0; i < result.Length; i++)
            {
                if (i < Math.Min(first.Degree, second.Degree))
                {
                    result[i] = first[i] + second[i];
                }
                else
                {
                    if (first.Degree > second.Degree)
                    {
                        result[i] = first[i];
                    }
                    else
                    {
                        result[i] = second[i];
                    }
                }
            }

            return new Polynomial(result);
        }

        public static Polynomial operator -(Polynomial first, Polynomial second)
        {
            if (ReferenceEquals(first, null))
            {
                throw new ArgumentNullException("First polynom haves null value");
            }

            if (ReferenceEquals(second, null))
            {
                throw new ArgumentNullException("Second polynom haves null value");
            }

            int resultLength = Math.Max(first.Degree, second.Degree);
            double[] result = new double[resultLength];
            for (int i = 0; i < result.Length; i++)
            {
                if (i < Math.Min(first.Degree, second.Degree))
                {
                    result[i] = first[i] - second[i];
                }
                else
                {
                    if (first.Degree > second.Degree)
                    {
                        result[i] = first[i];
                    }
                    else
                    {
                        result[i] = -second[i];
                    }
                }
            }

            return new Polynomial(result);
        }

        public static Polynomial operator -(Polynomial first)
        {
            if (ReferenceEquals(first, null))
            {
                throw new ArgumentNullException("Polynom haves null value");
            }

            double[] result = new double[first.Degree];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = -first[i];
            }

            return new Polynomial(result);
        }

        public static Polynomial operator *(Polynomial first, Polynomial second)
        {
            if (ReferenceEquals(first, null))
            {
                throw new ArgumentNullException("First polynom haves null value");
            }

            if (ReferenceEquals(second, null))
            {
                throw new ArgumentNullException("Second polynom haves null value");
            }

            int resultLength = Math.Max(first.Degree, second.Degree);
            double[] result = new double[resultLength];
            for (int i = 0; i < result.Length; i++)
            {
                if (i < Math.Min(first.Degree, second.Degree))
                {
                    result[i] = first[i] * second[i];
                }
                else
                {
                    if (first.Degree > second.Degree)
                    {
                        result[i] = first[i];
                    }
                    else
                    {
                        result[i] = second[i];
                    }
                }
            }

            return new Polynomial(result);
        }

        public static Polynomial operator *(Polynomial first, int number)
        {
            if (ReferenceEquals(first, null))
            {
                throw new ArgumentNullException("First polynom haves null value");
            }

            double[] result = new double[first.Degree];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = first[i] * number;
            }

            return new Polynomial(result);
        }

        public static Polynomial operator *(int number, Polynomial first)
        {
            if (ReferenceEquals(first, null))
            {
                throw new ArgumentNullException("First polynom haves null value");
            }

            double[] result = new double[first.Degree];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = first[i] * number;
            }

            return new Polynomial(result);
        }

        public static bool operator ==(Polynomial first, Polynomial second)
        {
            if (ReferenceEquals(first, null))
            {
                throw new ArgumentNullException("First polynom haves null value");
            }

            if (ReferenceEquals(second, null))
            {
                throw new ArgumentNullException("Second polynom haves null value");
            }

            return first.Equals(second);
        }

        public static bool operator !=(Polynomial first, Polynomial second)
        {
            if (ReferenceEquals(first, null))
            {
                throw new ArgumentNullException("First polynom haves null value");
            }

            if (ReferenceEquals(second, null))
            {
                throw new ArgumentNullException("Second polynom haves null value");
            }

            return !first.Equals(second);
        }
        #endregion

        #region overrided special methods
        public static Polynomial Plus(Polynomial firstPolynom)
            => firstPolynom;

        public static Polynomial Negate(Polynomial firstPolynom)
            => -firstPolynom;

        public static Polynomial Add(Polynomial firstPolynom, Polynomial secondPolynom)
            => firstPolynom + secondPolynom;

        public static Polynomial Substract(Polynomial firstPolynom, Polynomial secondPolynom)
            => firstPolynom - secondPolynom;

        public static Polynomial Multiply(Polynomial firstPolynom, Polynomial secondPolynom)
            => firstPolynom * secondPolynom;
        #endregion

        #region overrided methods
        /// <summary>
        /// Compare current polynomial with inserted polynomial
        /// </summary>
        /// <param name="other">inserted object to compare with current</param>
        /// <returns>true if coefficients are equal</returns>
        public override bool Equals(object other)
        {
            if (other.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((Polynomial)other);
        }

        public override int GetHashCode()
        {
            int hashCode = coefficients[0].GetHashCode();
            for (int i = 1; i < coefficients.Length - 1; i++)
            {
                hashCode ^= coefficients[i].GetHashCode();
            }

            return hashCode | ~coefficients[coefficients.Length - 1].GetHashCode();
        }

        /// <summary>
        /// String representation of polynomial instance
        /// </summary>
        /// <returns>polynomial like a string</returns>
        public override string ToString()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-Us");
            StringBuilder resultString = new StringBuilder();
            for (int i = 0; i < coefficients.Length; i++)
            {
                if (coefficients[i] >= 0 && i != 0)
                {
                    resultString.AppendFormat($"+{coefficients[i]}*X^{i}");
                }
                else if (coefficients[i] >= 0 && i != 0)
                {
                    resultString.AppendFormat($"{coefficients[i]}*X^{i}");
                }
                else
                {
                    resultString.AppendFormat($"{coefficients[i]}*X^{i}");
                }
            }

            return resultString.ToString();
        }
        #endregion

        #region implemented method IEqutable
        /// <summary>
        /// Compare current polynomial with inserted polynomial
        /// </summary>
        /// <param name="other">inserted polynomial to compare with current</param>
        /// <returns>true if coefficients are equal</returns>
        public bool Equals(Polynomial other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }

            bool result = true;
            for (int i = 0; i < this.Degree; i++)
            {
                if (this.Degree != other.Degree)
                {
                    result = false;
                    break;
                }

                if (Math.Abs(this[i] - other[i]) > epsilon)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
        #endregion

        #region implemented method ICloneable
        public object Clone()
        {
            return new Polynomial(Coefficients);
        }
        #endregion
    }
}