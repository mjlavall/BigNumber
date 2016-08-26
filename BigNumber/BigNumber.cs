using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MikeMath
{
    public class BigNumber : IComparable
    {
        private readonly BigInteger _a;
        private readonly BigInteger _b;
        public BigNumber AdditiveInverse => new BigNumber(-1*_a, _b);
        public BigNumber MultiplicativeInverse => new BigNumber(_b, _a);
        public static readonly BigNumber Zero = new BigNumber(0);
        public static readonly BigNumber One = new BigNumber(1);

        public BigNumber()
        {
            _a = BigInteger.Zero;
            _b = BigInteger.One;
        }

        public BigNumber(BigInteger n) : this()
        {
            _a = n;
        }

        public BigNumber(BigInteger a, BigInteger b) : this()
        {
            var gcd = BigInteger.GreatestCommonDivisor(a, b);
            _a = a/gcd;
            _b = b/gcd;
        }

        public static implicit operator BigNumber(BigInteger n)
        {
            return new BigNumber(n);
        }

        public static BigNumber Parse(string s)
        {
            if(string.IsNullOrEmpty(s)) throw new InvalidDataException("String is null or empty");
            var array = s.Split('/');
            if(array.Length > 2) throw new InvalidDataException("Fraction is not valid, to many '/' characters");
            if(array.Length == 0) throw new InvalidDataException("Invalid fraction");
            BigInteger a;
            if(!BigInteger.TryParse(array[0], out a)) throw new InvalidDataException("Numerator is not a valid integer");
            if (array.Length == 1) return new BigNumber(a);
            BigInteger b;
            if(!BigInteger.TryParse(array[1], out b)) throw new InvalidDataException("Denominator is not a valid integer");
            return new BigNumber(a, b);
        }

        public static bool TryParse(string s, out BigNumber b)
        {
            b = new BigNumber();
            try
            {
                b = Parse(s);
                return true;
            }
            catch (InvalidDataException)
            {
                return false;
            }
        }

        public static implicit operator string(BigNumber n)
        {
            return n._a == 0 ? "0": n._b == 1 ? n._a.ToString() : $"{n._a}/{n._b}";
        }

        public BigNumber Add(BigNumber other)
        {
            var b = _b * other._b;
            var a = _a * other._b + other._a * _b;
            return new BigNumber(a, b);
        }

        public static BigNumber operator +(BigNumber left, BigNumber right)
        {
            return left.Add(right);
        }

        public BigNumber Subtract(BigNumber other)
        {
            return Add(other.AdditiveInverse);
        }

        public static BigNumber operator -(BigNumber left, BigNumber right)
        {
            return left.Subtract(right);
        }

        public BigNumber Multiply(BigNumber other)
        {
            var a = _a * other._a;
            var b = _b * other._b;
            return new BigNumber(a, b);
        }

        public static BigNumber operator *(BigNumber left, BigNumber right)
        {
            return left.Multiply(right);
        }

        public BigNumber Divide(BigNumber other)
        {
            if(other == BigNumber.Zero) throw new DivideByZeroException("Cannont divide by 0");
            return Multiply(other.MultiplicativeInverse);
        }

        public static BigNumber operator /(BigNumber left, BigNumber right)
        {
            return left.Divide(right);
        }

        public int CompareTo(object other)
        {
            if (other == null || typeof(BigNumber) != other.GetType()) return 1;
            var diff = Subtract((BigNumber) other);
            return diff._a > 0 ? 1 : diff._a < 0 ? -1 : 0;
        }

        public static bool operator >(BigNumber left, BigNumber right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <(BigNumber left, BigNumber right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator ==(BigNumber left, BigNumber right)
        {
            return left?.Equals(right) ?? false;
        }

        public static bool operator !=(BigNumber left, BigNumber right)
        {
            return !(left == right);
        }

        public override bool Equals(object other)
        {
            return CompareTo(other) == 0;
        }

        public override int GetHashCode()
        {
            return (_a.GetHashCode() / _b.GetHashCode()).GetHashCode();
        }

    }
}
