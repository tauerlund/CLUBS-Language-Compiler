using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a bool type.
  /// </summary>
  public class BoolTypeNode : TypeNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="BoolTypeNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public BoolTypeNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string GetInitializationString(string id) {
      return "false;\n";
    }

    public override string ToString() {
      return "Bool";
    }
  }
}