using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a TAKE ALL action.
  /// </summary>
  public class TakeAllActionNode : TakeActionNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="TakeAllActionNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public TakeAllActionNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "TAKE ALL";
    }
  }
}