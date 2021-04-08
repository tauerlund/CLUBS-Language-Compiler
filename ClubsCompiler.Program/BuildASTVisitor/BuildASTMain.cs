using Antlr4.Runtime;
using ClubsCompiler.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mime;
using System.IO;
using Antlr4.Runtime.Misc;
using System.Security.Cryptography.X509Certificates;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Provides methods to build an Abstract Syntax Tree from a given <see cref="ParserRuleContext" />
  /// representing a Concrete Syntax Tree.
  /// </summary>
  public partial class BuildASTVisitor : CLUBSBaseVisitor<ASTNode> {

    /// <summary>
    /// Creates an AST from the given CST (<see cref="ParserRuleContext"/>).
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override ASTNode VisitProg(CLUBSParser.ProgContext context) {
      ProgNode node = new ProgNode(new SourcePosition(context.start));

      node.Children.Add(Visit(context.setupBlock));

      return node;
    }

    // HELPER METHODS

    public TypeNode GetTypeNode(int typeValue, SourcePosition sourcePosition) {
      switch(typeValue) {
        case CLUBSLexer.CARD:
          return new CardTypeNode(sourcePosition);

        case CLUBSLexer.CARDVALUE:
          return new CardValueTypeNode(sourcePosition);

        case CLUBSLexer.PLAYER:
          return new PlayerTypeNode(sourcePosition);

        case CLUBSLexer.INT:
          return new IntTypeNode(sourcePosition);

        case CLUBSLexer.BOOL:
          return new BoolTypeNode(sourcePosition);

        case CLUBSLexer.STRING:
          return new StringTypeNode(sourcePosition);

        default:
          throw new NotSupportedException();
      }
    }
  }
}