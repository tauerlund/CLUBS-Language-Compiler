using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Abstract class representing a base type with an order.
  /// </summary>
  public abstract class BaseTypeNode : TypeNode {

    public BaseTypeNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }
  }
}