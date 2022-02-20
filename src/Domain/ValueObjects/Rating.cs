using Domain.Common;

namespace Domain.ValueObjects
{
    public class Rating : ValueObject
    {
        public double Average { get; private set; }

        private Rating()
        {
        }

        private Rating(double average)
        {
            if (average < 1.0 || average > 10.0)
                throw new ArgumentOutOfRangeException(nameof(average), "Supply a double between 1.0 and 10.0");

            Average = average;
        }

        public static Rating FromDouble(double average) => new Rating(average);

        public override string ToString() => "Rating: " + Average;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Average;
        }
    }
}