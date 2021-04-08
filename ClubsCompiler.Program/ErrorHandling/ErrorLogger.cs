using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Dfa;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Sharpen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Logs all types of errors during compiling.
  /// </summary>
  public class ErrorLogger : BaseErrorListener {
    public List<Error> Errors { get; set; }

    public ErrorLogger() {
      Errors = new List<Error>();
    }

    /// <summary>
    /// Adds a customized compiler error to the logger.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="sourcePosition">The source position of the error.</param>
    public void LogError(string message, SourcePosition sourcePosition) {
      Errors.Add(new Error(message, sourcePosition));
    }

    /// <summary>
    /// Adds a compiler error to the logger.
    /// </summary>
    /// <param name="error">The error to log.</param>
    public void LogError(Error error) {
      Errors.Add(error);
    }

    public override void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] IToken offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e) {
      LogError(new SyntaxError(msg, new SourcePosition(line, charPositionInLine)));
    }

    public void PrintErrors() {
      const int spacesColumn1 = -33;
      const int spacesColumn2 = -15;
      // Print error list headline
      Console.WriteLine($"{"Error type",spacesColumn1} {"Position",spacesColumn2} Description");
      Console.WriteLine($"{new string('-', 10),spacesColumn1} {new string('-', 8),spacesColumn2} {new string('-', 11)}");
      // Print all errors
      foreach(var error in Errors) {
        Console.WriteLine(
          $"{$"[Error/{error}]",spacesColumn1} " +
          $"{$"(Ln{error.LineNumber}:Ch{error.CharPosition})",spacesColumn2} " +
          $"{error.Message} ");
      }
    }

    // XXX: Ved ikke om disse er relevante at override. Tror aldrig de rapporterer om fejl.
    // Men nu har vi fjernet ANTLRs egen error logger, så tænker vi bør beholde dem for nu,  hvis de skulle vise sig brugbare.
    public override void ReportAmbiguity([NotNull] Parser recognizer, [NotNull] DFA dfa, int startIndex, int stopIndex, bool exact, [Nullable] BitSet ambigAlts, [NotNull] ATNConfigSet configs) {
      LogError("Some ambiguity is going on", new SourcePosition(startIndex, 0));
    }

    public override void ReportAttemptingFullContext([NotNull] Parser recognizer, [NotNull] DFA dfa, int startIndex, int stopIndex, [Nullable] BitSet conflictingAlts, [NotNull] SimulatorState conflictState) {
      LogError("Attempting some full context", new SourcePosition(startIndex, 0));
    }

    public override void ReportContextSensitivity([NotNull] Parser recognizer, [NotNull] DFA dfa, int startIndex, int stopIndex, int prediction, [NotNull] SimulatorState acceptState) {
      //LogError("Something about context sensitivity", ErrorType.Syntax, null, new SourcePosition(startIndex, 0));
    }
  }
}