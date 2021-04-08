using System;
using System.Collections.Generic;
using System.Linq;
using ClubsCompiler.Program;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClubsCompiler.Tests {

  [TestClass]
  public class CheckerTests {
    public ErrorLogger ErrorLogger { get; set; }
    public Checker Checker { get; set; }

    public SourcePosition DummySrcPos { get; set; }

    public ReferenceNode PlayerReferenceNode { get; set; }
    public ReferenceNode HeartsReferenceNode { get; set; }
    public ReferenceNode SpadesReferenceNode { get; set; }
    public ReferenceNode BoolReferenceNode { get; set; }
    public ReferenceNode SuitReferenceNode { get; set; }
    public ReferenceNode DeckReferenceNode { get; set; }

    [TestInitialize]
    public void TestInitialize() {
      ErrorLogger = new ErrorLogger();
      Checker = new Checker(ErrorLogger);
      DummySrcPos = new SourcePosition(0, 0);

      VariableDeclarations();
      VariableReferences();
    }

    public void VariableReferences() {
      PlayerReferenceNode = new ReferenceNode(new IdentifierNode("player", DummySrcPos));
      HeartsReferenceNode = new ReferenceNode(new IdentifierNode("hearts", DummySrcPos));
      SpadesReferenceNode = new ReferenceNode(new IdentifierNode("spades", DummySrcPos));
      BoolReferenceNode = new ReferenceNode(new IdentifierNode("myBool", DummySrcPos));
      SuitReferenceNode = new ReferenceNode(new IdentifierNode("suit", DummySrcPos));
      DeckReferenceNode = new ReferenceNode(new IdentifierNode("deck", DummySrcPos));
    }

    public void VariableDeclarations() {
      Checker.SymbolTable.OpenScope();

      DeclarationNode playerDcl = new DeclarationNode(StandardTypes.Player, new IdentifierNode("player", DummySrcPos), DummySrcPos);
      DeclarationNode heartsDcl = new DeclarationNode(StandardTypes.CardValue, new IdentifierNode("hearts", DummySrcPos), DummySrcPos);
      DeclarationNode spadesDcl = new DeclarationNode(StandardTypes.CardValue, new IdentifierNode("spades", DummySrcPos), DummySrcPos);
      DeclarationNode boolDcl = new DeclarationNode(StandardTypes.Bool, new IdentifierNode("myBool", DummySrcPos), DummySrcPos);
      DeclarationNode suitDcl = new DeclarationNode(StandardTypes.GetSetType(new CardValueTypeNode(DummySrcPos)), new IdentifierNode("suit", DummySrcPos), DummySrcPos);
      DeclarationNode deckDcl = new DeclarationNode(StandardTypes.GetSetType(new CardTypeNode(DummySrcPos)), new IdentifierNode("deck", DummySrcPos), DummySrcPos);

      Checker.Visit(heartsDcl);
      Checker.Visit(playerDcl);
      Checker.Visit(spadesDcl);
      Checker.Visit(boolDcl);
      Checker.Visit(suitDcl);
      Checker.Visit(deckDcl);
    }

    [TestMethod]
    public void Visit_IllegalAssignment_LogsIncompatibleTypesError() {
      // Arrange
      AssignmentNode assignmentNode = new AssignmentNode(DummySrcPos);
      assignmentNode.Left = HeartsReferenceNode;
      assignmentNode.Right = PlayerReferenceNode;

      // Act
      Checker.Visit(assignmentNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 1 && ErrorLogger.Errors.First() is IncompatibleTypesError);
    }

    [TestMethod]
    public void Visit_LegalAssignment_LogsNoError() {
      // Arrange
      AssignmentNode assignmentNode = new AssignmentNode(DummySrcPos);
      assignmentNode.Left = HeartsReferenceNode;
      assignmentNode.Right = SpadesReferenceNode;

      // Act
      Checker.Visit(assignmentNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 0);
    }

    [TestMethod]
    public void Visit_ReferenceToUndeclaredVariable_LogsUndeclaredVariableError() {
      // Arrange
      ReferenceNode referenceNode = new ReferenceNode(new IdentifierNode("player two", DummySrcPos));

      // Act
      Checker.Visit(referenceNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 1 && ErrorLogger.Errors.First() is UndeclaredVariableError);
    }

    [TestMethod]
    public void Visit_ReferenceToDeclaredVariable_LogsNoError() {
      // Arrange
      ReferenceNode referenceNode = HeartsReferenceNode;

      // Act
      Checker.Visit(referenceNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 0);
    }

    [TestMethod]
    public void Visit_DeclarationWithAlreadyDeclaredName_LogsVariableAlreadyDeclaredError() {
      // Arrange
      DeclarationNode declarationNode = new DeclarationNode(StandardTypes.Player, new IdentifierNode("player", DummySrcPos), DummySrcPos);

      // Act
      Checker.Visit(declarationNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 1 && ErrorLogger.Errors.First() is VariableAlreadyDeclaredError);
    }

    [TestMethod]
    public void Visit_DeclarationWithNewName_LogsNoError() {
      // Arrange
      DeclarationNode declarationNode = new DeclarationNode(StandardTypes.Int, new IdentifierNode("myInt", DummySrcPos), DummySrcPos);

      // Act
      Checker.Visit(declarationNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 0);
    }

    [TestMethod]
    public void Visit_IfStatementWithWrongPredicateType_LogsIncompatibleTypesError() {
      // Arrange
      IfNode ifNode = new IfNode(DummySrcPos);
      ifNode.Block = new BlockNode(DummySrcPos);
      ifNode.Predicate = PlayerReferenceNode;

      // Act
      Checker.Visit(ifNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 1 && ErrorLogger.Errors.First() is IncompatibleTypesError);
    }

    [TestMethod]
    public void Visit_IfStatementWithCorrectPredicateType_LogsNoError() {
      // Arrange
      IfNode ifNode = new IfNode(DummySrcPos);
      ifNode.Block = new BlockNode(DummySrcPos);
      ifNode.Predicate = BoolReferenceNode;

      // Act
      Checker.Visit(ifNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 0);
    }

    [TestMethod]
    public void Visit_ForAllWithNonSetParentType_LogsExpectedTypeError() {
      // Arrange
      ForAllNode forAllNode = new ForAllNode(DummySrcPos);
      forAllNode.Block = new BlockNode(DummySrcPos);
      forAllNode.Parent = SpadesReferenceNode;
      forAllNode.Child = new DeclarationNode(new CardValueTypeNode(DummySrcPos), new IdentifierNode("child", DummySrcPos), DummySrcPos);

      // Act
      Checker.Visit(forAllNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 1 && ErrorLogger.Errors.First() is ExpectedTypeError);
    }

    [TestMethod]
    public void Visit_ForAllWithSetParentType_LogsNoError() {
      // Arrange
      ForAllNode forAllNode = new ForAllNode(DummySrcPos);
      forAllNode.Block = new BlockNode(DummySrcPos);
      forAllNode.Parent = SuitReferenceNode;
      forAllNode.Child = new DeclarationNode(new CardValueTypeNode(DummySrcPos), new IdentifierNode("child", DummySrcPos), DummySrcPos);

      // Act
      Checker.Visit(forAllNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 0);
    }

    [TestMethod]
    public void Visit_OwnsActionWithNonPlayerType_LogsExpectedTypeError() {
      // Arrange
      OwnsActionNode ownsActionNode = new OwnsActionNode(new IntTypeNode(DummySrcPos), new List<DeclarationNode>(), DummySrcPos);

      // Act
      Checker.Visit(ownsActionNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 1 && ErrorLogger.Errors.First() is ExpectedTypeError);
    }

    [TestMethod]
    public void Visit_OwnsActionWithPlayerType_LogsNoError() {
      // Arrange
      OwnsActionNode ownsActionNode = new OwnsActionNode(new PlayerTypeNode(DummySrcPos), new List<DeclarationNode>(), DummySrcPos);

      // Act
      Checker.Visit(ownsActionNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 0);
    }

    [TestMethod]
    public void Visit_PutActionWithNonCardSetType_LogsExpectedTypeError() {
      // Arrange
      PutActionNode putActionNode = new PutActionNode(DummySrcPos);
      putActionNode.Source = PlayerReferenceNode;
      putActionNode.Target = DeckReferenceNode;

      // Act
      Checker.Visit(putActionNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 1 && ErrorLogger.Errors.First() is ExpectedTypeError);
    }

    [TestMethod]
    public void Visit_PutActionWithCardSetType_LogsNoError() {
      // Arrange
      PutActionNode putActionNode = new PutActionNode(DummySrcPos);
      putActionNode.Source = DeckReferenceNode;
      putActionNode.Target = DeckReferenceNode;

      // Act
      Checker.Visit(putActionNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 0);
    }

    [TestMethod]
    public void Visit_WhileStatementWithWrongPredicateType_LogsExpectedTypeError() {
      // Arrange
      WhileNode whileNode = new WhileNode(DummySrcPos);
      whileNode.Block = new BlockNode(DummySrcPos);
      whileNode.Predicate = PlayerReferenceNode;

      // Act
      Checker.Visit(whileNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 1 && ErrorLogger.Errors.First() is ExpectedTypeError);
    }

    [TestMethod]
    public void Visit_WhileStatementWithCorrectPredicateType_LogsNoError() {
      // Arrange
      WhileNode whileNode = new WhileNode(DummySrcPos);
      whileNode.Block = new BlockNode(DummySrcPos);
      whileNode.Predicate = BoolReferenceNode;

      // Act
      Checker.Visit(whileNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 0);
    }

    [TestMethod]
    public void Visit_AdditionNodeWithIncompatibleTypes_LogsCannotUseWithOperatorError() {
      // Arrange
      AdditionNode additionNode = new AdditionNode(DummySrcPos);
      ReferenceNode left = PlayerReferenceNode;
      IntegerLiteral right = new IntegerLiteral("5", DummySrcPos);

      additionNode.Left = left;
      additionNode.Right = right;

      // Act
      Checker.Visit(additionNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 1 && ErrorLogger.Errors.First() is CannotUseWithOperatorError);
    }

    [TestMethod]
    public void Visit_AdditionNodeWithCompatibleTypes_LogsNoError() {
      // Arrange
      AdditionNode additionNode = new AdditionNode(DummySrcPos);
      IntegerLiteral left = new IntegerLiteral("2", DummySrcPos);
      IntegerLiteral right = new IntegerLiteral("5", DummySrcPos);

      additionNode.Left = left;
      additionNode.Right = right;

      // Act
      Checker.Visit(additionNode);

      // Assert
      Assert.IsTrue(ErrorLogger.Errors.Count == 0);
    }
  }
}