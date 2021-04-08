using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  public partial class CodeGenerator : ASTVisitor<string> {

    // Variable reference
    public override string Visit(ReferenceNode node, object obj) {
      return $"{node.Id}";
    }

    // Dot notation reference
    public override string Visit(DotReferenceNode node, object obj) {
      return $"{Visit(node.Parent)}.{Visit(node.Member)}";
    }

    // Set value expression
    public override string Visit(SetValueNode node, object obj) {
      StringBuilder builder = new StringBuilder();

      // Get the name of the node passed as parameter
      IdentifierNode setId = obj as IdentifierNode;

      builder.Append($"{setId};\n");
      builder.Append($"{setId}.Clear();\n");

      foreach (IdentifierNode id in node.Ids) {
        // If identifiers are not already declared, new instances will be declared
        if (SymbolTable.RetrieveSymbol(id.Text) == null) {
          string typeString = Visit((node.Type as SetTypeNode).Type);
          builder.Append($"{typeString} {id} = new {typeString}");

          // Base types (Player, CardValue, Card) are declared with a name in constructor
          TypeNode type = (node.Type as SetTypeNode).Type;
          if (type is BaseTypeNode) {
            builder.Append($"(\"{id}\");\n");
          }
          else {
            builder.Append($"();\n");
          }

          SymbolTable.EnterSymbol(id.Text, type);
        }
      }

      int order = 0; // Number representing where the node is placed in the hierarchy
      foreach (OrderedIdentifierNode id in node.Ids) {
        builder.Append($"{id}.Order = {order};\n");
        builder.Append($"{id}.Parent = \"{setId}\";\n");
        builder.Append($"{setId}.Add({id});\n");
        if (id.Order == Order.LT) {
          order++;
        }
      }
      return builder.ToString();
    }

    // Deck value expression
    public override string Visit(DeckValueNode node, object obj) {
      StringBuilder builder = new StringBuilder();

      // Get the name of the node passed as parameter
      IdentifierNode setId = obj as IdentifierNode;

      builder.Append($"{setId};\n");
      builder.Append($"{setId}.Clear();\n");

      int counter = 0;

      foreach (IdentifierNode id in node.Ids) {
        counter++;

        // The limit condition is retrieved from the type in the symbol table
        int limit = (SymbolTable.RetrieveSymbol(id.Text).Type as SetTypeNode).ElementCount;
        string j = $"j_{counter}";
        builder.Append($"for(int {j} = 0; {j} < {limit}; {j}++) {{\n ");
      }

      builder.Append($"Card _card = new Card();\n");

      counter = 0;

      foreach (IdentifierNode id in node.Ids) {
        counter++;

        string j = $"j_{counter}";
        builder.Append($"_card.CardValues.Add({id}[{j}]);\n");
      }
      builder.Append($"{setId}.Add(_card);\n");

      foreach (IdentifierNode id in node.Ids) {
        builder.Append("}\n");
      }

      return builder.ToString();
    }

    // Integer literal
    public override string Visit(IntegerLiteral node, object obj) {
      return node.Text;
    }

    // Bool literal
    public override string Visit(BoolLiteral node, object obj) {
      return node.Text;
    }

    // String literal
    public override string Visit(StringLiteral node, object obj) {
      return node.Text;
    }

    // .Count
    public override string Visit(CountNode node, object obj) {
      return "Count";
    }

    // IS
    public override string Visit(IsNode node, object obj) {
      // Different behavior in Query, as this compares the underlying CardValues in a Card
      if (obj is QueryNode) {
        return $"x.CardValues.Any(y => y.Parent == \"{Visit(node.Left)}\" && y.Name == \"{Visit(node.Right)}\")\n";
      }
      else { // If not a query, append regular equality comparison operator
        return $"{Visit(node.Left)} == {Visit(node.Right)}";
      }
    }

    // AND
    public override string Visit(AndNode node, object obj) {
      return $"{Visit(node.Left, obj)} && {Visit(node.Right, obj)}";
    }

    // OR
    public override string Visit(OrNode node, object obj) {
      return $"{Visit(node.Left, obj)} || {Visit(node.Right, obj)}";
    }

    // Greater than operator
    public override string Visit(GreaterThanNode node, object obj) {
      return $"{Visit(node.Left)} > {Visit(node.Right)}";
    }

    // Less than operator
    public override string Visit(LessThanNode node, object obj) {
      return $"{Visit(node.Left)} < {Visit(node.Right)}";
    }

    // Query (WHERE ... )
    public override string Visit(QueryNode node, object obj) {
      return $"{Visit(node.infixExpression, node)}";
    }

    // Addition operator
    public override string Visit(AdditionNode node, object obj) {
      return $"{Visit(node.Left)} + {Visit(node.Right)}";
    }

    // Subtraction operator
    public override string Visit(SubtractionNode node, object obj) {
      return $"{Visit(node.Left)} - {Visit(node.Right)}";
    }

    // Multiplication operator
    public override string Visit(MultiplicationNode node, object obj) {
      return $"{Visit(node.Left)} * {Visit(node.Right)}";
    }

    // Division operator
    public override string Visit(DivisionNode node, object obj) {
      return $"{Visit(node.Left)} / {Visit(node.Right)}";
    }

    // RANDOM
    public override string Visit(RandomNode node, object obj) {
      return $"_random.Next({Visit(node.LowerLimit)}, {Visit(node.UpperLimit)})";
    }

    // Card value expression
    public override string Visit(CardValueExpressionNode node, object obj) {
      StringBuilder builder = new StringBuilder();

      IdentifierNode cardId = obj as IdentifierNode;

      builder.Append($"{cardId};\n");
      builder.Append($"{cardId}.CardValues.Add({Visit(node.Child)});\n");

      return builder.ToString();
    }
  }
}