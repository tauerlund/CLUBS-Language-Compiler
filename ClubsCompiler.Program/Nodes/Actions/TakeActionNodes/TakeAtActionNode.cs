using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a TAKE AT action.
  /// </summary>
  public class TakeAtActionNode : TakeActionNode {
    public ExpressionNode Index { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TakeAtActionNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public TakeAtActionNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "TAKE AT";
    }
  }
}