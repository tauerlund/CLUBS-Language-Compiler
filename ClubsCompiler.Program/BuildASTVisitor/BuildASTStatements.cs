using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  public partial class BuildASTVisitor : CLUBSBaseVisitor<ASTNode> {

    // Block
    public override ASTNode VisitBlockStmt(CLUBSParser.BlockStmtContext context) {
      BlockNode blockNode = new BlockNode(new SourcePosition(context.start));

      foreach(CLUBSParser.StmtContext stmt in context.stmt()) {
        blockNode.Statements.Add(Visit(stmt) as StatementNode);
      }

      return blockNode;
    }

    // Declaration
    public override ASTNode VisitDclStmt(CLUBSParser.DclStmtContext context) {
      SourcePosition sourcePosition = new SourcePosition(context.start);

      DeclarationNode node;
      TypeNode type = GetTypeNode(context.type().kind.Type, sourcePosition);
      IdentifierNode id = new IdentifierNode(context.id.Text, new SourcePosition(context.id));

      // Check if declaration is a set.
      if(context.set != null) {
        // If set, type should be wrapped in a set type.
        node = new DeclarationNode(new SetTypeNode(type, sourcePosition), id, sourcePosition);
      }
      else {
        // Else, type should be as is.
        node = new DeclarationNode(type, id, sourcePosition);
      }

      // Visit assignment if has any
      if(context.assignment != null) {
        node.AssignmentExpression = Visit(context.assignment) as ExpressionNode;
      }

      return node;
    }

    // Assign
    public override ASTNode VisitAssignStmt(CLUBSParser.AssignStmtContext context) {
      AssignmentNode node = new AssignmentNode(new SourcePosition(context.start));

      node.Left = Visit(context.left) as ExpressionNode;
      node.Right = Visit(context.right) as ExpressionNode;

      return node;
    }

    // FORALL
    public override ASTNode VisitForAllStmt(CLUBSParser.ForAllStmtContext context) {
      SourcePosition forallPosition = new SourcePosition(context.start);
      SourcePosition childPosition = new SourcePosition(context.child);

      ForAllNode node = new ForAllNode(forallPosition);

      IdentifierNode childId = new IdentifierNode(context.child.Text, childPosition);

      node.Child = new DeclarationNode(null, childId, childPosition);
      node.Parent = Visit(context.parent) as ReferenceNode;
      node.Block = Visit(context.blockStmt()) as BlockNode;

      return node;
    }

    // IF
    public override ASTNode VisitIfStmt(CLUBSParser.IfStmtContext context) {
      IfNode node = new IfNode(new SourcePosition(context.start));

      node.Predicate = Visit(context.expr()) as ExpressionNode;
      node.Block = Visit(context.ifBlock) as BlockNode;

      // If has any ELSE IF, visit and add to chain
      if(context.elseIfStmt() != null) {
        foreach(CLUBSParser.ElseIfStmtContext elseIf in context.elseIfStmt()) {
          node.ElseIfChain.Add(Visit(elseIf) as ElseIfNode);
        }
      }

      // Visit ELSE if has any
      if(context.elseBlock != null) {
        node.ElseBlock = Visit(context.elseBlock) as BlockNode;
      }

      return node;
    }

    // ELSE IF
    public override ASTNode VisitElseIfStmt(CLUBSParser.ElseIfStmtContext context) {
      ElseIfNode node = new ElseIfNode(new SourcePosition(context.start));

      node.Predicate = Visit(context.expr()) as ExpressionNode;
      node.Block = Visit(context.blockStmt()) as BlockNode;

      return node;
    }

    // WHILE
    public override ASTNode VisitWhileStmt(CLUBSParser.WhileStmtContext context) {
      WhileNode node = new WhileNode(new SourcePosition(context.start));

      node.Predicate = Visit(context.predicate) as ExpressionNode;
      node.Block = Visit(context.blockStmt()) as BlockNode;

      return node;
    }
  }
}