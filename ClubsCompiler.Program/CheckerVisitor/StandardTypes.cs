using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  public class StandardTypes {
    public static BoolTypeNode Bool { get; set; }
    public static CardTypeNode Card { get; set; }
    public static CardValueTypeNode CardValue { get; set; }
    public static IntTypeNode Int { get; set; }
    public static PlayerTypeNode Player { get; set; }
    public static StringTypeNode String { get; set; }
    public static SetTypeNode Set { get; set; }

    public static SetTypeNode GetSetType(TypeNode type) {
      return new SetTypeNode(type, null);
    }
  }
}