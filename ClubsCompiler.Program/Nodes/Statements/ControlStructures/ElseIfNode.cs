using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an 'ELIF' statement.
  /// </summary>
  public class ElseIfNode : ControlStructureNode {
    public ExpressionNode Predicate { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ElseIfNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public ElseIfNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "ELSE IF";
    }
  }
}