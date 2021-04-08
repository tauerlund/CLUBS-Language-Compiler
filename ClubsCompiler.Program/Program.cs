using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace ClubsCompiler.Program {
  // Før aflevering
  // - Fjern alle debug print til console

  /// <summary>
  /// Facilitates program control.
  /// </summary>
  public class Program {
    private const string DEFAULT_INPUT_PATH = "input/source.clubs";

    private static void Main(string[] args) {
      string input = GetInput(args);
      Compiler compiler = new Compiler();

      if(input != null) {
        compiler.Compile(input);
      }

      Console.ReadKey();
    }

    public static string GetInput(string[] args) {
      // Get the input file specified in argument.
      string inputFilePath;
      if(args.Length > 0) {
        inputFilePath = args[0];
      }
      else {
        // NOTE: Only for development stage.
        inputFilePath = DEFAULT_INPUT_PATH;
      }

      if(!File.Exists(inputFilePath)) {
        Console.WriteLine($"[Error/Arguments] No such file: {inputFilePath}");
        return null;
      }

      // READ SOURCE CODE.
      return File.ReadAllText(inputFilePath);
    }
  }
}