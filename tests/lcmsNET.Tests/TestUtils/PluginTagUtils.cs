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

using lcmsNET.Plugin;
using System;

namespace lcmsNET.Tests.TestUtils
{
    public static class PluginTagUtils
    {
        public static PluginTag CreatePluginTag() =>
            new()
            {
                Base = Constants.PluginTag.Base,
                Signature = Constants.PluginTag.Signature,
                Descriptor = Constants.PluginTag.Descriptor
            };

        public static TagTypeSignature Decide(double iccVersion, IntPtr data)
        {
            using var mlu = MultiLocalizedUnicode.FromHandle(data);
            var text = mlu.GetASCII(MultiLocalizedUnicode.NoLanguage, MultiLocalizedUnicode.NoCountry);
            return TagTypeSignature.Text;
        }
    }
}
