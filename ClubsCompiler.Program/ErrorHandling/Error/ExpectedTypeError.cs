using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an error from using an unexpected type.
  /// </summary>
  public class ExpectedTypeError : Error {

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpectedTypeError"/> class.
    /// </summary>
    /// <param name="expectedType">The type expected by the compiler.</param>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public ExpectedTypeError(TypeNode expectedType, SourcePosition sourcePosition) : base(sourcePosition) {
      Message = $"Expected type '{expectedType}'.";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpectedTypeError"/> class.
    /// </summary>
    /// <param name="node">The node expecting the expected type.</param>
    /// <param name="expectedType">The type expected by the compiler.</param>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public ExpectedTypeError(ASTNode node, TypeNode expectedType, SourcePosition sourcePosition) : base(sourcePosition) {
      Message = $"'{node}' expected type '{expectedType}'.";
    }
  }
}