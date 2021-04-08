using System;
using System.Linq;
using System.Text;
using ClubsCompiler.Program;
using Antlr4.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClubsCompiler.Tests {

  [TestClass]
  public class BuildASTVisitorTests {
    public BuildASTVisitor BuildASTVisitor { get; set; }

    [TestInitialize]
    public void TestInitialize() {
      BuildASTVisitor = new BuildASTVisitor();
    }

    public ASTNode ParseInput(string input) {
      StringBuilder builder = new StringBuilder();
      builder.Append("setup {\n");
      builder.Append(input);
      builder.Append("\n}");

      AntlrInputStream inputStream = new AntlrInputStream(builder.ToString());
      CLUBSLexer CLUBSLexer = new CLUBSLexer(inputStream);
      CommonTokenStream commonTokenStream = new CommonTokenStream(CLUBSLexer);

      CLUBSParser CLUBSParser = new CLUBSParser(commonTokenStream);

      BlockNode block = (BuildASTVisitor.VisitProg(CLUBSParser.prog()) as ProgNode).Children.First() as BlockNode;

      return block.Statements.FirstOrDefault();
    }

    [TestMethod]
    public void Visit_DeclarationNoAssignAsInput_ReturnsCorrectASTNode() {
      // Arrange
      string input = "Int : test";

      // Act
      ASTNode result = ParseInput(input);

      // Assert
      Assert.IsTrue(result is DeclarationNode && (result as DeclarationNode).AssignmentExpression == null);
    }

    [TestMethod]
    public void Visit_DeclarationWithAssignAsInput_ReturnsCorrectASTNode() {
      // Arrange
      string input = "Int : test = 1";

      // Act
      ASTNode result = ParseInput(input);

      // Assert
      Assert.IsTrue(result is DeclarationNode && (result as DeclarationNode).AssignmentExpression != null);
    }

    [TestMethod]
    public void Visit_AssignmentAsInput_ReturnsCorrectASTNode() {
      // Arrange
      string input = "test = 1";

      // Act
      ASTNode result = ParseInput(input);

      // Assert
      Assert.IsTrue(result is AssignmentNode);
    }

    [TestMethod]
    public void Visit_IfStatementAsInput_ReturnsCorrectASTNode() {
      // Arrange
      string input = "IF myBool {\n}\n";

      // Act
      ASTNode result = ParseInput(input);

      // Assert
      Assert.IsTrue(result is IfNode);
    }

    [TestMethod]
    public void Visit_PutActionAsInput_ReturnsCorrectASTNode() {
      // Arrange
      string input = "hand PUT deck\n";

      // Act
      ASTNode result = ParseInput(input);

      // Assert
      Assert.IsTrue(result is PutActionNode);
    }

    [TestMethod]
    public void Visit_TakeActionAsInput_ReturnsCorrectASTNode() {
      // Arrange
      string input = "FROM deck TAKE 1 PUT hand\n";

      // Act
      ASTNode result = ParseInput(input);

      // Assert
      Assert.IsTrue(result is TakeActionNode);
    }

    [TestMethod]
    public void Visit_TakeAtActionAsInput_ReturnsCorrectASTNode() {
      // Arrange
      string input = "FROM deck TAKE 1 AT 1 PUT hand\n";

      // Act
      ASTNode result = ParseInput(input);

      // Assert
      Assert.IsTrue(result is TakeAtActionNode);
    }

    [TestMethod]
    public void Visit_TakeWhereActionAsInput_ReturnsCorrectASTNode() {
      // Arrange
      string input = "FROM deck TAKE 1 WHERE suit IS hearts PUT hand\n";

      // Act
      ASTNode result = ParseInput(input);

      // Assert
      Assert.IsTrue(result is TakeWhereActionNode);
    }

    [TestMethod]
    public void Visit_OwnsActionAsInput_ReturnsCorrectASTNode() {
      // Arrange
      string input = "Player OWNS [Set OF Card : hand]\n";

      // Act
      ASTNode result = ParseInput(input);

      // Assert
      Assert.IsTrue(result is OwnsActionNode);
    }

    [TestMethod]
    public void Visit_ForAllLoopAsInput_ReturnsCorrectASTNode() {
      // Arrange
      string input = "FORALL child IN parent {\n}\n";

      // Act
      ASTNode result = ParseInput(input);

      // Assert
      Assert.IsTrue(result is ForAllNode);
    }

    [TestMethod]
    public void Visit_PrintActionAsInput_ReturnsCorrectASTNode() {
      // Arrange
      string input = "PRINT \"hello world\"\n";

      // Act
      ASTNode result = ParseInput(input);

      // Assert
      Assert.IsTrue(result is PrintActionNode);
    }

    [TestMethod]
    public void Visit_SetAssignmentAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "players = [player1, player2, player3]\n";

      // Act
      ASTNode result = (ParseInput(input) as AssignmentNode).Right;

      // Assert
      Assert.IsTrue(result is SetValueNode);
    }

    [TestMethod]
    public void Visit_DeckAssignmentAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "deck = [suit][rank]\n";

      // Act
      ASTNode result = (ParseInput(input) as AssignmentNode).Right;

      // Assert
      Assert.IsTrue(result is DeckValueNode);
    }

    [TestMethod]
    public void Visit_WhileLoopStmtAsInput_ReturnsCorrectASTNode() {
      // Arrange
      string input = "WHILE myBool {\n}\n";

      // Act
      ASTNode result = ParseInput(input);

      // Assert
      Assert.IsTrue(result is WhileNode);
    }

    [TestMethod]
    public void Visit_AssignmentWithInfixAdditionOperatorAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "myInt = 5 + 5";

      // Act
      ASTNode result = (ParseInput(input) as AssignmentNode).Right;

      // Assert
      Assert.IsTrue(result is AdditionNode);
    }

    [TestMethod]
    public void Visit_AssignmentWithInfixSubtractionOperatorAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "myInt = 10 - 5";

      // Act
      ASTNode result = (ParseInput(input) as AssignmentNode).Right;

      // Assert
      Assert.IsTrue(result is SubtractionNode);
    }

    [TestMethod]
    public void Visit_AssignmentWithInfixMultiplicationOperatorAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "myInt = 5 * 2";

      // Act
      ASTNode result = (ParseInput(input) as AssignmentNode).Right;

      // Assert
      Assert.IsTrue(result is MultiplicationNode);
    }

    [TestMethod]
    public void Visit_AssignmentWithInfixDivisionOperatorAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "myInt = 6 / 2";

      // Act
      ASTNode result = (ParseInput(input) as AssignmentNode).Right;

      // Assert
      Assert.IsTrue(result is DivisionNode);
    }

    [TestMethod]
    public void Visit_AssignmentWithInfixIsOperatorAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "myBool = var1 IS var2";

      // Act
      ASTNode result = (ParseInput(input) as AssignmentNode).Right;

      // Assert
      Assert.IsTrue(result is IsNode);
    }

    [TestMethod]
    public void Visit_AssignmentWithInfixAndOperatorAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "myBool = var1 IS var2 AND var3 IS var4";

      // Act
      ASTNode result = (ParseInput(input) as AssignmentNode).Right;

      // Assert
      Assert.IsTrue(result is AndNode);
    }

    [TestMethod]
    public void Visit_AssignmentWithInfixOrOperatorAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "myBool = var1 IS var2 OR var3 IS var4";

      // Act
      ASTNode result = (ParseInput(input) as AssignmentNode).Right;

      // Assert
      Assert.IsTrue(result is OrNode);
    }

    [TestMethod]
    public void Visit_AssignmentWithInfixGreaterThanOperatorAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "myBool = var1 > var2";

      // Act
      ASTNode result = (ParseInput(input) as AssignmentNode).Right;

      // Assert
      Assert.IsTrue(result is GreaterThanNode);
    }

    [TestMethod]
    public void Visit_AssignmentWithInfixLessThanOperatorAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "myBool = var1 < var2";

      // Act
      ASTNode result = (ParseInput(input) as AssignmentNode).Right;

      // Assert
      Assert.IsTrue(result is LessThanNode);
    }

    [TestMethod]
    public void Visit_AssignmentWithReferenceAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "var1 = var2";

      // Act
      ASTNode result = (ParseInput(input) as AssignmentNode).Right;

      // Assert
      Assert.IsTrue(result is ReferenceNode);
    }

    [TestMethod]
    public void Visit_AssignmentWithDotReferenceAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "var1 = var2.member";

      // Act
      ASTNode result = (ParseInput(input) as AssignmentNode).Right;

      // Assert
      Assert.IsTrue(result is DotReferenceNode);
    }

    [TestMethod]
    public void Visit_AssignmentWithDotCountAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "myInt = mySet.count";

      // Act
      ASTNode result = ((ParseInput(input) as AssignmentNode).Right as DotReferenceNode).Member;

      // Assert
      Assert.IsTrue(result is CountNode);
    }

    [TestMethod]
    public void Visit_AssignmentWithRandomAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "var1 = RANDOM 0 TO 100";

      // Act
      ASTNode result = (ParseInput(input) as AssignmentNode).Right;

      // Assert
      Assert.IsTrue(result is RandomNode);
    }

    [TestMethod]
    public void Visit_BlockAsInput_ReturnsCorrectASTNode() {
      // Arrange
      string input = "{ }";

      // Act
      ASTNode result = ParseInput(input);

      // Assert
      Assert.IsTrue(result is BlockNode);
    }

    [TestMethod]
    public void Visit_IfElseStatementAsInput_IfNodeContainsElseBlock() {
      // Arrange
      string input = "IF myBool { } \n ELSE { }";

      // Act
      ASTNode result = ParseInput(input);

      // Assert
      Assert.IsTrue(result is IfNode ifNode && ifNode.ElseBlock != null);
    }

    [TestMethod]
    public void Visit_IfElseIfStatementAsInput_IfNodeElseIfChainContainsNodes() {
      // Arrange
      string input = "IF myBool { } \n ELSE IF myBool2 { }";

      // Act
      ASTNode result = ParseInput(input);

      // Assert
      Assert.IsTrue(result is IfNode ifNode && ifNode.ElseIfChain.Count > 0);
    }

    [TestMethod]
    public void Visit_AssignmentWithIntegerLiteralAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "myInt = 2";

      // Act
      ASTNode result = (ParseInput(input) as AssignmentNode).Right;

      // Assert
      Assert.IsTrue(result is IntegerLiteral);
    }

    [TestMethod]
    public void Visit_AssignmentWithBoolLiteralAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "myBool = true";

      // Act
      ASTNode result = (ParseInput(input) as AssignmentNode).Right;

      // Assert
      Assert.IsTrue(result is BoolLiteral);
    }

    [TestMethod]
    public void Visit_AssignmentWithStringLiteralAsInput_AssignmentRightNodeIsCorrect() {
      // Arrange
      string input = "myString = \"hello world\"";

      // Act
      ASTNode result = (ParseInput(input) as AssignmentNode).Right;

      // Assert
      Assert.IsTrue(result is StringLiteral);
    }
  }
}