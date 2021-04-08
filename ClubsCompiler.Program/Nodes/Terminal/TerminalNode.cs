using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Abstract class representing a terminal and its text representation.
  /// </summary>
  public abstract class TerminalNode : ExpressionNode {
    public string Text { get; set; }

    public TerminalNode(string text, SourcePosition sourcePosition) : base(sourcePosition) {
      Text = text;
    }

    public override string ToString() {
      return Text;
    }
  }
}