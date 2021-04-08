using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an error from attempting to declare an already declared variable.
  /// </summary>
  public class VariableAlreadyDeclaredError : Error {

    /// <summary>
    /// Initializes a new instance of the <see cref="VariableAlreadyDeclaredError"/> class.
    /// </summary>
    /// <param name="variableName">The name of the variable attempting to be declared.</param>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public VariableAlreadyDeclaredError(string variableName, SourcePosition sourcePosition) : base(sourcePosition) {
      Message = $"Variable '{variableName}' already declared.";
    }
  }
}