using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Provides methods for generating C# code for a class.
  /// </summary>
  public class ClassWriter {
    private StringBuilder _classBuilder;

    // Builders used to easily separate different code.

    private StringBuilder _propertiesBuilder;
    private StringBuilder _constructorBuilder;
    private StringBuilder _methodsBuilder;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClassWriter"/> class.
    /// </summary>
    /// <param name="name">The name of the class in the generated code. </param>
    public ClassWriter(string name) {
      // Instantiate all builders.
      _classBuilder = new StringBuilder();
      _propertiesBuilder = new StringBuilder();
      _constructorBuilder = new StringBuilder();
      _methodsBuilder = new StringBuilder();

      WriteBaseCode(name);
    }

    // Writes class signature and constructors.
    private void WriteBaseCode(string name) {
      _classBuilder.Append($"public class {name} : BaseType {{\n");
      _constructorBuilder.Append($"public {name}(string name) : this() {{ Name = name; }}\n");
      _constructorBuilder.Append($"public {name}() {{\n");
    }

    /// <summary>
    /// Returns the code generated using this <see cref="ClassWriter"/> as
    /// a string representing a class in C# code.
    /// </summary>
    public string WriteClass() {
      return _classBuilder.ToString() + _propertiesBuilder.ToString() +
        _constructorBuilder.ToString() + "}\n" + _methodsBuilder.ToString() +
        "}\n";
    }

    /// <summary>
    /// Adds a new class property in the code.
    /// </summary>
    /// <param name="type">The property type.</param>
    /// <param name="id">The property identifier</param>
    public void AddClassProperty(string type, string id) {
      _propertiesBuilder.Append($"public {type} {id} {{ get; set; }}\n"); // Write property.
      _constructorBuilder.Append($"{id} = new {type}();\n"); // Instantiate in constructor.
    }

    public void AddPropertyCustomGet(string type, string id, string getter) {
      _propertiesBuilder.Append($"public {type} {id} {{ get {{ {getter} }} }}");
    }

    public void AddPrimitiveProperty(string type, string id) {
      _propertiesBuilder.Append($"public {type} {id} {{ get; set; }}\n"); // Write property.
    }

    /// <summary>
    /// Adds a new method in the code.
    /// </summary>
    /// <param name="returnType">The method return type.</param>
    /// <param name="name">The method name.</param>
    /// <param name="body">The method body.</param>
    /// <param name="parameters">The method parameters.</param>
    public void AddMethod(string returnType, string name, string body, params string[] parameters) {
      _methodsBuilder.Append($"public {returnType} {name}({string.Join(", ", parameters)}) {{\n"); // Write method signature.
      _methodsBuilder.Append($"{body}\n}}"); // Write method body.
    }
  }
}