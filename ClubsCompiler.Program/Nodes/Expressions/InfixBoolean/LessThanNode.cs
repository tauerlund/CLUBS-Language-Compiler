using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an infix 'less than' operator.
  /// </summary>
  public class LessThanNode : InfixExpressionNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="LessThanNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public LessThanNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "<";
    }
  }
}