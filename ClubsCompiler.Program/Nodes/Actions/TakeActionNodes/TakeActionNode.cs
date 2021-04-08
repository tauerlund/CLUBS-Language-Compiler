using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a TAKE action.
  /// </summary>
  public class TakeActionNode : PutActionNode {
    public ExpressionNode Quantity { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TakeActionNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public TakeActionNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "TAKE";
    }
  }
}