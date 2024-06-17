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
    public static class GamutBoundaryDescriptorUtils
    {
        public static GamutBoundaryDescriptor CreateGamutBoundaryDescriptor() =>
            GamutBoundaryDescriptor.Create(ContextUtils.CreateContext());

        public static void AddPoints(GamutBoundaryDescriptor gbd)
        {
            CIELab add = new();
            for (int L = 0; L <= 100; L += 10)
                for (int a = -128; a <= 128; a += 5)
                    for (int b = -128; b <= 128; b += 5)
                    {
                        add.L = L;
                        add.a = a;
                        add.b = b;
                        gbd.AddPoint(add);
                    }
        }
    }
}
