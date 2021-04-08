using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a 'Set' type with an underlying <see cref="TypeNode"/>.
  /// </summary>
  public class SetTypeNode : TypeNode {

    /// <summary>
    /// The type of the elements in the set.
    /// </summary>
    public TypeNode Type { get; set; }

    public int ElementCount { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SetTypeNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public SetTypeNode(TypeNode type, SourcePosition sourcePosition) : base(sourcePosition) {
      Type = type;
    }

    public override string ToString() {
      return $"Set OF {Type}";
    }

    public override string GetInitializationString(string id) {
      if (Type is BaseTypeNode) {
        return $"new ComparableList<{Type}>();\n";
      }
      else {
        return $"new List<{Type}>();\n";
      }
    }
  }
}