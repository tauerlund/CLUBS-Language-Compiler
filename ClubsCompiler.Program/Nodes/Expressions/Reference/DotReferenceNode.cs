using ClubsCompiler.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a reference using the dot-notation.
  /// </summary>
  public class DotReferenceNode : ReferenceNode {

    /// <summary>
    /// The element that gets dotted.
    /// </summary>
    public ReferenceNode Parent { get; set; }

    /// <summary>
    /// The member reached by dotting.
    /// </summary>
    public ReferenceNode Member { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DotReferenceNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public DotReferenceNode(SourcePosition sourcePosition) : base(new IdentifierNode("", sourcePosition)) {
    }
  }
}