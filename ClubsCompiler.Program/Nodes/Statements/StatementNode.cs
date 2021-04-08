using Antlr4.Runtime;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Abstract class representing a statement.
  /// </summary>
  public abstract class StatementNode : ASTNode {

    public StatementNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }
  }
}