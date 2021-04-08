using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// The abstract base class of all type nodes.
  /// </summary>
  public abstract class TypeNode : ASTNode {

    public TypeNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    /// <summary>
    /// Equality operator override, equality is determined by type.
    /// </summary>
    public static bool operator ==(TypeNode type1, TypeNode type2) {
      if ((type1 is ErrorTypeNode) || (type2 is ErrorTypeNode)) {
        return true;
      }
      if (type1 is SetTypeNode set1 && type2 is SetTypeNode set2) {
        return (set1.Type == set2.Type);
      }
      return type1.GetType() == type2.GetType();
    }

    /// <summary>
    /// Inequality operator override, equality is determined by type.
    /// </summary>
    public static bool operator !=(TypeNode type1, TypeNode type2) {
      return !(type1 == type2);
    }

    /// <summary>
    /// Returns code that will initalize this type.
    /// </summary>
    /// <param name="name">The identifier text.</param>
    /// <returns></returns>
    public abstract string GetInitializationString(string id);
  }
}