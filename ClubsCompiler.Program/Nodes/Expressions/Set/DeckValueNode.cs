using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents the value of a Set of type 'Card'.
  /// </summary>
  public class DeckValueNode : ExpressionNode {
    public List<IdentifierNode> Ids { get; set; }
    public int ElementCount { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeckValueNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public DeckValueNode(List<IdentifierNode> ids, SourcePosition sourcePosition) : base(sourcePosition) {
      Ids = ids;
      Type = new SetTypeNode(new CardTypeNode(sourcePosition), sourcePosition);
    }

    public override string ToString() {
      return "Deck value expression";
    }
  }
}