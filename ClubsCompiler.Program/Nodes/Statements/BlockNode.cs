using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a block in the program.
  /// </summary>
  public class BlockNode : StatementNode {
    public List<StatementNode> Statements { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlockNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public BlockNode(SourcePosition sourcePosition) : base(sourcePosition) {
      Statements = new List<StatementNode>();
    }

    public override string ToString() {
      return "Block";
    }
  }
}