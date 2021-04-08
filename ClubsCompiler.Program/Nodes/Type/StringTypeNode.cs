﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsCompiler.Program {

  /// <summary>
  /// Represents a string type.
  /// </summary>
  public class StringTypeNode : TypeNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="StringTypeNode"/> class.
    /// </summary>
    /// <param name="sourcePosition">The source position of the node in the program.</param>
    public StringTypeNode(SourcePosition sourcePosition) : base(sourcePosition) {
    }

    public override string GetInitializationString(string id) {
      throw new NotImplementedException();
    }

    public override string ToString() {
      return "STRING";
    }
  }
}