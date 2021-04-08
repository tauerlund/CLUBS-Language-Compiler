using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a string literal.
  /// </summary>
  public class StringLiteral : ExpressionNode {
    public string Text { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StringLiteral"/> class.
    /// </summary>
    /// <param name="text">The text of the string.</param>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public StringLiteral(string text, SourcePosition sourcePosition) : base(sourcePosition) {
      Text = text;
    }

    public override string ToString() {
      return Text;
    }
  }
}