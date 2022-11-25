namespace GameLib
{
    /// <summary>
    /// Represents a filling bar meter
    /// </summary>
    public readonly struct Bar
    {
        /// <summary>
        /// The threshold
        /// </summary>
        public readonly int Max;
        /// <summary>
        /// The current accumulated value.
        /// </summary>
        public readonly double Value;
        /// <summary>
        /// Switches to true if the value has trespassed the limit in any time
        /// </summary>
        public readonly bool Valid;
        /// <summary>
        /// The difference between the accumulated value and the maximum value
        /// </summary>
        public double Remaining => Max - Value;
        /// <summary>
        /// The fullness percentage relative to the maximum
        /// </summary>
        public double Percentage => Value / Max;

        private Bar(double value, int max, bool valid)
        {
            this.Value = value;
            this.Max = max;
            this.Valid = valid;
        }
        /// <summary>
        /// Creates a new filling bar with it's value starting at zero.
        /// </summary>
        /// <param name="max">The maximum value</param>
        /// <returns>A new bar with the value set to zero and the maximum set</returns>
        public static Bar Of(int max)
        {
            return new Bar(0, max, true);
        }

        public static Bar operator + (Bar a, double value)
        {
            var result = a.Value + value;
            var valid = a.Valid && (result < a.Max);
            return new Bar(result, a.Max, valid);
        }
        public static Bar operator -(Bar a, double value)
        {
            var result = Math.Max(0, a.Value - value);
            var valid = a.Valid && (result < a.Max);
            return new Bar(result, a.Max, valid);
        }

        public static Bar operator *(Bar a, double value)
        {
            var result = a.Value * value;
            var valid = a.Valid && (result < a.Max);
            return new Bar(result, a.Max, valid);
        }
    }
}