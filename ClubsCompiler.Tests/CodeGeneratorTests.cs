using System;
using System.Text;
using ClubsCompiler.Program;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClubsCompiler.Tests {

  [TestClass]
  public class CodeGeneratorTests {
    public ErrorLogger ErrorLogger { get; set; }
    public CodeGenerator CodeGenerator { get; set; }

    public SourcePosition DummySrcPos { get; set; }

    public ReferenceNode PlayerReferenceNode { get; set; }
    public ReferenceNode HeartsReferenceNode { get; set; }
    public ReferenceNode SpadesReferenceNode { get; set; }
    public ReferenceNode BoolReferenceNode { get; set; }
    public ReferenceNode SuitReferenceNode { get; set; }
    public ReferenceNode DeckReferenceNode { get; set; }
    public ReferenceNode HandReferenceNode { get; set; }

    [TestInitialize]
    public void TestInitialize() {
      ErrorLogger = new ErrorLogger();
      CodeGenerator = new CodeGenerator();
      DummySrcPos = new SourcePosition(0, 0);

      CodeGenerator.SymbolTable.OpenScope();

      VariableReferences();
    }

    public void VariableReferences() {
      PlayerReferenceNode = new ReferenceNode(new IdentifierNode("player", DummySrcPos));
      HeartsReferenceNode = new ReferenceNode(new IdentifierNode("hearts", DummySrcPos));
      SpadesReferenceNode = new ReferenceNode(new IdentifierNode("spades", DummySrcPos));
      BoolReferenceNode = new ReferenceNode(new IdentifierNode("myBool", DummySrcPos));
      SuitReferenceNode = new ReferenceNode(new IdentifierNode("suit", DummySrcPos));
      DeckReferenceNode = new ReferenceNode(new IdentifierNode("deck", DummySrcPos));
      HandReferenceNode = new ReferenceNode(new IdentifierNode("hand", DummySrcPos));
    }

    [TestMethod]
    public void Visit_AssignmentNode_EmitsCorrectCode() {
      // Arrange
      AssignmentNode assignmentNode = new AssignmentNode(DummySrcPos);
      assignmentNode.Left = HeartsReferenceNode;
      assignmentNode.Right = SpadesReferenceNode;

      string expectedResult = "hearts = spades;\n";

      // Act
      string actualResult = CodeGenerator.Visit(assignmentNode);

      // Assert
      Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void Visit_ReferenceNode_EmitsCorrectCode() {
      // Arrange
      ReferenceNode referenceNode = PlayerReferenceNode;

      string expectedResult = "player";

      // Act
      string actualResult = CodeGenerator.Visit(referenceNode);

      // Assert
      Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void Visit_DeclarationNode_EmitsCorrectCode() {
      // Arrange
      DeclarationNode declarationNode = new DeclarationNode(new IntTypeNode(0, DummySrcPos),
                                        new IdentifierNode("myInt", DummySrcPos), DummySrcPos);

      string expectedResult = "int myInt = 0;\n";

      // Act
      string actualResult = CodeGenerator.Visit(declarationNode);

      // Assert
      Assert.AreEqual(expectedResult, actualResult, actualResult);
    }

    [TestMethod]
    public void Visit_IfNode_EmitsCorrectCode() {
      // Arrange
      IfNode ifNode = new IfNode(DummySrcPos);
      ifNode.Block = new BlockNode(DummySrcPos);
      ifNode.Predicate = BoolReferenceNode;

      string expectedResult = "if (myBool)\n{\n}\n";

      // Act
      string actualResult = CodeGenerator.Visit(ifNode);

      // Assert
      Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void Visit_ForAllNode_EmitsCorrectCode() {
      // Arrange
      ForAllNode forAllNode = new ForAllNode(DummySrcPos);
      forAllNode.Block = new BlockNode(DummySrcPos);
      forAllNode.Parent = SuitReferenceNode;
      forAllNode.Child = new DeclarationNode(new CardValueTypeNode(DummySrcPos), new IdentifierNode("child", DummySrcPos), DummySrcPos);

      StringBuilder builder = new StringBuilder();

      builder.Append("for(int i_1 = 0; i_1 < suit.Count; i_1++)\n");
      builder.Append("{\n var child = suit[i_1];\n");
      builder.Append("{\n}\n}");

      string expectedResult = builder.ToString();

      // Act
      string actualResult = CodeGenerator.Visit(forAllNode);

      // Assert
      Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void Visit_PutActionNode_EmitsCorrectCode() {
      // Arrange
      PutActionNode putActionNode = new PutActionNode(DummySrcPos);
      putActionNode.Source = HandReferenceNode;
      putActionNode.Target = DeckReferenceNode;

      StringBuilder builder = new StringBuilder();

      builder.Append("deck.AddRange(hand);\n");
      builder.Append("hand.Clear();\n");

      string expectedResult = builder.ToString();

      // Act
      string actualResult = CodeGenerator.Visit(putActionNode);

      // Assert
      Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void Visit_AdditionNode_EmitsCorrectCode() {
      // Arrange
      AdditionNode additionNode = new AdditionNode(DummySrcPos);
      IntegerLiteral left = new IntegerLiteral("2", DummySrcPos);
      IntegerLiteral right = new IntegerLiteral("5", DummySrcPos);

      additionNode.Left = left;
      additionNode.Right = right;

      string expectedResult = "2 + 5";

      // Act
      string actualResult = CodeGenerator.Visit(additionNode);

      // Assert
      Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void Visit_SubtractionNode_EmitsCorrectCode() {
      // Arrange
      SubtractionNode subtractionNode = new SubtractionNode(DummySrcPos);
      IntegerLiteral left = new IntegerLiteral("10", DummySrcPos);
      IntegerLiteral right = new IntegerLiteral("5", DummySrcPos);

      subtractionNode.Left = left;
      subtractionNode.Right = right;

      string expectedResult = "10 - 5";

      // Act
      string actualResult = CodeGenerator.Visit(subtractionNode);

      // Assert
      Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void Visit_MultiplicationNode_EmitsCorrectCode() {
      // Arrange
      MultiplicationNode multiplicationNode = new MultiplicationNode(DummySrcPos);
      IntegerLiteral left = new IntegerLiteral("3", DummySrcPos);
      IntegerLiteral right = new IntegerLiteral("3", DummySrcPos);

      multiplicationNode.Left = left;
      multiplicationNode.Right = right;

      string expectedResult = "3 * 3";

      // Act
      string actualResult = CodeGenerator.Visit(multiplicationNode);

      // Assert
      Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void Visit_DivisionNode_EmitsCorrectCode() {
      // Arrange
      DivisionNode divisionNode = new DivisionNode(DummySrcPos);
      IntegerLiteral left = new IntegerLiteral("8", DummySrcPos);
      IntegerLiteral right = new IntegerLiteral("2", DummySrcPos);

      divisionNode.Left = left;
      divisionNode.Right = right;

      string expectedResult = "8 / 2";

      // Act
      string actualResult = CodeGenerator.Visit(divisionNode);

      // Assert
      Assert.AreEqual(expectedResult, actualResult);
    }
  }
}