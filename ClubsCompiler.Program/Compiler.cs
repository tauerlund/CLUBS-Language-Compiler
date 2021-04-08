using Antlr4.Runtime;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents the compiler.
  /// </summary>
  public class Compiler {

    /// <summary>
    /// Compiles the received input to an executable file.
    /// </summary>
    /// <param name="input"></param>
    public void Compile(string input) {
      // CREATE LEXER FROM SOURCE CODE, GET TOKENS.
      // Create char stream from input source code.
      AntlrInputStream inputStream = new AntlrInputStream(input);
      // Create lexer that will use the char stream.
      CLUBSLexer CLUBSLexer = new CLUBSLexer(inputStream);
      // Create token stream using the lexer.
      CommonTokenStream commonTokenStream = new CommonTokenStream(CLUBSLexer);

      // CREATE PARSER.
      ErrorLogger errorLogger = new ErrorLogger();
      // Create parser that will use the tokens.
      CLUBSParser CLUBSParser = new CLUBSParser(commonTokenStream);
      ;
      // Add own error handling.
      CLUBSParser.RemoveErrorListeners();
      CLUBSParser.AddErrorListener(errorLogger);
      Console.WriteLine("\n\nCOMPILER STAGE: Parser generated");
      ;
      // GENERATE CST/Parse tree from tokens.
      // Begin parsing from prog rule (the start rule)
      CLUBSParser.ProgContext cst = CLUBSParser.prog();
      Console.WriteLine("COMPILER STAGE: Parsing done (CST built)");
      // Check any parser errrors. Print these and dont proceed to next stage.
      //PrintErrorsAndExitIfAny(errorLogger); // XXX: Vi kan ikke fortsætte med at compile hvis syntax errors. Er det et problem?

      //ParseTreeWalker walker = new ParseTreeWalker(); // NOTE: Do not remove. Prints the parse tree (ish..)
      //.Walk(new ParseTreeListener(), cst);
      //Console.ReadKey();

      // GENERATE AST.
      ASTNode ast = new BuildASTVisitor().VisitProg(cst);

      ;

      Console.WriteLine("COMPILER STAGE: AST built.");
      ;
      // PRINT AST
      //new PrintVisitor().Visit(ast);
      // DECORATE AST
      Checker checker = new Checker(errorLogger);
      checker.Visit(ast);

      Console.WriteLine("COMPILER STAGE: Checker finished.");

      // SHOW ACE COMPILER ERRORS
      // Print any compiler errors and do not translate to IR (C#).
      PrintErrorsAndExitIfAny(errorLogger);

      string sourceCode = new CodeGenerator().GenerateCode(ast);
      CompileToExe(sourceCode, "AceProgram.exe");

      Console.WriteLine("All done.");
    }

    private static void PrintErrorsAndExitIfAny(ErrorLogger errorLogger) {
      Console.WriteLine("\nCompilation: " + (errorLogger.Errors.Count == 0 ? "SUCCESS!" : "FAILED") + "\n");
      if(errorLogger.Errors.Count > 0) {
        errorLogger.PrintErrors();
        Console.ReadKey();
        Environment.Exit(0);
      }
    }

    // Compiles the intermediate code (C#) to an executable using the C# compiler.
    // Full process is: ACE -> C# -> CIL/exe -> target code.
    private static void CompileToExe(string intermediateCode, string outputName) {
      File.WriteAllText("output.cs", intermediateCode);

      CSharpCodeProvider provider = new CSharpCodeProvider();
      CompilerParameters parameters = new CompilerParameters {
        GenerateExecutable = true,
        GenerateInMemory = false,
        OutputAssembly = outputName,
        TreatWarningsAsErrors = false
      };

      parameters.ReferencedAssemblies.Add("System.Core.dll"); // Add .dll (To support Linq)

      CompilerResults results = provider.CompileAssemblyFromSource(parameters, intermediateCode);

      if(results.Errors.Count != 0) {
        foreach(CompilerError error in results.Errors) {
          Console.WriteLine(error.ToString());
        }
      }
    }

    // ??
    private static void PrintParseTree(ParserRuleContext context) {
      foreach(var item in context.children) {
        if(item is ParserRuleContext c) {
          PrintParseTree(c);
        }
        else {
          Console.WriteLine(item.ToString());
        }
      }
    }
  }
}