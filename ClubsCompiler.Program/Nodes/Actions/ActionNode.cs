using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Abstract class representing an action.
  /// </summary>
  public abstract class ActionNode : StatementNode {

    public ActionNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }
  }
}