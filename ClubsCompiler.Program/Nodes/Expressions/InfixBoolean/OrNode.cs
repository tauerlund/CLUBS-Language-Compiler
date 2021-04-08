using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an infix 'or' operator.
  /// </summary>
  public class OrNode : InfixExpressionNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="OrNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public OrNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "OR";
    }
  }
}