using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  public partial class Checker : ASTVisitor<TypeNode> {

    // Block
    public override TypeNode Visit(BlockNode node, object obj) {
      SymbolTable.OpenScope();

      node.Statements.ForEach(x => Visit(x));

      SymbolTable.CloseScope();
      return null;
    }

    // Declaration
    public override TypeNode Visit(DeclarationNode node, object obj) {
      Symbol duplicate = SymbolTable.RetrieveSymbol(node.Id.Text);

      // If declaration has assignment, get the type of the assignment
      if (node.AssignmentExpression != null) {
        TypeNode assignmentType = Visit(node.AssignmentExpression, node.Type);

        // If assignment type is not the same type as the declaration, log error
        if (assignmentType != node.Type) {
          ErrorLogger.LogError(new IncompatibleTypesError(assignmentType, node.Type, node.AssignmentExpression.SourcePosition));
          return new ErrorTypeNode(node.SourcePosition);
        }
      }

      // Enter in symbol table if not a duplicate, else log error
      if (duplicate == null) {
        SymbolTable.EnterSymbol(node.Id.Text, node.Type);
      }
      else {
        ErrorLogger.LogError(new VariableAlreadyDeclaredError(node.Id.Text, node.Id.SourcePosition));
        node.Type = new ErrorTypeNode(node.SourcePosition);
      }
      return node.Type;
    }

    // Assignment
    public override TypeNode Visit(AssignmentNode node, object obj) {
      TypeNode leftType = Visit(node.Left);
      TypeNode rightType = Visit(node.Right, leftType);

      // If left and right types are not the same, log error
      if (leftType != rightType) {
        ErrorLogger.LogError(new IncompatibleTypesError(leftType, rightType, node.Right.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      return null;
    }

    // FORALL
    public override TypeNode Visit(ForAllNode node, object obj) {
      node.Parent.Type = Visit(node.Parent);

      // Open local scope for FORALL node, so child only exists within this scope
      SymbolTable.OpenScope();

      // Log error if parent type is not Set, else visit child and block
      if (node.Parent.Type != StandardTypes.Set) {
        ErrorLogger.LogError(new ExpectedTypeError(node, StandardTypes.Set, node.Parent.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }
      else {
        node.Child.Type = (node.Parent.Type as SetTypeNode).Type;
        Visit(node.Child);
        Visit(node.Block);
      }

      SymbolTable.CloseScope(); // Close local scope

      return null;
    }

    // IF
    public override TypeNode Visit(IfNode node, object obj) {
      TypeNode predicateType = Visit(node.Predicate);

      // If predicate type is not Bool, log error
      if (predicateType != StandardTypes.Bool) {
        ErrorLogger.LogError(new IncompatibleTypesError(predicateType, StandardTypes.Bool, node.Predicate.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }

      Visit(node.Block);

      // Visit each ELSE IF in the chain
      foreach (ElseIfNode elseIf in node.ElseIfChain) {
        Visit(elseIf);
      }

      // Visit ELSE block, if has any
      if (node.ElseBlock != null) {
        Visit(node.ElseBlock);
      }

      return null;
    }

    // ELSE IF
    public override TypeNode Visit(ElseIfNode node, object obj) {
      TypeNode predicateType = Visit(node.Predicate);

      // If predicate type is not Bool, log error
      if (predicateType != StandardTypes.Bool) {
        ErrorLogger.LogError(new IncompatibleTypesError(predicateType, StandardTypes.Bool, node.Predicate.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }
      Visit(node.Block);

      return null;
    }

    // WHILE
    public override TypeNode Visit(WhileNode node, object obj) {
      TypeNode predicateType = Visit(node.Predicate);

      // If predicate type is not Bool, log error
      if (predicateType != StandardTypes.Bool) {
        ErrorLogger.LogError(new ExpectedTypeError(node, StandardTypes.Bool, node.Predicate.SourcePosition));
        return new ErrorTypeNode(node.SourcePosition);
      }
      Visit(node.Block);

      return null;
    }
  }
}