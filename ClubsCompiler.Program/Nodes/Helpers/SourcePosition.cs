using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents the position of an element in the source code.
  /// </summary>
  public class SourcePosition {
    public int LineNumber { get; set; }
    public int CharStartIndex { get; set; }

    /// <summary>
    /// Gets the source position of a given <see cref="IToken"/>.
    /// </summary>
    /// <param name="token">The <see cref="IToken"/> to get the position from.</param>
    public SourcePosition(IToken token) {
      LineNumber = token.Line;
      CharStartIndex = token.Column;
    }

    /// <summary>
    /// Brews a good ol' source position as we know it.
    /// </summary>
    /// <param name="lineNumber">I really think this goes without saying.</param>
    /// <param name="charStartIndex">Same.</param>
    public SourcePosition(int lineNumber, int charStartIndex) {
      LineNumber = lineNumber;
      CharStartIndex = charStartIndex;
    }
  }
}