using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Prints the parse tree when passed to ANTRL tree walker.
  /// </summary>
  class ParseTreeListener : IParseTreeListener {
    private int _currentIndentation = 0;
    private const int INDENTATION_WS_COUNT = 2; // How much every rule should be indented.
    private bool _isPreviousTerminal = false; // If the previous element printed to console was a terminal (else it's a rule!)
    private bool _foundTerminal; // A terminal was found and printed to console.

    public void EnterEveryRule([NotNull] ParserRuleContext ctx) {
      if(_isPreviousTerminal) {
        Console.Write("\b\"" + "\n"); // \b backs up one char
        _isPreviousTerminal = false;
      }

      Console.WriteLine(new string(' ', ++_currentIndentation * INDENTATION_WS_COUNT) + ctx.GetType().Name.Replace("Context", string.Empty));

      _foundTerminal = false;
    }

    public void ExitEveryRule([NotNull] ParserRuleContext ctx) {
      if(!_foundTerminal) {
        Console.WriteLine("\n\n!!DID NOT FIND TERMINAL!!\n\n");
      }
      _currentIndentation--;
      //Console.WriteLine("Exiting rule");
    }

    public void VisitErrorNode([NotNull] IErrorNode node) {
      //throw new NotImplementedException();
    }

    public void VisitTerminal([NotNull] ITerminalNode node) {
      if(!_isPreviousTerminal) {
        Console.Write(new string(' ', _currentIndentation + INDENTATION_WS_COUNT));
        Console.Write("\"");
      }
      _isPreviousTerminal = true;
      Console.Write(EscapeCharToString(node.Symbol.Text) + " ");
      _foundTerminal = true;

      //throw new NotImplementedException();
    }

    private string EscapeCharToString(string terminal) {
      ;
      if(terminal == "\n") {
        return @"\n";
      }
      if(terminal == "\t") {
        return @"\t";
      }
      if(terminal == "\r") {
        return @"\r";
      }
      return terminal;
    }
  }
}