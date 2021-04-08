using Antlr4.Runtime;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Abstract class representing an expression.
  /// </summary>
  public abstract class ExpressionNode : ASTNode {

    /// <summary>
    /// The type of the expression.
    /// </summary>
    public TypeNode Type { get; set; }

    public ExpressionNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }
  }
}