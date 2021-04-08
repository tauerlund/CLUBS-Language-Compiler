namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a an entry in a symbol table consisting of a name, type and scope level.
  /// </summary>
  public class Symbol {
    public string Name { get; set; }
    public TypeNode Type { get; set; }
    public int ScopeLevel { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Symbol"/> class.
    /// </summary>
    /// <param name="name">The name of the symbol.</param>
    /// <param name="type">The type of the symbol.</param>
    /// <param name="scopeLevel">The scope level of the symbol.</param>
    public Symbol(string name, TypeNode type, int scopeLevel) {
      Name = name;
      Type = type;
      ScopeLevel = scopeLevel;
    }
  }
}