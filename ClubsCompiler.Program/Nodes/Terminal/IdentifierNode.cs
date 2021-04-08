using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an identifier.
  /// </summary>
  public class IdentifierNode : TerminalNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentifiableNode"/> class.
    /// </summary>
    /// <param name="text">The name of the identifier.</param>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public IdentifierNode(string text, SourcePosition sourcePosition) : base(text, sourcePosition) {
    }
  }
}