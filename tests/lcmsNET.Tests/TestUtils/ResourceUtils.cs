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

using System.Reflection;
using System.IO;
using System;

namespace lcmsNET.Tests.TestUtils
{
    public static class ResourceUtils
    {
        public static void Save(string resourceName, string path)
        {
            var thisExe = Assembly.GetExecutingAssembly();
            var assemblyName = new AssemblyName(thisExe.FullName);
            using var s = thisExe.GetManifestResourceStream(assemblyName.Name + resourceName);
            using var fs = File.Create(path);
            s.CopyTo(fs);
        }

        public static MemoryStream Save(string resourceName)
        {
            MemoryStream ms = new();
            var thisExe = Assembly.GetExecutingAssembly();
            var assemblyName = new AssemblyName(thisExe.FullName);
            using (var s = thisExe.GetManifestResourceStream(assemblyName.Name + resourceName))
            {
                s?.CopyTo(ms);
            }
            return ms;
        }

        public static void SaveTemporarily(string resourceName, string fileName, Action<string> action)
        {
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var path = Path.Combine(tempPath, fileName);
                Save(resourceName, path);

                action(path);
            }
            finally
            {
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }
            }
        }
    }
}
