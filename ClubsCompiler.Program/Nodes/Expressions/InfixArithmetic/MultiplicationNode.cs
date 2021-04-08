using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an infix multiplication operator.
  /// </summary>
  public class MultiplicationNode : InfixOperatorNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="MultiplicationNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public MultiplicationNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "*";
    }
  }
}