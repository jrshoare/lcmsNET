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
    public static class ProfileUtils
    {
        public static Profile CreateRGBGamma(double gamma)
        {
            CIExyY D65 = new() { x = 0.3127, y = 0.3290, Y = 1.0 };
            CIExyYTRIPLE Rec709Primaries = new()
            {
                Red = new() { x = 0.6400, y = 0.3300, Y = 1.0 },
                Green = new() { x = 0.3000, y = 0.6000, Y = 1.0 },
                Blue = new() { x = 0.1500, y = 0.0600, Y = 1.0 }
            };

            using var toneCurve = ToneCurve.BuildGamma(null, gamma);
            return Profile.CreateRGB(D65, Rec709Primaries, [toneCurve, toneCurve, toneCurve]);
        }
    }
}
