using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents an error from attempting to reference an undeclared variable.
  /// </summary>
  public class UndeclaredVariableError : Error {

    /// <summary>
    /// Initializes a new instance of the <see cref="UndeclaredVariableError"/> class.
    /// </summary>
    /// <param name="variableName">The name of the referenced variable.</param>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public UndeclaredVariableError(string variableName, SourcePosition sourcePosition) : base(sourcePosition) {
      Message = $"Undeclared variable: '{variableName}'.";
    }
  }
}