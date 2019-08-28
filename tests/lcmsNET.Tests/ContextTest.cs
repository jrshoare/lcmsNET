using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices;
using static lcmsNET.Cms;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class ContextTest
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

        #region From the as yet unwritten plugin package
        private const int PluginMagicNumber = 0x61637070;   // 'acpp'
        private const int PluginTagSig      = 0x74616748;   // 'tagH'

        private const int MAX_TYPES_IN_LCMS_PLUGIN = 20;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate TagTypeSignature DecideType([MarshalAs(UnmanagedType.R8)] double iccVersion, IntPtr userData);

        [StructLayout(LayoutKind.Sequential)]
        public struct TagDescriptor
        {
            [MarshalAs(UnmanagedType.U4)]
            public int ElemCount;
            [MarshalAs(UnmanagedType.U4)]
            public int nSupportedTypes;
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U4, SizeConst = MAX_TYPES_IN_LCMS_PLUGIN)]
            public TagTypeSignature[] SupportedTypes;
            public DecideType Decider;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PluginBase
        {
            [MarshalAs(UnmanagedType.U4)]
            public int Magic;
            [MarshalAs(UnmanagedType.U4)]
            public int ExpectedVersion;
            [MarshalAs(UnmanagedType.U4)]
            public int Type;
            public IntPtr Next;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PluginTag
        {
            public PluginBase Base;
            [MarshalAs(UnmanagedType.U4)]
            public TagSignature Signature;
            public TagDescriptor Descriptor;
        }
        #endregion

        [TestMethod()]
        public void CreateTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            {
                // Assert
                Assert.IsNotNull(context);
            }
        }

        [TestMethod()]
        public void DuplicateTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            {
                using (var duplicate = context.Duplicate(userData))
                {
                    // Assert
                    Assert.IsNotNull(duplicate);
                    Assert.AreNotSame(duplicate, context);
                }
            }
        }

        [TestMethod()]
        public void GetUserData()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            byte[] bytes = new byte[] { 0xff, 0xaa, 0xdd, 0xee };
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            IntPtr expected = handle.AddrOfPinnedObject();
            try
            {
                // Act
                using (var context = Context.Create(plugin, expected))
                {
                    IntPtr actual = context.UserData;

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                handle.Free();
            }
        }

        [TestMethod()]
        public void RegisterPluginsTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            {
                PluginTag tag = new PluginTag
                {
                    Base = new PluginBase
                    {
                        Magic = PluginMagicNumber,
                        ExpectedVersion = Cms.EncodedCMMVersion,
                        Type = PluginTagSig,
                        Next = IntPtr.Zero
                    },
                    Signature = (TagSignature)0x696e6b63,   // 'inkc'
                    Descriptor = new TagDescriptor
                    {
                        ElemCount = 1,
                        nSupportedTypes = 1,
                        SupportedTypes = new TagTypeSignature[MAX_TYPES_IN_LCMS_PLUGIN],
                        Decider = null
                    }
                };

                int rawsize = Marshal.SizeOf(tag);
                IntPtr buffer = Marshal.AllocHGlobal(rawsize);
                Marshal.StructureToPtr(tag, buffer, false);
                try
                {
                    var registered = context.RegisterPlugins(buffer);

                    // Assert
                    Assert.IsTrue(registered);
                }
                finally
                {
                    context.UnregisterPlugins();
                    Marshal.FreeHGlobal(buffer);
                }
            }
        }

        [TestMethod()]
        public void UnregisterPluginsTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            {
                context.UnregisterPlugins();

                // Assert
            }
        }
    }
}
