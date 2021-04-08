using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Abstract class representing an infix expression containing left and right <see cref="ExpressionNode"/>s.
  /// </summary>
  public abstract class InfixExpressionNode : ExpressionNode {
    public ExpressionNode Left { get; set; } // Left operand. The variable that gets assigned.
    public ExpressionNode Right { get; set; } // Right operand. The value assigned to the variable.

    public InfixExpressionNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }
  }
}