using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an infix division operator.
  /// </summary>
  public class DivisionNode : InfixOperatorNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="DivisionNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public DivisionNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "/";
    }
  }
}