using System.Text;

namespace MMS.Core.Generators
{
    public class StringGenerator
    {
        public static string GenerateConsequentNumbers(int length, string? previousValue = null, string? startsWith = null)
        {
            var builder = new StringBuilder(length);
            var startsWithLength = 0;

            if (!string.IsNullOrEmpty(startsWith))
            {
                builder.Append(startsWith);
                startsWithLength = startsWith.Length;
            }

            if (!string.IsNullOrEmpty(previousValue))
            {
                return Increment(previousValue);
            }

            var numbersCount = length - startsWithLength;
            var zeroesCount = numbersCount - 1;
            var zeroes = new string('0', zeroesCount);

            builder.Append(zeroes);
            builder.Append('1');

            return builder.ToString();
        }

        private static string Increment(in string previousValue)
        {
            var last = previousValue.Last();
            var indexOfLast = previousValue.LastIndexOf(last);
            var previousValueWithoutLastChar = previousValue.Remove(indexOfLast);

            if (!char.IsNumber(last))
            {
                throw new ArgumentException(
                    $"Maximum value of given string has been reached or parameter:" +
                    $" '{nameof(previousValue)}' is not valid!" +
                    $"\nIn order to use increment logic, provided string must end with a number");
            }

            var convertedLast = last - '0';

            if (convertedLast == 9)
            {
                previousValueWithoutLastChar = Increment(previousValueWithoutLastChar);

                convertedLast = 0;
            }
            else
            {
                convertedLast += 1;
            }

            return previousValueWithoutLastChar + convertedLast;
        }
    }
}
