using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// The base class for all nodes in an Abstract Syntax Tree
  /// contaning the children of the concrete node and the position in the source code.
  /// </summary>
  public abstract class ASTNode {
    public SourcePosition SourcePosition { get; set; }

    public ASTNode(SourcePosition sourcePosition) {
      SourcePosition = sourcePosition;
    }

    // Require implementation of ToString override, for use in error logging
    public abstract override string ToString();
  }
}