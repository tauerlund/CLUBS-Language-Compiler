using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  public partial class BuildASTVisitor : CLUBSBaseVisitor<ASTNode> {

    // Identifier
    public override ASTNode VisitId(CLUBSParser.IdContext context) {
      SourcePosition sourcePosition = new SourcePosition(context.id);
      return new ReferenceNode(new IdentifierNode(context.id.Text, sourcePosition));
    }

    // Dot notation
    public override ASTNode VisitIdDot(CLUBSParser.IdDotContext context) {
      SourcePosition sourcePosition = new SourcePosition(context.start);

      DotReferenceNode node = new DotReferenceNode(sourcePosition);

      node.Parent = Visit(context.parent) as ReferenceNode;
      node.Member = Visit(context.member) as ReferenceNode;

      return node;
    }

    // Set value
    public override ASTNode VisitSetValueExpr(CLUBSParser.SetValueExprContext context) {
      SetValueNode node = new SetValueNode(new SourcePosition(context.start));

      // Visit all elements in the set and add them as Ids to the SetValueNode
      node.Ids = context.setElement().Select(id => Visit(id) as OrderedIdentifierNode).ToList();
      // Last element is added manually to get the correct order
      node.Ids.Add(new OrderedIdentifierNode(context.id.Text, Order.LAST, new SourcePosition(context.id)));

      return node;
    }

    // Set element
    public override ASTNode VisitSetElement(CLUBSParser.SetElementContext context) {
      Order order = Order.EQUAL;

      // Get the correct Order enum by switching on token
      switch(context.order().kind.Type) {
        case CLUBSLexer.LT:
          order = Order.LT;
          break;

        case CLUBSLexer.COMMA:
          order = Order.EQUAL;
          break;

        default:
          break;
      }

      OrderedIdentifierNode node = new OrderedIdentifierNode(context.id.Text, order, new SourcePosition(context.start));

      return node;
    }

    // Card value expression
    public override ASTNode VisitCardValueExpr(CLUBSParser.CardValueExprContext context) {
      CardValueExpressionNode node = new CardValueExpressionNode(new SourcePosition(context.start));

      node.Parent = Visit(context.parent) as ReferenceNode;
      node.Child = Visit(context.child) as ReferenceNode;

      return node;
    }

    // Infix arithmetic expression
    public override ASTNode VisitInfixArithmeticExpr(CLUBSParser.InfixArithmeticExprContext context) {
      SourcePosition sourcePosition = new SourcePosition(context.start);
      InfixOperatorNode node;

      // Get the correct Infix operator by switching on the operator token
      switch(context.op.Type) {
        case CLUBSParser.MUL:
          node = new MultiplicationNode(sourcePosition);
          break;

        case CLUBSParser.DIV:
          node = new DivisionNode(sourcePosition);
          break;

        case CLUBSParser.ADD:
          node = new AdditionNode(sourcePosition);
          break;

        case CLUBSParser.SUB:
          node = new SubtractionNode(sourcePosition);
          break;

        default:
          throw new Exception("Operator not found");
      }

      node.Left = Visit(context.left) as ExpressionNode;
      node.Right = Visit(context.right) as ExpressionNode;

      return node;
    }

    // Atom
    public override ASTNode VisitAtom(CLUBSParser.AtomContext context) {
      SourcePosition sourcePosition = new SourcePosition(context.start);

      // Get the correct literal node by switching on the token
      switch(context.kind.Type) {
        case CLUBSLexer.INUM:
          return new IntegerLiteral(context.kind.Text, sourcePosition);

        case CLUBSLexer.BOOL_VAL:
          return new BoolLiteral(context.kind.Text, sourcePosition);

        case CLUBSLexer.STRING_VAL:
          return new StringLiteral(context.kind.Text, sourcePosition);

        default:
          throw new Exception("Type not found");
      }
    }

    // Deck value expression
    public override ASTNode VisitDeckValueExpr(CLUBSParser.DeckValueExprContext context) {
      SourcePosition sourcePosition = new SourcePosition(context.start);
      // Add all CardValue set Ids as IdentifierNodes in the Ids list
      return new DeckValueNode(new List<IdentifierNode>(context.ID().Select(i => new IdentifierNode(i.GetText(), sourcePosition))), sourcePosition);
    }

    // PRINT id
    public override ASTNode VisitPrintId(CLUBSParser.PrintIdContext context) {
      return Visit(context.idExpr());
    }

    // PRINT string
    public override ASTNode VisitPrintString(CLUBSParser.PrintStringContext context) {
      SourcePosition sourcePosition = new SourcePosition(context.start);
      return new StringLiteral(context.content.Text, sourcePosition);
    }

    // RANDOM
    public override ASTNode VisitRandomExpr(CLUBSParser.RandomExprContext context) {
      RandomNode node = new RandomNode(new SourcePosition(context.start));

      node.LowerLimit = Visit(context.lower) as ExpressionNode;
      node.UpperLimit = Visit(context.upper) as ExpressionNode;

      return node;
    }

    // AND
    public override ASTNode VisitInfixAnd(CLUBSParser.InfixAndContext context) {
      AndNode node = new AndNode(new SourcePosition(context.start));

      node.Left = Visit(context.left) as ExpressionNode;
      node.Right = Visit(context.right) as ExpressionNode;

      return node;
    }

    // OR
    public override ASTNode VisitInfixOr(CLUBSParser.InfixOrContext context) {
      OrNode node = new OrNode(new SourcePosition(context.start));

      node.Left = Visit(context.left) as ExpressionNode;
      node.Right = Visit(context.right) as ExpressionNode;

      return node;
    }

    // IS
    public override ASTNode VisitInfixIs(CLUBSParser.InfixIsContext context) {
      IsNode node = new IsNode(new SourcePosition(context.start));

      node.Left = Visit(context.left) as ExpressionNode;
      node.Right = Visit(context.right) as ExpressionNode;

      return node;
    }

    // Infix comparison (greater than / less than)
    public override ASTNode VisitInfixComparison(CLUBSParser.InfixComparisonContext context) {
      SourcePosition sourcePosition = new SourcePosition(context.start);
      InfixExpressionNode node;

      // Get the correct comparison operator by switching on the token
      switch(context.op.Type) {
        case CLUBSLexer.GT:
          node = new GreaterThanNode(sourcePosition);
          break;

        case CLUBSLexer.LT:
          node = new LessThanNode(sourcePosition);
          break;

        default:
          throw new Exception("Operator not found");
      }

      node.Left = Visit(context.left) as ExpressionNode;
      node.Right = Visit(context.right) as ExpressionNode;

      return node;
    }

    // Query (WHERE ...)
    public override ASTNode VisitQuery(CLUBSParser.QueryContext context) {
      QueryNode node = new QueryNode(new SourcePosition(context.start));

      node.infixExpression = Visit(context.infixBooleanExpr()) as InfixExpressionNode;

      return node;
    }

    // Dot notation id
    public override ASTNode VisitIdDotReference(CLUBSParser.IdDotReferenceContext context) {
      return Visit(context.idExpr());
    }

    // .Count
    public override ASTNode VisitCountDotReference(CLUBSParser.CountDotReferenceContext context) {
      return new CountNode(new SourcePosition(context.start));
    }
  }
}