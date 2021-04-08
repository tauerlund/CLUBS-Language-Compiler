using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Translates an AST into C# code.
  /// </summary>
  public partial class CodeGenerator : ASTVisitor<string> {
    public SymbolTable SymbolTable { get; }
    private CodeWriter _codeWriter;

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeGenerator"/> class.
    /// </summary>
    public CodeGenerator() { // TODO: Get symbol table from checker and reset scope?
      SymbolTable = new SymbolTable();
    }

    /// <summary>
    /// Generates C# code based on the given <see cref="ASTNode"/>.
    /// </summary>
    /// <param name="node">The AST.</param>
    public string GenerateCode(ASTNode node) {
      _codeWriter = new CodeWriter();

      _codeWriter.Emit(Visit(node));

      return _codeWriter.GenerateCode();
    }

    public override string Visit(ProgNode node, object obj) {
      StringBuilder builder = new StringBuilder();
      node.Children.ForEach(child => builder.Append(Visit(child)));
      return builder.ToString();
    }
  }
}