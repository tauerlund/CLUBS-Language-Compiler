using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a 'Player' type.
  /// </summary>
  public class PlayerTypeNode : BaseTypeNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerTypeNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public PlayerTypeNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string GetInitializationString(string id) {
      return $"new Player(\"{id}\");\n";
    }

    public override string ToString() {
      return "Player";
    }
  }
}