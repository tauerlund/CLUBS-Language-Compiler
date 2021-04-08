using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents the integer type 'Int' in the ACE language.
  /// </summary>
  public class IntTypeNode : TypeNode {
    public int Value { get; set; }

    public IntTypeNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public IntTypeNode(int value, SourcePosition sourcePosition) : base(sourcePosition) {
      Value = value;
    }

    public override string ToString() {
      return "int";
    }

    public override string GetInitializationString(string id) {
      return "0;\n";
    }
  }
}