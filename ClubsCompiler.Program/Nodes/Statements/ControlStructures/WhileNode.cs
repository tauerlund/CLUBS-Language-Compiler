using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a 'WHILE' statement.
  /// </summary>
  public class WhileNode : ControlStructureNode {
    public ExpressionNode Predicate { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="WhileNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public WhileNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "WHILE";
    }
  }
}