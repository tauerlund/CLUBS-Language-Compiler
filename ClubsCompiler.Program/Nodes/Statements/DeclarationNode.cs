using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a declaration of an identifier containing its id and type.
  /// </summary>
  public class DeclarationNode : StatementNode {
    public IdentifierNode Id { get; set; }

    public TypeNode Type { get; set; }

    public ExpressionNode AssignmentExpression { get; set; }

    public bool IsAssigned { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeclarationNode"/> class.
    /// </summary>
    /// <param name="type">The type of the newly declared identifier.</param>
    /// <param name="id">The name of the newly declared identifier.</param>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public DeclarationNode(TypeNode type, IdentifierNode id, SourcePosition sourcePosition) : base(sourcePosition) {
      Id = id;
      Type = type;
    }

    public override string ToString() {
      return "Declaration";
    }
  }
}