using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a PRINT action.
  /// </summary>
  public class PrintActionNode : ActionNode {
    public List<ExpressionNode> Content { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PrintActionNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public PrintActionNode(SourcePosition sourcePosition) : base(sourcePosition) {
      Content = new List<ExpressionNode>();
    }

    public override string ToString() {
      return "PRINT";
    }
  }
}