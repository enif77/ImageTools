using System;

namespace ImageTools.Shared
{
    public static class MathExtensions
    {
        // number of digits to be compared    
        public static readonly int PrecisionDigits = 12;
            
        // n+1 because b/a tends to 1 with n leading digits
        public static double Tolerance { get; } = Math.Pow(10, -(PrecisionDigits + 1)); 

        /// <summary>
        /// Compares two double values for equality.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns>True, if both values are equal.</returns>
        public static bool IsEqual(double a, double b)
        {
            // Avoiding division by zero
            if (Math.Abs(a)<= double.Epsilon || Math.Abs(b) <= double.Epsilon)
                return Math.Abs(a - b) <=  double.Epsilon;

            // Comparison
            return Math.Abs(1.0 - a / b) <=  Tolerance;
        }
    }
}