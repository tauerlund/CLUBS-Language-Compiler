using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Abstract class representing an element that is identifiable (is represented by an id).
  /// </summary>
  public abstract class IdentifiableNode : ExpressionNode {
    public IdentifierNode Id { get; set; }

    public IdentifiableNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }
  }
}