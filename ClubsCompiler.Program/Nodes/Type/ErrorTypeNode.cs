using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a type that is an error.
  /// </summary>
  internal class ErrorTypeNode : TypeNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorTypeNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public ErrorTypeNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string GetInitializationString(string id) {
      throw new NotImplementedException();
    }

    public override string ToString() {
      return "ERROR";
    }
  }
}