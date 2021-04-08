using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an integer literal.
  /// </summary>
  public class IntegerLiteral : TerminalNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegerLiteral"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public IntegerLiteral(string text, SourcePosition sourcePosition) : base(text, sourcePosition) {
    }
  }
}