using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PolynomialLib
{
    /// <summary>
    /// Class for work with polynomial members
    /// </summary>
    public class Polynomial
    {
        private double[] coefficients;

        /// <summary>
        /// Constructor for creating instance of polynomial member
        /// </summary>
        /// <param name="coefficients">Coefficients of variables starts from younger (x^0) by queue</param>
        public Polynomial(params double[] coefficients)
        {
            Coefficients = coefficients;
        }

        /// <summary>
        /// Coefficients of polynomial member
        /// <exception cref="NullReferenceException">Thrown when value are set like a null</exception>
        /// </summary>
        public double[] Coefficients
        {
            get { return coefficients; }
            set { coefficients = value ?? throw new NullReferenceException("Values mustn't be null"); }
        }

        /// <summary>
        /// Length of polynomial member
        /// </summary>
        public int GetLength
        {
            get { return coefficients.Length; }
            private set { }
        }

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

        public static Polynomial operator +(Polynomial first, Polynomial second)
        {
            int resultLength = Math.Max(first.GetLength, second.GetLength);
            double[] result = new double[resultLength];
            for (int i = 0; i < result.Length; i++)
            {
                if (i < Math.Min(first.GetLength, second.GetLength))
                {
                    result[i] = first[i] + second[i];
                }
                else
                {
                    if (first.GetLength > second.GetLength)
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
            int resultLength = Math.Max(first.GetLength, second.GetLength);
            double[] result = new double[resultLength];
            for (int i = 0; i < result.Length; i++)
            {
                if (i < Math.Min(first.GetLength, second.GetLength))
                {
                    result[i] = first[i] - second[i];
                }
                else
                {
                    if (first.GetLength > second.GetLength)
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

        public static Polynomial operator *(Polynomial first, Polynomial second)
        {
            int resultLength = Math.Max(first.GetLength, second.GetLength);
            double[] result = new double[resultLength];
            for (int i = 0; i < result.Length; i++)
            {
                if (i < Math.Min(first.GetLength, second.GetLength))
                {
                    result[i] = first[i] * second[i];
                }
                else
                {
                    if (first.GetLength > second.GetLength)
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
            double[] result = new double[first.GetLength];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = first[i] * number;
            }

            return new Polynomial(result);
        }

        public static Polynomial operator *(int number, Polynomial first)
        {
            double[] result = new double[first.GetLength];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = first[i] * number;
            }

            return new Polynomial(result);
        }

        public static bool operator ==(Polynomial first, Polynomial second)
        {
            int resultLength = Math.Max(first.GetLength, second.GetLength);
            bool result = true;
            for (int i = 0; i < first.GetLength; i++)
            {
                if (first.GetLength != second.GetLength)
                {
                    result = false;
                    break;
                }

                if (Math.Abs(first[i] - second[i]) > double.Epsilon)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public static bool operator !=(Polynomial first, Polynomial second)
        {
            int resultLength = Math.Max(first.GetLength, second.GetLength);
            bool result = false;
            for (int i = 0; i < first.GetLength; i++)
            {
                if (first.GetLength != second.GetLength)
                {
                    result = true;
                    break;
                }

                if (Math.Abs(first[i] - second[i]) > double.Epsilon)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Polynomial temp = obj as Polynomial;
            bool result = true;
            for (int i = 0; i < this.GetLength; i++)
            {
                if (this.GetLength != temp.GetLength)
                {
                    result = false;
                    break;
                }

                if (Math.Abs(this[i] - temp[i]) > double.Epsilon)
                {
                    result = false;
                    break;
                }
            }

            return result;
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
    }
}