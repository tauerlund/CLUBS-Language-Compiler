using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Provides methods for creating and maintaining a symbol table including entering and retrieving
  /// a symbol from the table. Each table contains all symbols defined in the current scope level.
  /// </summary>
  public class SymbolTable {
    private List<Hashtable> _hashTables;

    public int ScopeLevelCounter { get; private set; }

    public SymbolTable() {
      _hashTables = new List<Hashtable>();
    }

    /// <summary>
    /// Opens a new scope in the program.
    /// </summary>
    public void OpenScope() {
      ScopeLevelCounter++;
      _hashTables.Add(new Hashtable());
    }

    /// <summary>
    /// Closes the latest scope in the program.
    /// </summary>
    public void CloseScope() {
      ScopeLevelCounter--;
      _hashTables.RemoveAt(_hashTables.Count - 1);
    }

    /// <summary>
    /// Enters the desired symbol in the symbol table.
    /// </summary>
    /// <param name="name">The name of the symbol.</param>
    /// <param name="type">The type of the symbol.</param>
    public void EnterSymbol(string name, TypeNode type) {
      if (RetrieveSymbol(name) != null) {
        // If symbol already found, throw exception as this should be prevented by the Checker class
        throw new Exception("Symbol already found in symbol table.");
      }
      else {
        // If not in symbol table, add it
        _hashTables.Last().Add(name, new Symbol(name, type, ScopeLevelCounter));
      }
    }

    /// <summary>
    /// Retrieves the desired symbol from the symbol table.
    /// </summary>
    /// <param name="name">The name of the symbol to retrieve.</param>
    public Symbol RetrieveSymbol(string name) {
      foreach (Hashtable table in _hashTables) {
        if (table[name] != null) {
          return table[name] as Symbol;
        }
      }
      return null;
    }

    // DELETE?
    public void PrintTable() {
      foreach (Hashtable table in _hashTables) {
        Console.WriteLine("\n\nNew table:");
        foreach (DictionaryEntry e in table) {
          Symbol s = e.Value as Symbol;

          Console.WriteLine($"Name: {s.Name} Type: {s.Type} Level: {s.ScopeLevel}");
        }
      }
    }
  }
}