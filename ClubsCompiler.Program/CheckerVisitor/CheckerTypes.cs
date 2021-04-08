using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  public partial class Checker : ASTVisitor<TypeNode> {

    public override TypeNode Visit(BoolTypeNode node, object obj) {
      return node;
    }

    public override TypeNode Visit(CardTypeNode node, object obj) {
      return node;
    }

    public override TypeNode Visit(CardValueTypeNode node, object obj) {
      return node;
    }

    public override TypeNode Visit(IntTypeNode node, object obj) {
      return node;
    }

    public override TypeNode Visit(PlayerTypeNode node, object obj) {
      return node;
    }

    public override TypeNode Visit(SetTypeNode node, object obj) {
      node.Type = Visit(node.Type);
      return node;
    }
  }
}