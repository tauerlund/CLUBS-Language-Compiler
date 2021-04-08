using System;
using System.IO;
using System.Linq;
using System.Text;
using ClubsCompiler.Program;
using Antlr4.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClubsCompiler.Tests {

  [TestClass]
  public class CheckerTestsOLD {
    private Checker _checker;

    // Setup.
    [TestInitialize]
    public void TestInitialize() {
      ErrorLogger errorLogger = new ErrorLogger();
      _checker = new Checker(errorLogger);
    }
  }
}