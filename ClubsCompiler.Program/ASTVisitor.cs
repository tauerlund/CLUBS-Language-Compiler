using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Requires all derived classes to implement visit methods.
  /// Provides the ability to call the correct visit method for a particular node.
  /// </summary>
  /// <typeparam name="T">The return type of the visit methods.</typeparam>
  public abstract class ASTVisitor<T> {

    public abstract T Visit(ProgNode node, object obj);

    #region Statements

    public abstract T Visit(BlockNode node, object obj);

    public abstract T Visit(DeclarationNode node, object obj);

    public abstract T Visit(AssignmentNode node, object obj);

    public abstract T Visit(IfNode node, object obj);

    public abstract T Visit(ElseIfNode node, object obj);

    public abstract T Visit(WhileNode node, object obj);

    #endregion Statements

    #region Actions

    public abstract T Visit(PrintActionNode node, object obj);

    public abstract T Visit(OwnsActionNode node, object obj);

    public abstract T Visit(PutActionNode node, object obj);

    public abstract T Visit(TakeActionNode node, object obj);

    public abstract T Visit(TakeAtActionNode node, object obj);

    public abstract T Visit(TakeWhereActionNode node, object obj);

    #endregion Actions

    #region Expressions

    public abstract T Visit(SetValueNode node, object obj);

    public abstract T Visit(CardValueExpressionNode node, object obj);

    public abstract T Visit(CountNode node, object obj);

    public abstract T Visit(ReferenceNode node, object obj);

    public abstract T Visit(DeckValueNode node, object obj);

    public abstract T Visit(QueryNode node, object obj);

    public abstract T Visit(IsNode node, object obj);

    public abstract T Visit(AndNode node, object obj);

    public abstract T Visit(OrNode node, object obj);

    public abstract T Visit(GreaterThanNode node, object obj);

    public abstract T Visit(LessThanNode node, object obj);

    public abstract T Visit(ForAllNode node, object obj);

    public abstract T Visit(DotReferenceNode node, object obj);

    public abstract T Visit(StringLiteral node, object obj);

    public abstract T Visit(BoolLiteral node, object obj);

    public abstract T Visit(IntegerLiteral node, object obj);

    public abstract T Visit(AdditionNode node, object obj);

    public abstract T Visit(SubtractionNode node, object obj);

    public abstract T Visit(MultiplicationNode node, object obj);

    public abstract T Visit(DivisionNode node, object obj);

    public abstract T Visit(RandomNode node, object obj);

    #endregion Expressions

    #region Types

    public abstract T Visit(BoolTypeNode node, object obj);

    public abstract T Visit(CardTypeNode node, object obj);

    public abstract T Visit(CardValueTypeNode node, object obj);

    public abstract T Visit(IntTypeNode node, object obj);

    public abstract T Visit(PlayerTypeNode node, object obj);

    public abstract T Visit(SetTypeNode node, object obj);

    #endregion Types

    /// <summary>
    /// Calls the correct visit method for the given node and returns the result.
    /// </summary>
    /// <param name="node">The node to pass the correct visit method.</param>
    /// <param name="obj">An additional object to pass to the correct visit method if necessary.</param>
    /// <returns></returns>
    public T Visit(ASTNode node, object obj = null) {
      // Use the dynamic keyword to call the correct visit method
      return Visit((dynamic)node, obj);
    }
  }
}