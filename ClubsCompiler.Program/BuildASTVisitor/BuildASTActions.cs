using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  public partial class BuildASTVisitor : CLUBSBaseVisitor<ASTNode> {

    // OWNS
    public override ASTNode VisitOwnsActionStmt(CLUBSParser.OwnsActionStmtContext context) {
      List<DeclarationNode> ownedObjectDcls = context.dclStmt().Select(x => Visit(x) as DeclarationNode).ToList();

      SourcePosition sourcePosition = new SourcePosition(context.type().kind);
      TypeNode ownerType = GetTypeNode(context.type().kind.Type, sourcePosition);

      return new OwnsActionNode(ownerType, ownedObjectDcls, sourcePosition);
    }

    // PUT
    public override ASTNode VisitPutStmt(CLUBSParser.PutStmtContext context) {
      PutActionNode node = new PutActionNode(new SourcePosition(context.start));

      node.Source = Visit(context.source) as ReferenceNode;
      node.Target = Visit(context.target) as ReferenceNode;

      return node;
    }

    // TAKE
    public override ASTNode VisitTakeStmt(CLUBSParser.TakeStmtContext context) {
      TakeActionNode node = Visit(context.takeActionStmt()) as TakeActionNode;

      node.Source = Visit(context.source) as ReferenceNode;
      node.Target = Visit(context.target) as ReferenceNode;

      return node;
    }

    // TAKE QUANTITY
    public override ASTNode VisitTakeQuantityStmt(CLUBSParser.TakeQuantityStmtContext context) {
      TakeActionNode node = new TakeActionNode(new SourcePosition(context.start));

      node.Quantity = Visit(context.quantity) as ExpressionNode;

      return node;
    }

    // TAKE WHERE
    public override ASTNode VisitTakeQueryStmt(CLUBSParser.TakeQueryStmtContext context) {
      TakeWhereActionNode node = new TakeWhereActionNode(new SourcePosition(context.start));

      node.Quantity = Visit(context.quantity) as ExpressionNode;
      node.Query = Visit(context.query()) as QueryNode;

      return node;
    }

    // TAKE AT
    public override ASTNode VisitTakeIndexStmt(CLUBSParser.TakeIndexStmtContext context) {
      TakeAtActionNode node = new TakeAtActionNode(new SourcePosition(context.start));

      node.Quantity = Visit(context.quantity) as ExpressionNode;
      node.Index = Visit(context.index) as ExpressionNode;

      return node;
    }

    // TAKE ALL
    public override ASTNode VisitTakeAllStmt(CLUBSParser.TakeAllStmtContext context) {
      return new PutActionNode(new SourcePosition(context.start));
    }

    // PRINT
    public override ASTNode VisitPrintActionStmt(CLUBSParser.PrintActionStmtContext context) {
      PrintActionNode node = new PrintActionNode(new SourcePosition(context.start));

      // Add all elements of the print statement.
      foreach(CLUBSParser.PrintContentContext content in context.printContent()) {
        node.Content.Add(Visit(content) as ExpressionNode);
      }

      return node;
    }
  }
}