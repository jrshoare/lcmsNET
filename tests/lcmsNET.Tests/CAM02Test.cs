// Copyright(c) 2019-2021 John Stevenson-Hoare
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class CAM02Test
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void CreateTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            ViewingConditions conditions = new ViewingConditions
            {
                whitePoint = Colorimetric.D50_XYZ,
                Yb = 1.0,
                La = 0.0,
                surround = Surround.Dark,
                D_value = 0.75
            };

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var cam02 = CAM02.Create(context, conditions))
            {
                // Assert
                Assert.IsNotNull(cam02);
            }
        }

        [TestMethod()]
        public void ForwardTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            ViewingConditions conditions = new ViewingConditions
            {
                whitePoint = Colorimetric.D50_XYZ,
                Yb = 1.0,
                La = 0.0,
                surround = Surround.Dark,
                D_value = 0.75
            };
            CIEXYZ xyz = new CIEXYZ { X = 0.8322, Y = 1.0, Z = 0.7765 };

            using (var context = Context.Create(plugin, userData))
            using (var cam02 = CAM02.Create(context, conditions))
            {
                // Act
                cam02.Forward(xyz, out JCh jch);

                // Assert
            }
        }

        [TestMethod()]
        public void ForwardTestDisposed()
        {
            // Arrange
            ViewingConditions conditions = new ViewingConditions
            {
                whitePoint = Colorimetric.D50_XYZ,
                Yb = 1.0,
                La = 0.0,
                surround = Surround.Dark,
                D_value = 0.75
            };
            CIEXYZ xyz = new CIEXYZ { X = 0.8322, Y = 1.0, Z = 0.7765 };

            using (var cam02 = CAM02.Create(null, conditions))
            {
                // Act
                cam02.Dispose();

                // Assert
                Assert.ThrowsException<ObjectDisposedException>(() => cam02.Forward(xyz, out JCh jch));
            }
        }

        [TestMethod()]
        public void ReverseTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            ViewingConditions conditions = new ViewingConditions
            {
                whitePoint = Colorimetric.D50_XYZ,
                Yb = 1.0,
                La = 0.0,
                surround = Surround.Dark,
                D_value = 0.75
            };
            CIEXYZ xyz = new CIEXYZ { X = 0.8322, Y = 1.0, Z = 0.7765 };

            using (var context = Context.Create(plugin, userData))
            using (var cam02 = CAM02.Create(context, conditions))
            {
                cam02.Forward(xyz, out JCh jch);

                // Act
                cam02.Reverse(jch, out CIEXYZ xyz2);

                // Assert
            }
        }

        [TestMethod()]
        public void ReverseTestDisposed()
        {
            // Arrange
            ViewingConditions conditions = new ViewingConditions
            {
                whitePoint = Colorimetric.D50_XYZ,
                Yb = 1.0,
                La = 0.0,
                surround = Surround.Dark,
                D_value = 0.75
            };
            CIEXYZ xyz = new CIEXYZ { X = 0.8322, Y = 1.0, Z = 0.7765 };

            using (var cam02 = CAM02.Create(null, conditions))
            {
                cam02.Forward(xyz, out JCh jch);

                // Act
                cam02.Dispose();

                // Assert
                Assert.ThrowsException<ObjectDisposedException>(() => cam02.Reverse(jch, out CIEXYZ xyz2));
            }
        }
    }
}
