using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an error from using the wrong type with an operator.
  /// </summary>
  public class CannotUseWithOperatorError : Error {

    /// <summary>
    /// Initializes a new instance of the <see cref="CannotUseWithOperatorError"/> class.
    /// </summary>
    /// <param name="type">The type that was attempted to be used with the operator.</param>
    /// <param name="op">The operator used.</param>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public CannotUseWithOperatorError(TypeNode type, InfixExpressionNode op, SourcePosition sourcePosition) : base(sourcePosition) {
      Message = $"Type '{type}' cannot be used with '{op}' operator.";
    }
  }
}