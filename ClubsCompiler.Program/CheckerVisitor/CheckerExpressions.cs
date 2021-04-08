using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  public partial class Checker : ASTVisitor<TypeNode> {

    // Integer literal
    public override TypeNode Visit(IntegerLiteral node, object obj) {
      return new IntTypeNode(int.Parse(node.Text), node.SourcePosition);
    }

    // Bool literal
    public override TypeNode Visit(BoolLiteral node, object obj) {
      return new BoolTypeNode(node.SourcePosition);
    }

    // Set value expression
    public override TypeNode Visit(SetValueNode node, object obj) {
      TypeNode leftType;
      // Get the type of the node passed as parameter, must be Set type
      if (obj is SetTypeNode leftSet) {
        leftType = leftSet.Type; // Get the element type of the Set
      }
      else { // If not Set type, log error
        ErrorLogger.LogError(new ExpectedTypeError(node, StandardTypes.Set, node.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      foreach (IdentifierNode id in node.Ids) {
        Symbol symbol = SymbolTable.RetrieveSymbol(id.Text);

        // Declare the id if not already declared, else check whether type is compatible
        if (symbol == null) {
          SymbolTable.EnterSymbol(id.Text, leftType);
        }
        else if (symbol.Type != leftType) { // If compatible, log error
          ErrorLogger.LogError(new IncompatibleTypesError(symbol.Type, leftType, id.SourcePosition));
          return new ErrorTypeNode(node.SourcePosition);
        }
      }
      node.Type = new SetTypeNode(leftType, node.SourcePosition);

      // Save the element count in the Type for later use
      (node.Type as SetTypeNode).ElementCount = node.Ids.Count;
      (obj as SetTypeNode).ElementCount = node.Ids.Count;
      return node.Type;
    }

    // Variable reference
    public override TypeNode Visit(ReferenceNode node, object obj) {
      Symbol symbol = SymbolTable.RetrieveSymbol(node.Id.Text);

      // If variable not declared, log error
      if (symbol == null) {
        ErrorLogger.LogError(new UndeclaredVariableError(node.Id.Text, node.Id.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }
      else {
        node.Type = symbol.Type;
        return node.Type;
      }
    }

    // Dot notation reference
    public override TypeNode Visit(DotReferenceNode node, object obj) {
      node.Parent.Type = Visit(node.Parent);
      node.Member.Type = Visit(node.Member);

      node.Type = node.Member.Type; // Assign the type of the member to the DotReferenceNode

      // If DotReferenceNode is CountNode, parent must be of type Int
      if (node.Member is CountNode) {
        if (node.Parent.Type != StandardTypes.Set) { // Log error
          ErrorLogger.LogError(new ExpectedTypeError(node.Member, StandardTypes.Set, node.Parent.SourcePosition));
          return new ErrorTypeNode(node.Parent.SourcePosition);
        }
      }

      return node.Type;
    }

    // .Count
    public override TypeNode Visit(CountNode node, object obj) {
      return new IntTypeNode(node.SourcePosition);
    }

    // Deck value expression
    public override TypeNode Visit(DeckValueNode node, object obj) {
      SourcePosition sourcePosition = node.SourcePosition;

      SetTypeNode cardValueSet = StandardTypes.GetSetType(StandardTypes.CardValue);

      int count = 1;

      // Go through all identifiers in the deck for errors
      foreach (IdentifierNode id in node.Ids) {
        Symbol symbol = SymbolTable.RetrieveSymbol(id.Text);
        // If id has not been declared or the reference type is not Set OF CardValue, log error
        if (symbol == null) {
          ErrorLogger.LogError(new UndeclaredVariableError(id.Text, id.SourcePosition));
          return new ErrorTypeNode(node.SourcePosition);
        }
        else if (symbol.Type != cardValueSet) {
          ErrorLogger.LogError(new ExpectedTypeError(node, cardValueSet, id.SourcePosition));
          return new ErrorTypeNode(node.SourcePosition);
        }
        count *= (symbol.Type as SetTypeNode).ElementCount; // Get total number of cards in deck
      }

      node.Type = new SetTypeNode(new CardTypeNode(sourcePosition), sourcePosition);
      node.ElementCount = count; // Save the number of cards in the type for later use

      return node.Type;
    }

    // String
    public override TypeNode Visit(StringLiteral node, object obj) {
      return new StringTypeNode(node.SourcePosition);
    }

    // IS
    public override TypeNode Visit(IsNode node, object obj) {
      TypeNode leftType = Visit(node.Left);
      TypeNode rightType = Visit(node.Right);

      // If left and right types are not the same, log error
      if (leftType != rightType) {
        ErrorLogger.LogError(new IncompatibleTypesError(leftType, rightType, node.Left.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      return new BoolTypeNode(node.SourcePosition);
    }

    // AND
    public override TypeNode Visit(AndNode node, object obj) {
      return VisitInfixBoolExpression(node); // Call generic infix boolean visit method
    }

    // OR
    public override TypeNode Visit(OrNode node, object obj) {
      return VisitInfixBoolExpression(node); // Call generic infix boolean visit method
    }

    // Greater than operator
    public override TypeNode Visit(GreaterThanNode node, object obj) {
      return VisitComparisonNode(node); // Call generic comparison visit method
    }

    // Less than operator
    public override TypeNode Visit(LessThanNode node, object obj) {
      return VisitComparisonNode(node); // Call generic comparison visit method
    }

    // Addition operator
    public override TypeNode Visit(AdditionNode node, object obj) {
      return VisitInfixArithmeticExpression(node); // Call generic infix arithmetic expression visit method
    }

    // Subtraction operator
    public override TypeNode Visit(SubtractionNode node, object obj) {
      return VisitInfixArithmeticExpression(node); // Call generic infix arithmetic expression visit method
    }

    // Multiplication operator
    public override TypeNode Visit(MultiplicationNode node, object obj) {
      return VisitInfixArithmeticExpression(node); // Call generic infix arithmetic expression visit method
    }

    // Division operator
    public override TypeNode Visit(DivisionNode node, object obj) {
      return VisitInfixArithmeticExpression(node); // Call generic infix arithmetic expression visit method
    }

    // Query (WHERE ... )
    public override TypeNode Visit(QueryNode node, object obj) {
      TypeNode infixType = Visit(node.infixExpression);

      // If the infix expression is not Bool, log error
      if (infixType != StandardTypes.Bool) {
        ErrorLogger.LogError(new ExpectedTypeError(StandardTypes.Bool, node.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      return new BoolTypeNode(node.SourcePosition);
    }

    // RANDOM
    public override TypeNode Visit(RandomNode node, object obj) {
      TypeNode lowerLimitType = Visit(node.LowerLimit);
      TypeNode upperLimitType = Visit(node.UpperLimit);

      // If lower limit type is not Int, log error
      if (lowerLimitType != StandardTypes.Int) {
        ErrorLogger.LogError(new ExpectedTypeError(StandardTypes.Int, node.LowerLimit.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      // If upper limit type is not Int, log error
      if (upperLimitType != StandardTypes.Int) {
        ErrorLogger.LogError(new ExpectedTypeError(StandardTypes.Int, node.UpperLimit.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      return new IntTypeNode(node.SourcePosition);
    }

    // Card value expression
    public override TypeNode Visit(CardValueExpressionNode node, object obj) {
      TypeNode parentType = Visit(node.Parent);
      TypeNode childType = Visit(node.Child);

      TypeNode CardValueSetType = StandardTypes.GetSetType(StandardTypes.CardValue);

      // If parent type is not Set OF CardValue, log error
      if (parentType != CardValueSetType) {
        ErrorLogger.LogError(new ExpectedTypeError(CardValueSetType, node.Parent.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      // If parent type is not CardValue, log error
      if (childType != StandardTypes.CardValue) {
        ErrorLogger.LogError(new ExpectedTypeError(StandardTypes.CardValue, node.Child.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      return new CardTypeNode(node.SourcePosition);
    }

    // HELPER METHODS

    // Generic visitor for infix boolean expressions
    private TypeNode VisitInfixBoolExpression(InfixExpressionNode node) {
      TypeNode leftType = Visit(node.Left);
      TypeNode rightType = Visit(node.Right);

      // If left type is not a Bool, log error
      if (leftType != StandardTypes.Bool) {
        ErrorLogger.LogError(new CannotUseWithOperatorError(leftType, node, node.Left.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      // If right type is not a Bool, log error
      if (rightType != StandardTypes.Bool) {
        ErrorLogger.LogError(new CannotUseWithOperatorError(rightType, node, node.Left.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      return new BoolTypeNode(node.SourcePosition);
    }

    // Generic visitor for comparison expressions
    private TypeNode VisitComparisonNode(InfixExpressionNode node) {
      TypeNode leftType = Visit(node.Left);
      TypeNode rightType = Visit(node.Left);

      // If left type is Bool or String, log error
      if (leftType == StandardTypes.Bool || leftType == StandardTypes.String) {
        ErrorLogger.LogError(new CannotUseWithOperatorError(leftType, node, node.Left.SourcePosition));
        return new ErrorTypeNode(node.Left.SourcePosition);
      }

      // If right type is Bool or String, log error
      if (rightType == StandardTypes.Bool || rightType == StandardTypes.String) {
        ErrorLogger.LogError(new CannotUseWithOperatorError(rightType, node, node.Left.SourcePosition));
        return new ErrorTypeNode(node.Left.SourcePosition);
      }

      return new BoolTypeNode(node.SourcePosition);
    }

    // Generic visitor for infix arithmethic expressions
    private TypeNode VisitInfixArithmeticExpression(InfixOperatorNode node) {
      TypeNode leftType = Visit(node.Left);
      TypeNode rightType = Visit(node.Right);

      // If left type is not Int, log error
      if (leftType != StandardTypes.Int) {
        ErrorLogger.LogError(new CannotUseWithOperatorError(leftType, node, node.Left.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      // If right type is not Int, log error
      if (rightType != StandardTypes.Int) {
        ErrorLogger.LogError(new CannotUseWithOperatorError(rightType, node, node.Right.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }
      return new IntTypeNode(node.SourcePosition);
    }
  }
}