using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an infix equality operator.
  /// </summary>
  public class IsNode : InfixExpressionNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="IsNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public IsNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "IS";
    }
  }
}