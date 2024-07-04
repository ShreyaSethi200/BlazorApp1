using System;

namespace BlazorApp.Services
{
    public interface IArithmeticService
    {
        int Evaluate(string expression);
        string ConvertToWords(int number);
    }

    public class ArithmeticService : IArithmeticService
    {
        public int Evaluate(string expression)
        {
            // Split the expression by '+'
            var parts = expression.Split('+');
            // Sum the two parts and return the result
            return int.Parse(parts[0]) + int.Parse(parts[1]);
        }

        public string ConvertToWords(int number)
        {
            // Use the NumberToWordsConverter to convert the number to words
            string words = NumberToWordsConverter.Convert(number);

            // Capitalize the first letter of the words
            return char.ToUpper(words[0]) + words.Substring(1).TrimEnd();
        }
    }

    public static class NumberToWordsConverter
    {
        public static string Convert(int number)
        {
            if (number == 0) return "Zero";

            if (number < 0)
                return "Minus " + Convert(Math.Abs(number));

            string words = "";

            if ((number / 1000) > 0)
            {
                words += Convert(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += Convert(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[]
                {
                    "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
                    "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
                };
                var tensMap = new[]
                {
                    "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
                };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            // Capitalize first letter of the words
            return char.ToUpper(words[0]) + words.Substring(1).TrimEnd();
        }
    }
}
