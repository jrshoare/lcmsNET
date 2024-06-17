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
    public static class PluginParametricCurvesUtils
    {
        public static PluginParametricCurves CreatePluginParametricCurves(uint nFunctions) =>
            new()
            {
                Base = Constants.PluginParametricCurves.Base,
                nFunctions = nFunctions,
                FunctionTypes = new uint[PluginParametricCurves.MAX_TYPES_IN_LCMS_PLUGIN],
                ParameterCount = new uint[PluginParametricCurves.MAX_TYPES_IN_LCMS_PLUGIN]
            };

        public static double MyFns(int Type, double[] Params, double R)
        {
            return Type switch
            {
                Constants.PluginParametricCurves.TYPE_SIN => Params[0] * Math.Sin(R * Math.PI),
                -Constants.PluginParametricCurves.TYPE_SIN => Math.Asin(R) / (Math.PI * Params[0]),
                Constants.PluginParametricCurves.TYPE_COS => Params[0] * Math.Cos(R * Math.PI),
                -Constants.PluginParametricCurves.TYPE_COS => Math.Acos(R) / (Math.PI * Params[0]),
                _ => -1.0,
            };
        }

        public static double MyFns2(int Type, double[] Params, double R)
        {
            return Type switch
            {
                Constants.PluginParametricCurves.TYPE_TAN => Params[0] * Math.Tan(R * Math.PI),
                -Constants.PluginParametricCurves.TYPE_TAN => Math.Atan(R) / (Math.PI * Params[0]),
                _ => -1.0,
            };
        }

        public static double Rec709Math(int Type, double[] Params, double R)
        {
            double Fun = 0;

            switch (Type)
            {
                case Constants.PluginParametricCurves.TYPE_709:
                    if (R <= (Params[3] * Params[4])) Fun = R / Params[3];
                    else Fun = Math.Pow(((R - Params[2]) / Params[1]), Params[0]);
                    break;
                case -Constants.PluginParametricCurves.TYPE_709:
                    if (R <= Params[4]) Fun = R * Params[3];
                    else Fun = Params[1] * Math.Pow(R, (1 / Params[0])) + Params[2];
                    break;
            }

            return Fun;
        }
    }
}
