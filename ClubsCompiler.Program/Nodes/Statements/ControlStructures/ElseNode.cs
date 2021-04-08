using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an 'ELSE' statement.
  /// </summary>
  public class ElseNode : ControlStructureNode {

    public ElseNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "ELSE";
    }
  }
}