using ClubsCompiler.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an 'IF' statement.
  /// </summary>
  public class IfNode : ControlStructureNode {
    public ExpressionNode Predicate { get; set; }

    public List<ElseIfNode> ElseIfChain { get; set; }

    public BlockNode ElseBlock { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IfNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public IfNode(SourcePosition sourcePosition) : base(sourcePosition) {
      ElseIfChain = new List<ElseIfNode>();
    }

    public override string ToString() {
      return "IF";
    }
  }
}