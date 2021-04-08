using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  public partial class CodeGenerator : ASTVisitor<string> {

    public override string Visit(BoolTypeNode node, object obj) {
      return "bool";
    }

    public override string Visit(CardTypeNode node, object obj) {
      return "Card";
    }

    public override string Visit(CardValueTypeNode node, object obj) {
      return "CardValue";
    }

    public override string Visit(IntTypeNode node, object obj) {
      return "int";
    }

    public override string Visit(PlayerTypeNode node, object obj) {
      return "Player";
    }

    public override string Visit(SetTypeNode node, object obj) {
      if (node.Type is BaseTypeNode) {
        return $"ComparableList<{node.Type}>";
      }
      else {
        return $"List<{Visit(node.Type)}>";
      }
    }
  }
}