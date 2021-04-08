using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a value applied to a Card.
  /// </summary>
  public class CardValueExpressionNode : ExpressionNode {
    public ReferenceNode Parent { get; set; }
    public ReferenceNode Child { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CardValueExpressionNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public CardValueExpressionNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "Card value expression";
    }
  }
}