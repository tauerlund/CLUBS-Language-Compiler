using Antlr4.Runtime;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an assignment with a left and a right <see cref="ExpressionNode"/>.
  /// </summary>
  public class AssignmentNode : StatementNode {
    public ExpressionNode Left { get; set; }
    public ExpressionNode Right { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssignmentNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public AssignmentNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "Assignment";
    }
  }
}