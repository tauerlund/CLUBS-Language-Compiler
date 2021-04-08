using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Abstract class representing an infix arithmetic operator.
  /// </summary>
  public abstract class InfixOperatorNode : InfixExpressionNode {

    public InfixOperatorNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }
  }
}