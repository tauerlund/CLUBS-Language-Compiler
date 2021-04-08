using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a PUT action.
  /// </summary>
  public class PutActionNode : ActionNode {
    public ReferenceNode Source { get; set; }

    public ReferenceNode Target { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PutActionNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public PutActionNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "PUT";
    }
  }
}