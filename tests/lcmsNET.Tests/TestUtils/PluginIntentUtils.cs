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
using System.Runtime.InteropServices;

namespace lcmsNET.Tests.TestUtils
{
    public static class PluginIntentUtils
    {
        public static PluginIntent CreatePluginIntent() =>
            new()
            {
                Base = Constants.PluginIntent.Base,
                Intent = Constants.PluginIntent.INTENT_DECEPTIVE,
                Link = Marshal.GetFunctionPointerForDelegate(_myIntent),
                Description = new byte[256]
            };

        private static IntPtr MyNewIntent(IntPtr contextID,
            uint nProfiles,
            IntPtr intents,             // uint[]
            IntPtr hProfiles,           // IntPtr[]
            IntPtr BPC,                 // int[]
            IntPtr AdaptationStates,    // double[]
            uint flags)
        {
            Intent[] ICCIntents = new Intent[nProfiles];
            Profile[] profiles = new Profile[nProfiles];
            bool[] bpc = new bool[nProfiles];
            double[] adaptationStates = new double[nProfiles];
            Context context = Context.FromHandle(contextID);

            unsafe
            {
                uint* theIntents = (uint*)intents.ToPointer();
                IntPtr* pProfiles = (IntPtr*)hProfiles.ToPointer();
                int* pBPC = (int*)BPC.ToPointer();
                double* pAdaptationStates = (double*)AdaptationStates.ToPointer();

                for (uint i = 0; i < nProfiles; i++)
                {
                    ICCIntents[i] = (theIntents[i] == Constants.PluginIntent.INTENT_DECEPTIVE)
                            ? Intent.Perceptual : (Intent)theIntents[i];
                    profiles[i] = Profile.FromHandle(pProfiles[i]);
                    bpc[i] = pBPC[i] != 0;
                    adaptationStates[i] = pAdaptationStates[i];
                }

                if (profiles[0].ColorSpace != ColorSpaceSignature.GrayData ||
                    profiles[nProfiles - 1].ColorSpace != ColorSpaceSignature.GrayData)
                {
                    return Pipeline.DefaultICCIntents(context,
                            ICCIntents, profiles, bpc, adaptationStates, (CmsFlags)flags).Handle;
                }

                Pipeline result = Pipeline.Create(context, 1, 1);
                result.Insert(Stage.Create(context, 1), StageLoc.At_Begin);
                return result.Release();
            }
        }

        private static IntentFn _myIntent = new(MyNewIntent);
    }
}
