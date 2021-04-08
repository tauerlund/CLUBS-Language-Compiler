using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a RANDOM expression.
  /// </summary>
  public class RandomNode : ExpressionNode {
    public ExpressionNode LowerLimit { get; set; }
    public ExpressionNode UpperLimit { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RandomNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public RandomNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "RANDOM";
    }
  }
}