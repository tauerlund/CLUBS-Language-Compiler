using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an infix subtraction operator.
  /// </summary>
  public class SubtractionNode : InfixOperatorNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="SubtractionNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public SubtractionNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "-";
    }
  }
}