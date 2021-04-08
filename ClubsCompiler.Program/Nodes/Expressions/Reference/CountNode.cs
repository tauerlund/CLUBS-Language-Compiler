using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents the count of a Set.
  /// </summary>
  public class CountNode : ReferenceNode {
    public int Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CountNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public CountNode(SourcePosition sourcePosition) : base(new IdentifierNode("", sourcePosition)) {
    }

    public override string ToString() {
      return ".count";
    }
  }
}