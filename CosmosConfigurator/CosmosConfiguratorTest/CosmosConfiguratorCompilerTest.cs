﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CosmosConfigurator;
using AppConfigs;

namespace CosmosConfiguratorTest
{
    [TestClass]
    public class CosmosConfiguratorCompilerTest
    {
        [TestMethod]
        public void CompileTestExcel()
        {
            var compiler = new Compiler(
                new CompilerConfig
                {
                    ExportTabExt = ".bytes",
                    ExportCodePath = "../../TabConfigs.cs",
                });
            Assert.IsTrue(compiler.Compile("./test_excel.xls"));
        }

        [TestMethod]
        public void ReadCompliedTsv()
        {
            var tabFile = TabFile.LoadFromFile("./test_excel.bytes");
            Assert.AreEqual<int>(3, tabFile.GetColumnCount());

            var headerNames = tabFile.HeaderNames.ToArray();
            Assert.AreEqual("Id", headerNames[0]);
            Assert.AreEqual("Name", headerNames[1]);
            Assert.AreEqual("StrArray", headerNames[2]);
        }

        [TestMethod]
        public void ReadCompliedTsvWithClass()
        {
            var tabFile = TabFile<TestExcelConfig>.LoadFromFile("./test_excel.bytes");

            var config = tabFile.FindByPrimaryKey(1);

            Assert.IsNotNull(config);
            Assert.AreEqual(config.Name, "Test1");
        }



        class TestWrite : TabRow
        {
            public override bool IsAutoParse
            {
                get { return true; }
            }

            [TabColumn] public string TestColumn1;
            [TabColumn] public int TestColumn2;
        }

        /// <summary>
        /// 测试写入TSV
        /// </summary>
        [TestMethod]
        public void TestWriteTSV()
        {
            var tabFileWrite = new TabFileWriter<TestWrite>();
            var newRow = tabFileWrite.NewRow();
            newRow.TestColumn1 = "Test String";
            newRow.TestColumn2 = 123123;

            tabFileWrite.Save("./test_write_2.bytes");

        }

        /// <summary>
        /// 读入，然后再写入测试
        /// </summary>
        [TestMethod]
        public void TestWriteTSVRead()
        {
            var tabFile = TabFile<TestWrite>.LoadFromFile("./test_write.bytes");

            var tabFileWrite = new TabFileWriter<TestWrite>(tabFile);

            var newRow = tabFileWrite.NewRow();
            newRow.TestColumn1 = Path.GetRandomFileName();
            newRow.TestColumn2 = new Random().Next();

            tabFileWrite.Save("./test_write.bytes");

        }
    }
}
