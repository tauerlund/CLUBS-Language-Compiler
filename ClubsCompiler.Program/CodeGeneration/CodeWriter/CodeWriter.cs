using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Provides methods for generating C# source code for the ACE language.
  /// </summary>
  public class CodeWriter {
    private StringBuilder _baseBuilder;

    // Separate builders for each base class

    private ClassWriter _playerWriter;
    private ClassWriter _cardValueWriter;
    private ClassWriter _cardWriter;

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeWriter"/> class
    /// and sets up base code.
    /// </summary>
    public CodeWriter() {
      _baseBuilder = new StringBuilder();
      BuildBaseCode();
    }

    /// <summary>
    /// Returns the generated C# code.
    /// </summary>
    /// <returns></returns>
    public string GenerateCode() {
      Emit("Console.ReadKey();\n");
      Emit("}\n}\n");
      BuildClasses();
      Emit("}\n");
      return _baseBuilder.ToString();
    }

    /// <summary>
    /// Emits the received string as C# code.
    /// </summary>
    /// <param name="code">The code to emit.</param>
    public void Emit(string code) {
      _baseBuilder.Append(code);
    }

    /// <summary>
    /// Adds a public property to the chosen type (class).
    /// </summary>
    /// <param name="type">The type (class) to add the property to.</param>
    /// <param name="propertyType">The type of the desired property.</param>
    /// <param name="name">The name of the desired property.</param>
    public void AddProperty(TypeNode type, string propertyType, string name) {
      switch (type) {
        case PlayerTypeNode p:
          _playerWriter.AddClassProperty(propertyType, name);
          break;

        case CardValueTypeNode cv:
          _cardValueWriter.AddClassProperty(propertyType, name);
          break;

        case CardTypeNode c:
          _cardWriter.AddClassProperty(propertyType, name);
          break;

        default:
          throw new Exception("Unknown type.");
      }
    }

    private void BuildBaseCode() {
      _baseBuilder.Append("using System;\n");
      _baseBuilder.Append("using System.Collections.Generic;\n");
      _baseBuilder.Append("using System.Linq;\n");
      _baseBuilder.Append("namespace ACE {\n");
      _baseBuilder.Append("public class Program {\n");
      _baseBuilder.Append("private static void Main(string[] args) {");
      _baseBuilder.Append("Random _random = new Random();\n");

      _playerWriter = new ClassWriter("Player");
      _cardValueWriter = new ClassWriter("CardValue");
      _cardWriter = new ClassWriter("Card");
    }

    private void BuildClasses() {
      _baseBuilder.Append("public class ComparableList<T> : List<T> where T : BaseType { " +
                          "public static bool operator >(ComparableList<T> value1, ComparableList<T> value2) { return value1.Select(x => x.Order).Sum() > value2.Select(x => x.Order).Sum(); }" +
                          "public static bool operator <(ComparableList<T> value1, ComparableList<T> value2) { return value1.Select(x => x.Order).Sum() < value2.Select(x => x.Order).Sum(); } }");

      _baseBuilder.Append("public abstract class BaseType { public string Name { get; set; } public string Parent { get; set; } public virtual int Order { get; set; } ");

      _baseBuilder.Append("public static bool operator >(BaseType value1, BaseType value2) { return value1.Order > value2.Order; }\n");
      _baseBuilder.Append("public static bool operator <(BaseType value1, BaseType value2) { return value1.Order < value2.Order; }\n");
      _baseBuilder.Append("public override string ToString() { return Name; } }\n");

      _cardWriter.AddClassProperty("List<CardValue>", "CardValues");
      _cardWriter.AddPropertyCustomGet("override int", "Order", "return CardValues.Select(x => x.Order).Sum();");
      _cardWriter.AddMethod("override string", "ToString", "return string.Join(\"/\", CardValues);\n");

      _cardWriter.AddMethod("static bool", "operator >", "return card1.CardValues.Select(x => x.Order).Sum() > card2.CardValues.Select(x => x.Order).Sum();\n", "Card card1", "Card card2");
      _cardWriter.AddMethod("static bool", "operator <", "return card1.CardValues.Select(x => x.Order).Sum() < card2.CardValues.Select(x => x.Order).Sum();\n", "Card card1", "Card card2");

      _baseBuilder.Append(_playerWriter.WriteClass());
      _baseBuilder.Append(_cardValueWriter.WriteClass());
      _baseBuilder.Append(_cardWriter.WriteClass());
    }
  }
}