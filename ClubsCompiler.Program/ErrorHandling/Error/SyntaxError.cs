using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a syntactical error.
  /// </summary>
  public class SyntaxError : Error {

    /// <summary>
    /// Initializes a new instance of the <see cref="SyntaxError"/> class.
    /// </summary>
    /// <param name="message">The syntax error message to log.</param>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public SyntaxError(string message, SourcePosition sourcePosition) : base(sourcePosition) {
      Message = message;
    }
  }
}