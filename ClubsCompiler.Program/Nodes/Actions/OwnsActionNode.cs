using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a OWNS action.
  /// </summary>
  public class OwnsActionNode : ActionNode {

    /// <summary>
    /// The type which owns the objects.
    /// </summary>
    public TypeNode OwnerType { get; set; }

    /// <summary>
    /// The objects that are owned.
    /// </summary>
    public List<DeclarationNode> OwnedObjectDcls { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OwnsActionNode"/> class.
    /// </summary>
    /// <param name="ownerType">The type which owns the objects.</param>
    /// <param name="ownedObjectIds"> The objects that should be owned by the ownerType.</param>
    /// <param name="sourcePosition">The source position of the action.</param>
    public OwnsActionNode(TypeNode ownerType, List<DeclarationNode> ownedObjectDcls, SourcePosition sourcePosition) : base(sourcePosition) {
      OwnerType = ownerType;
      OwnedObjectDcls = ownedObjectDcls;
    }

    public override string ToString() {
      return "OWNS";
    }
  }
}