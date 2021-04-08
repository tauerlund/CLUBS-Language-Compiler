using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a 'Card' type.
  /// </summary>
  public class CardTypeNode : BaseTypeNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="CardTypeNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public CardTypeNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string GetInitializationString(string id) {
      return $"new Card(\"{id}\");\n";
    }

    public override string ToString() {
      return "Card";
    }
  }
}