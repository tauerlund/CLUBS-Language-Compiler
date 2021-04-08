using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  public partial class CodeGenerator : ASTVisitor<string> {

    // OWNS
    public override string Visit(OwnsActionNode node, object obj) {
      foreach (DeclarationNode ownedObjectDcl in node.OwnedObjectDcls) {
        string typeString = Visit(ownedObjectDcl.Type);

        // Add a property to the appropriate class for every owned object
        _codeWriter.AddProperty(node.OwnerType, typeString, ownedObjectDcl.Id.ToString());
      }
      return null;
    }

    // PUT
    public override string Visit(PutActionNode node, object obj) {
      StringBuilder builder = new StringBuilder();

      builder.Append($"{Visit(node.Target)}.AddRange({Visit(node.Source)});\n");
      builder.Append($"{Visit(node.Source)}.Clear();\n");

      return builder.ToString();
    }

    // TAKE
    public override string Visit(TakeActionNode node, object obj) {
      StringBuilder builder = new StringBuilder();

      builder.Append("{\n");
      builder.Append($"var _tempList = {Visit(node.Source)}.Take({Visit(node.Quantity)}).ToList();\n");
      builder.Append($"{Visit(node.Target)}.AddRange(_tempList);\n");
      builder.Append($"_tempList.ForEach(x => {Visit(node.Source)}.Remove(x));\n");
      builder.Append("}\n");

      return builder.ToString();
    }

    // TAKE AT
    public override string Visit(TakeAtActionNode node, object obj) {
      StringBuilder builder = new StringBuilder();

      builder.Append("{\n");
      builder.Append($"var _tempList = {Visit(node.Source)}.Skip({Visit(node.Index)}).Take({Visit(node.Quantity)}).ToList();\n");
      builder.Append($"{Visit(node.Target)}.AddRange(_tempList);\n");
      builder.Append($"_tempList.ForEach(x => {Visit(node.Source)}.Remove(x));\n");
      builder.Append("}\n");

      return builder.ToString();
    }

    // TAKE WHERE
    public override string Visit(TakeWhereActionNode node, object obj) {
      StringBuilder builder = new StringBuilder();

      builder.Append("{\n");
      builder.Append($"var _tempList = {Visit(node.Source)}.Where(x => {Visit(node.Query)}).Take({Visit(node.Quantity)}).ToList();\n");
      builder.Append($"{Visit(node.Target)}.AddRange(_tempList);\n");
      builder.Append($"_tempList.ForEach(x => {Visit(node.Source)}.Remove(x));\n");
      builder.Append("}\n");

      return builder.ToString();
    }

    // PRINT
    public override string Visit(PrintActionNode node, object obj) {
      StringBuilder builder = new StringBuilder();
      builder.Append($"Console.WriteLine(");

      // Append all content
      foreach (ASTNode child in node.Content) {
        if (child is ReferenceNode childReference && childReference.Type is SetTypeNode) {
          builder.Append($"string.Join(\", \", {Visit(child)})"); // If type is Set, use String.Join
        }
        else {
          builder.Append(Visit(child));
        }

        if (child != node.Content.Last()) {
          builder.Append(" + ");
        }
      }

      builder.Append(");\n");
      return builder.ToString();
    }
  }
}