using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an infix 'and' operator.
  /// </summary>
  public class AndNode : InfixExpressionNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="AndNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public AndNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "AND";
    }
  }
}