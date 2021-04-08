using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a 'FORALL' statement.
  /// </summary>
  public class ForAllNode : ControlStructureNode {

    /// <summary>
    /// The iteration item representing the current element in the set.
    /// </summary>
    public DeclarationNode Child { get; set; }

    /// <summary>
    /// The element that should be iterated.
    /// </summary>
    public ReferenceNode Parent { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ForAllNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public ForAllNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string ToString() {
      return "FORALL";
    }
  }
}