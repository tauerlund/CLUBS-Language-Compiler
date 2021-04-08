using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a TAKE WHERE action.
  /// </summary>
  public class TakeWhereActionNode : TakeActionNode {
    public QueryNode Query { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TakeWhereActionNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public TakeWhereActionNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "TAKE WHERE";
    }
  }
}