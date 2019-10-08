using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using PhoneNumbers;

namespace Template.Common.Extensions
{
    public static class StringExtensions
    {
        private const string OMAN_ISO_3166_2 = "OM";

        public static string StripNonNumeric(this string str) => Regex.Replace(str, @"[^\d]", "");

        public static string StripNonAlphaNumeric(this string str) => Regex.Replace(str, @"[^A-Za-z0-9]+", "");

        public static string StripHTMLTags(this string str) => Regex.Replace(str, @"<[^>]*>", "");

        /// <summary>
        ///  In a specified input string, removes all strings that are not arabic characters or numbers.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StripNonArabicNumeric(this string str) => Regex.Replace(str, @"[^ 0-9\p{IsArabic}]+", "");

        public static bool IsValidEmail(this string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                return false;

            var regex = new Regex(@"^(([0-9a-zA-Z][-.\w]*[0-9a-zA-Z])*(\+[0-9a-zA-Z]*)?@([0-9a-zA-Z]*[-\w]*[0-9a-zA-Z]*\.)+([a-zA-Z]{2,9}))$");

            return regex.IsMatch(emailAddress);
        }

        public static bool IsValidPhoneNumber(this string phoneNumber)
        {
            var regex = new Regex(@"\+?\s*([0-9])$");
            if (string.IsNullOrWhiteSpace(phoneNumber) || !regex.IsMatch(phoneNumber))
                return false;

            return true;
        }

        public static bool IsValidOMANPhoneNumber(this string phoneNumber)
        {
            var parsedNumber = ParsePhoneNumber(phoneNumber);

            if (parsedNumber == null) return false;

            var phoneNumberUtil = PhoneNumberUtil.GetInstance();

            return phoneNumberUtil.IsValidNumberForRegion(parsedNumber, OMAN_ISO_3166_2);
        }

        /// <summary>
        /// Parses the entire number and returns only the national number part if it's a valid Oman number. Otherwise returns null.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static string PhoneNumberToOmanNational(this string phoneNumber)
        {
            var parsedNumber = ParsePhoneNumber(phoneNumber);

            if (parsedNumber == null) return null;

            return parsedNumber.NationalNumber.ToString();
        }

        public static string Bracketed(this string s) => $"[{s}]";

        public static bool IsNumeric(this string str) => int.TryParse(str, out _);

        public static bool PasswordMeetsRequirements(this string password) => password.Any(char.IsLetter) && password.Any(char.IsDigit) && password.Length >= 8;

        public static string BracketsTrimmed(this string str) => str.Trim(new char[] { '[', ']' });

        public static string BracketsRemoved(this string str) => str.Replace("[", "").Replace("]", "");

        public static T StringToEnum<T>(this string str)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();

            return (T)Enum.Parse(typeof(T), str);
        }

        public static T StringToFlagEnum<T>(this string str)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();

            var enumStrings = str.Split(',');

            int enumInteger = 0;

            foreach(string enumString in enumStrings)
                enumInteger = enumInteger | (int)Enum.Parse(typeof(T), enumString.Trim());

            return (T)Enum.ToObject(typeof(T), enumInteger);
        }

        public static string FromSnakeCaseToPascalCase(this string s)
        {
            var words = s.Split('_', StringSplitOptions.RemoveEmptyEntries).Select(word => $"{word.Substring(0, 1).ToUpper()}{word.Substring(1).ToLower()}");
            return String.Concat(words);
        }

        public static T GetValueFromDescription<T>(this string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }

                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            // Shouldn't get here
            throw new ArgumentException("Not found.", "description");
        }

        public static string ToPhoneNumber(this string phoneNumber) => $"+{phoneNumber}";

        public static string TrimEnd(this string source, string value) => source.EndsWith(value) ? source.Remove(source.LastIndexOf(value, StringComparison.Ordinal)) : source;

        private static PhoneNumber ParsePhoneNumber(string phoneNumber)
        {
            var isValid = IsValidPhoneNumber(phoneNumber);

            if (!isValid) return null;

            var phoneNumberUtil = PhoneNumberUtil.GetInstance();

            try
            {
                return phoneNumberUtil.Parse(phoneNumber, OMAN_ISO_3166_2);
            }
            catch
            {
                return null;
            }
        }
    }
}
