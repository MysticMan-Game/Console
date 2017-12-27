using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MysticMan.ConsoleApp;
using MysticMan.ConsoleApp.Fields;

namespace MysticManConsole.Tests.Fields {
  [TestClass]
  public class SolutionInputFieldTests{
    [TestMethod]
    public void ReadValidSolution() {
      Mock<IScreenReader> screenReaderMock = new Mock<IScreenReader>(MockBehavior.Strict);
      Mock<IScreenInfo> screenInfoMock = new Mock<IScreenInfo>(MockBehavior.Strict);
      Mock<IScreenWriter> screenWriter = new Mock<IScreenWriter>(MockBehavior.Loose);
      SolutionInputField inputField = new SolutionInputField(new Size(5, 5), screenReaderMock.Object, screenWriter.Object, screenInfoMock.Object);

      screenReaderMock.Setup(_ => _.ReadLine(It.IsAny<Position>())).Returns("A1");
      inputField.Read();
      Assert.AreEqual("A1", inputField.Input);
    }

    [TestMethod]
    public void ReadSolutionOutOfRange() {
      Mock<IScreenReader> screenReaderMock = new Mock<IScreenReader>(MockBehavior.Strict);
      Mock<IScreenInfo> screenInfoMock = new Mock<IScreenInfo>(MockBehavior.Strict);
      Mock<IScreenWriter> screenWriter = new Mock<IScreenWriter>(MockBehavior.Loose);
      SolutionInputField inputField = new SolutionInputField(new Size(5, 5), screenReaderMock.Object, screenWriter.Object, screenInfoMock.Object);

      screenReaderMock.SetupSequence(_ => _.ReadLine(It.IsAny<Position>())).Returns("G1").Returns("A8").Returns("A1");
      inputField.Read();
      Assert.AreEqual("A1", inputField.Input);
    }

    [TestMethod]
    public void ReadSolutionInvalidFormat() {
      Mock<IScreenReader> screenReaderMock = new Mock<IScreenReader>(MockBehavior.Strict);
      Mock<IScreenInfo> screenInfoMock = new Mock<IScreenInfo>(MockBehavior.Strict);
      Mock<IScreenWriter> screenWriter = new Mock<IScreenWriter>(MockBehavior.Loose);
      SolutionInputField inputField = new SolutionInputField(new Size(5, 5), screenReaderMock.Object, screenWriter.Object, screenInfoMock.Object);

      screenReaderMock.SetupSequence(_ => _.ReadLine(It.IsAny<Position>())).Returns("1A").Returns("4E").Returns("B2");
      inputField.Read();
      Assert.AreEqual("B2", inputField.Input);
    }


    [TestMethod]
    public void ReadSolutionValidBoundariesFormat() {
      Mock<IScreenReader> screenReaderMock = new Mock<IScreenReader>(MockBehavior.Strict);
      Mock<IScreenInfo> screenInfoMock = new Mock<IScreenInfo>(MockBehavior.Strict);
      Mock<IScreenWriter> screenWriter = new Mock<IScreenWriter>(MockBehavior.Loose);
      SolutionInputField inputField = new SolutionInputField(new Size(5, 5), screenReaderMock.Object, screenWriter.Object, screenInfoMock.Object);

      screenReaderMock.SetupSequence(_ => _.ReadLine(It.IsAny<Position>())).Returns("E5");
      inputField.Read();
      Assert.AreEqual("E5", inputField.Input);
    }

    [TestMethod]
    public void ReadSolutionValidBoundaries10x10Format() {
      Mock<IScreenReader> screenReaderMock = new Mock<IScreenReader>(MockBehavior.Strict);
      Mock<IScreenInfo> screenInfoMock = new Mock<IScreenInfo>(MockBehavior.Strict);
      Mock<IScreenWriter> screenWriter = new Mock<IScreenWriter>(MockBehavior.Loose);
      SolutionInputField inputField = new SolutionInputField(new Size(10, 10), screenReaderMock.Object, screenWriter.Object, screenInfoMock.Object);

      screenReaderMock.SetupSequence(_ => _.ReadLine(It.IsAny<Position>())).Returns("K6");
      inputField.Read();
      Assert.AreEqual("K6", inputField.Input);
    }

    [TestMethod]
    public void ReadValidSolutionInLargeGameField() {
      Mock<IScreenReader> screenReaderMock = new Mock<IScreenReader>(MockBehavior.Strict);
      Mock<IScreenInfo> screenInfoMock = new Mock<IScreenInfo>(MockBehavior.Strict);
      Mock<IScreenWriter> screenWriter = new Mock<IScreenWriter>(MockBehavior.Loose);
      SolutionInputField inputField = new SolutionInputField(new Size(15, 15), screenReaderMock.Object, screenWriter.Object, screenInfoMock.Object);

      screenReaderMock.Setup(_ => _.ReadLine(It.IsAny<Position>())).Returns("E11");
      inputField.Read();
      Assert.AreEqual("E11", inputField.Input);
    }
  }
}
