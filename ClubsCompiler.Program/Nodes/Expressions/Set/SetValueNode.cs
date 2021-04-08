using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents the value a Set of <see cref="OrderedIdentifierNode"/>s.
  /// </summary>
  public class SetValueNode : ExpressionNode {
    public List<OrderedIdentifierNode> Ids { get; set; }
    public int ElementCount { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SetValueNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public SetValueNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return $"Set value expression";
    }
  }
}