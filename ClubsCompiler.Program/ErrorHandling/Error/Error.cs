using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an error that occured during compilation.
  /// </summary>
  public class Error {
    public string Message { get; protected set; }
    public int LineNumber { get; }
    public int CharPosition { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public Error(SourcePosition sourcePosition) {
      LineNumber = sourcePosition.LineNumber;
      CharPosition = sourcePosition.CharStartIndex;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class with a custom message.
    /// </summary>
    /// <param name="message">The error message to log.</param>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public Error(string message, SourcePosition sourcePosition) {
      Message = message;
      LineNumber = sourcePosition.LineNumber;
      CharPosition = sourcePosition.CharStartIndex;
    }

    // Print name of class without "error"-suffix.
    public override string ToString() {
      return GetType().Name.Replace("Error", string.Empty);
    }
  }
}