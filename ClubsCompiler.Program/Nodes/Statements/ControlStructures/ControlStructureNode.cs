using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Abstract class representing a generic control structure containing a block.
  /// </summary>
  public abstract class ControlStructureNode : StatementNode {
    public BlockNode Block { get; set; }

    public ControlStructureNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }
  }
}