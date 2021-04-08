using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a boolean literal.
  /// </summary>
  public class BoolLiteral : TerminalNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="BoolLiteral"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public BoolLiteral(string text, SourcePosition sourcePosition) : base(text, sourcePosition) {
    }
  }
}