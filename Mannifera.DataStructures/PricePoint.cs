using System.Diagnostics.CodeAnalysis;

namespace Mannifera.DataStructures
{
    public struct PricePoint
    {
        private double _priceLevel;
        private double _quantity;

        public static readonly double Precision = 0.00000001;
        public static readonly int PrecisionDigits = 8;

        public double PriceLevel
        {
            get => _priceLevel;

            init
            {
                if (value >= 0 && !double.IsPositiveInfinity(value))
                {
                    _priceLevel = value;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        public double Quantity
        {
            get => _quantity;

            init
            {
                if (value >= 0 && !double.IsPositiveInfinity(value))
                {
                    _quantity = value;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        public PricePoint(double priceLevel, double quantity) : this()
        {
            PriceLevel = priceLevel;
            Quantity = quantity;
        }

        public static bool operator ==(PricePoint x, PricePoint y)
        {
            return Math.Abs(x.PriceLevel - y.PriceLevel) < Precision
                && Math.Abs(x.Quantity - y.Quantity) < Precision;
        }

        public static bool operator !=(PricePoint x, PricePoint y)
        {
            return Math.Abs(x.PriceLevel - y.PriceLevel) >= Precision
                || Math.Abs(x.Quantity - y.Quantity) >= Precision;
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is PricePoint other
                && this == other;
        }

        public override int GetHashCode() // overriden to match Equals behavior and generate identical hashes for numbers considered the same given the precision
        {
            return HashCode.Combine(Math.Round(PriceLevel, PrecisionDigits), Math.Round(Quantity, PrecisionDigits));
        }
    }
}
