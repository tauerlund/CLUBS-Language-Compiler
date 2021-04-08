using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents the entirety of the program.
  /// </summary>
  public class ProgNode : ASTNode {
    public List<ASTNode> Children { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProgNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public ProgNode(SourcePosition sourcePosition) : base(sourcePosition) {
      Children = new List<ASTNode>();
    }

    public override string ToString() {
      return "Program";
    }
  }
}