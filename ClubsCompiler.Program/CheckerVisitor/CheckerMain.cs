using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Performs type checking.
  /// </summary>
  public partial class Checker : ASTVisitor<TypeNode> {
    public SymbolTable SymbolTable { get; private set; }

    public ErrorLogger ErrorLogger { get; private set; }

    /// <summary>
    /// Initalizes a new instance of the <see cref="Checker"/> class.
    /// </summary>
    /// <param name="errorLogger">An error logger for logging type checking errors.</param>
    public Checker(ErrorLogger errorLogger) {
      SymbolTable = new SymbolTable();
      ErrorLogger = errorLogger;
      EstablishStandardTypes();
    }

    public override TypeNode Visit(ProgNode node, object obj) {
      node.Children.ForEach(x => Visit(x));
      return null;
    }

    // HELPER METHODS

    // Instantiates static TypeNodes for each type for easy comparison in Checker
    private void EstablishStandardTypes() {
      StandardTypes.Bool = new BoolTypeNode(null);
      StandardTypes.Card = new CardTypeNode(null);
      StandardTypes.CardValue = new CardValueTypeNode(null);
      StandardTypes.Int = new IntTypeNode(null);
      StandardTypes.Player = new PlayerTypeNode(null);
      StandardTypes.String = new StringTypeNode(null);
      StandardTypes.Set = new SetTypeNode(new ErrorTypeNode(null), null);
    }
  }
}