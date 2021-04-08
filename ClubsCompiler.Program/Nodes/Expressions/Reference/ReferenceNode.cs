using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a reference to an already declared identifier.
  /// </summary>
  public class ReferenceNode : IdentifiableNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentifierNode"/> class.
    /// </summary>
    /// <param name="id">The name of the referenced identifier.</param>
    public ReferenceNode(IdentifierNode id) : base(id.SourcePosition) {
      Id = id;
    }

    public override string ToString() {
      return Id.Text;
    }
  }
}