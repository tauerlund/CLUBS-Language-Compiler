using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents identfier node that is ordered.
  /// </summary>
  public class OrderedIdentifierNode : IdentifierNode {
    public Order Order { get; set; }

    /// <summary>
    /// Initializes a new instance of the class <see cref="OrderedIdentifierNode"/>.
    /// </summary>
    /// <param name="text">The name of the identifier.</param>
    /// <param name="order">The order of the identifier</param>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public OrderedIdentifierNode(string text, Order order, SourcePosition sourcePosition) : base(text, sourcePosition) {
      Order = order;
    }

    public override string ToString() {
      return Text;
    }
  }
}