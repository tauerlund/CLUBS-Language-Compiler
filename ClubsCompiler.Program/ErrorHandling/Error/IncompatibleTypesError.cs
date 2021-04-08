using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an error from using two incompatible types.
  /// </summary>
  public class IncompatibleTypesError : Error {

    /// <summary>
    /// Initializes a new instance of the <see cref="IncompatibleTypesError"/> class.
    /// </summary>
    /// <param name="vType">The variable type.</param>
    /// <param name="eType">The expression type.</param>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public IncompatibleTypesError(TypeNode vType, TypeNode eType, SourcePosition sourcePosition) : base(sourcePosition) {
      Message = $"Type '{vType}' is not compatible with type '{eType}'.";
    }
  }
}