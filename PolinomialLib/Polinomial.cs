using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PolinomialLib
{
    public class Polinomial
    {
        private double[] coefficients;

        public Polinomial(params double[] coefficients)
        {
            Coefficients = coefficients;
        }

        public double[] Coefficients
        {
            get { return coefficients; }
            set { coefficients = value ?? throw new NullReferenceException("Values mustn't be null"); }
        }
      
        public int GetLength
        {
            get { return coefficients.Length; }
            private set { }
        }

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

        public static Polinomial operator +(Polinomial first, Polinomial second)
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

            return new Polinomial(result);
        }

        public static Polinomial operator -(Polinomial first, Polinomial second)
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

            return new Polinomial(result);
        }

        public static Polinomial operator *(Polinomial first, Polinomial second)
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

            return new Polinomial(result);
        }

        public static Polinomial operator *(Polinomial first, int number)
        {
            double[] result = new double[first.GetLength];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = first[i] * number;
            }

            return new Polinomial(result);
        }

        public static Polinomial operator *(int number, Polinomial first)
        {
            double[] result = new double[first.GetLength];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = first[i] * number;
            }

            return new Polinomial(result);
        }

        public static bool operator ==(Polinomial first, Polinomial second)
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

                if (first[i] != second[i])
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public static bool operator !=(Polinomial first, Polinomial second)
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

                if (first[i] != second[i])
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

            Polinomial temp = obj as Polinomial;
            bool result = true;
            for (int i = 0; i < this.GetLength; i++)
            {
                if (this.GetLength != temp.GetLength)
                {
                    result = false;
                    break;
                }

                if (this[i] != temp[i])
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