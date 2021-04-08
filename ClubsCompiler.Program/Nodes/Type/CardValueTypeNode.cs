using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a 'CardValue' type.
  /// </summary>
  public class CardValueTypeNode : BaseTypeNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="CardValueTypeNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public CardValueTypeNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string GetInitializationString(string id) {
      return $"new CardValue(\"{id}\");\n";
    }

    public override string ToString() {
      return "CardValue";
    }
  }
}