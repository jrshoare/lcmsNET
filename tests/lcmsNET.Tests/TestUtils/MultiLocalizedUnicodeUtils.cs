// Copyright(c) 2019-2024 John Stevenson-Hoare
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace lcmsNET.Tests.TestUtils
{
    public static class MultiLocalizedUnicodeUtils
    {
        // To ensure compatibility across frameworks without changing project settings,
        // use this simple immutable class instead of a positional record.
        public sealed class DisplayName
        {
            public string Value { get; }
            public string LanguageCode { get; }
            public string CountryCode { get; }

            public DisplayName(string value, string languageCode = "en", string countryCode = "GB")
            {
                Value = value;
                LanguageCode = languageCode;
                CountryCode = countryCode;
            }

            public void Deconstruct(out string value, out string languageCode, out string countryCode)
            {
                value = Value;
                languageCode = LanguageCode;
                countryCode = CountryCode;
            }

            public override string ToString() => Value;
        }

        public static MultiLocalizedUnicode CreateMultiLocalisedUnicode() =>
            MultiLocalizedUnicode.Create(null, 0);

        public static MultiLocalizedUnicode CreateAsWide(DisplayName displayName)
        {
            var mlu = CreateMultiLocalisedUnicode();
            mlu.SetWide(displayName.LanguageCode, displayName.CountryCode, displayName.Value);
            return mlu;
        }

        public static MultiLocalizedUnicode CreateAsASCII(DisplayName displayName)
        {
            var mlu = CreateMultiLocalisedUnicode();
            mlu.SetASCII(displayName.LanguageCode, displayName.CountryCode, displayName.Value);
            return mlu;
        }
    }
}
