using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  public partial class CodeGenerator : ASTVisitor<string> {

    // Block
    public override string Visit(BlockNode node, object obj) {
      StringBuilder builder = new StringBuilder();

      SymbolTable.OpenScope();

      builder.Append("{\n");
      node.Statements.ForEach(stmt => builder.Append(Visit(stmt))); // Append all statements
      builder.Append("}\n");

      SymbolTable.CloseScope();

      return builder.ToString();
    }

    // Assignment
    public override string Visit(AssignmentNode node, object obj) {
      IdentifiableNode id = node.Left as IdentifiableNode;
      return $"{Visit(node.Left, id.Id)} = {Visit(node.Right, id.Id)};\n";
    }

    // Declaration
    public override string Visit(DeclarationNode node, object obj) {
      StringBuilder builder = new StringBuilder();

      // Append declaration of variable with a type-based default initialization
      builder.Append($"{Visit(node.Type)} {node.Id} = {node.Type.GetInitializationString(node.Id.Text)}");

      // If declaration has assignment, append new statement with assignment to the newly declared variable
      if (node.AssignmentExpression != null) {
        builder.Append($"{node.Id} = {Visit(node.AssignmentExpression, node.Id)};\n");
      }

      SymbolTable.EnterSymbol(node.Id.Text, node.Type);

      return builder.ToString();
    }

    // FORALL
    public override string Visit(ForAllNode node, object obj) {
      StringBuilder builder = new StringBuilder();

      // Counter name is scope-level dependent, so as to avoid name conflicts in nested scopes
      string i = $"i_{SymbolTable.ScopeLevelCounter}";

      builder.Append($"for(int {i} = 0; {i} < {Visit(node.Parent)}.Count; {i}++)\n");
      builder.Append($"{{\n var {node.Child.Id} = {Visit(node.Parent)}[{i}];\n");
      builder.Append($"{Visit(node.Block)}");
      builder.Append($"}}");

      return builder.ToString();
    }

    // IF
    public override string Visit(IfNode node, object obj) {
      StringBuilder builder = new StringBuilder();

      builder.Append($"if ({Visit(node.Predicate)})\n");
      builder.Append(Visit(node.Block));

      // Append all ELSE IF statements, if any
      foreach (ElseIfNode elseIf in node.ElseIfChain) {
        builder.Append(Visit(elseIf));
      }

      // Append ELSE statement, if has
      if (node.ElseBlock != null) {
        builder.Append($"else {Visit(node.ElseBlock)}");
      }

      return builder.ToString();
    }

    // ELSE IF
    public override string Visit(ElseIfNode node, object obj) {
      return $"else if ({Visit(node.Predicate)}) {Visit(node.Block)}";
    }

    // WHILE
    public override string Visit(WhileNode node, object obj) {
      return $"while ({Visit(node.Predicate)}) {Visit(node.Block)}";
    }
  }
}