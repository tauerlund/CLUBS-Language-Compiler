using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a query as part of a <see cref="TakeWhereActionNode"/>.
  /// </summary>
  public class QueryNode : ExpressionNode {
    public InfixExpressionNode infixExpression { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="QueryNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public QueryNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "Query";
    }
  }
}