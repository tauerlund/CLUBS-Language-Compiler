using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  public partial class Checker : ASTVisitor<TypeNode> {

    // OWNS
    public override TypeNode Visit(OwnsActionNode node, object obj) {
      // If the owner type is not Player, log error
      if (node.OwnerType != StandardTypes.Player) {
        ErrorLogger.LogError(new ExpectedTypeError(StandardTypes.Player, node.OwnerType.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      // Visit all owned object declarations
      node.OwnedObjectDcls.ForEach(x => Visit(x));
      return null;
    }

    // PUT
    public override TypeNode Visit(PutActionNode node, object obj) {
      VisitPutAction(node); // Call generic VisitPut method
      return null;
    }

    // TAKE
    public override TypeNode Visit(TakeActionNode node, object obj) {
      VisitPutAction(node); // Call generic VisitPut method

      TypeNode quantityType = Visit(node.Quantity);

      // If quantity type is not Int, log error
      if (quantityType != StandardTypes.Int) {
        ErrorLogger.LogError(new ExpectedTypeError(StandardTypes.Int, quantityType.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      return null;
    }

    // TAKE AT
    public override TypeNode Visit(TakeAtActionNode node, object obj) {
      VisitPutAction(node); // Call generic VisitPut method

      TypeNode quantityType = Visit(node.Quantity);
      TypeNode indexType = Visit(node.Index);

      // If quantity type is not Int, log error
      if (quantityType != StandardTypes.Int) {
        ErrorLogger.LogError(new ExpectedTypeError(StandardTypes.Int, quantityType.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      // If index type is not Int, log error
      if (indexType != StandardTypes.Int) {
        ErrorLogger.LogError(new ExpectedTypeError(StandardTypes.Int, indexType.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      return null;
    }

    // TAKE WHERE
    public override TypeNode Visit(TakeWhereActionNode node, object obj) {
      VisitPutAction(node); // Call generic VisitPut method

      TypeNode quantityType = Visit(node.Quantity);
      TypeNode queryType = Visit(node.Query);

      // If quantity type is not Int, log error
      if (quantityType != StandardTypes.Int) {
        ErrorLogger.LogError(new ExpectedTypeError(StandardTypes.Int, quantityType.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      // If query type is not Bool, log error
      if (queryType != StandardTypes.Bool) {
        ErrorLogger.LogError(new ExpectedTypeError(StandardTypes.Bool, node.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      return null;
    }

    // PRINT
    public override TypeNode Visit(PrintActionNode node, object obj) {
      node.Content.ForEach(x => Visit(x));
      return null;
    }

    // HELPER METHODS

    // Generic visitor for PUT actions
    private TypeNode VisitPutAction(PutActionNode node) {
      SetTypeNode deckType = StandardTypes.GetSetType(StandardTypes.Card);

      TypeNode sourceType = Visit(node.Source);
      TypeNode targetType = Visit(node.Target);

      // If the source type is not a Set OF Card, log error
      if (sourceType != deckType) {
        ErrorLogger.LogError(new ExpectedTypeError(deckType, node.Source.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      // If the target type is not a Set OF Card, log error
      if (targetType != deckType) {
        ErrorLogger.LogError(new ExpectedTypeError(deckType, node.Target.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      return null;
    }
  }
}