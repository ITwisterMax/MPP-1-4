using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestGeneratorLibrary.Block;
using TestGeneratorLibrary.Model;

namespace UnitTests
{
    [TestClass]
    public class AnalyzerAndGeneratorTest
    {
        private string Code = @"
            using System.Collections.Generic;

            public class T3
            {
                public IEnumerable<int> Interface { get; private set; }

                public T3(IEnumerable<int> a){}
    
                public void Function1(int b, string c){}

                public void Function2(){}
            }";

        private FileData Data;

        [TestInitialize]
        public void Init()
        {
            Data = Analyzer.GetFileData(Code);
        }

        [TestMethod]
        public void TestFileData()
        {
            Assert.IsNotNull(Data);
            Assert.AreEqual(1, Data.ClassesData.Count);
            
        }

        [TestMethod]
        public void TestClassesData()
        {
            Assert.IsNotNull(Data.ClassesData[0]);
            Assert.AreEqual("T3", Data.ClassesData[0].Name);
            Assert.AreEqual(1, Data.ClassesData[0].ConstructorsData.Count);
            Assert.AreEqual(2, Data.ClassesData[0].MethodsData.Count);
        }

        [TestMethod]
        public void TestConstructorsData()
        {
            Assert.IsNotNull(Data.ClassesData[0].ConstructorsData[0]);
            Assert.AreEqual("T3", Data.ClassesData[0].ConstructorsData[0].Name);
            Assert.AreEqual(1, Data.ClassesData[0].ConstructorsData[0].Parameters.Count);
        }

        [TestMethod]
        public void TestMethodsData()
        {
            Assert.IsNotNull(Data.ClassesData[0].MethodsData[0]);
            Assert.AreEqual("Function1", Data.ClassesData[0].MethodsData[0].Name);
            Assert.AreEqual(2, Data.ClassesData[0].MethodsData[0].Parameters.Count);
            Assert.AreEqual("void", Data.ClassesData[0].MethodsData[0].ReturnType);

            Assert.IsNotNull(Data.ClassesData[0].MethodsData[1]);
            Assert.AreEqual("Function2", Data.ClassesData[0].MethodsData[1].Name);
            Assert.AreEqual(0, Data.ClassesData[0].MethodsData[1].Parameters.Count);
            Assert.AreEqual("void", Data.ClassesData[0].MethodsData[1].ReturnType);
        }
    }
}
