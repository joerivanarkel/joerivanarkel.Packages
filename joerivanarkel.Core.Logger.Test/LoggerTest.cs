using FakeItEasy;
using joerivanarkel.Core.Logger;
using joerivanarkel.Core.Logger.FileHandlers;
using joerivanarkel.Core.Logger.Interfaces;
using joerivanarkel.Core.Logger.FileHandlers.Model;
using joerivanarkel.Core.Logger.Exception;
using Xunit;
using static joerivanarkel.Core.Logger.Logger;

namespace joerivanarkel.Core.Logger.Test;

public class LoggerTest
{
    [Fact]
    public void ShouldLog()
    {
        // Arrange
        var FileWriteHandler = A.Fake<IFileWriteHandler>();
        var logger = new Logger();

        A.CallTo(() => FileWriteHandler.WriteToFile(A<FileWriteModel>.Ignored, A<bool>.Ignored)).Returns(true);
        var message = "Test message";
        var type = LogType.INFO;

        // Act
        var result = logger.Log(message, type);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ShouldLogToConsole()
    {
        // Arrange
        var FileWriteHandler = A.Fake<IFileWriteHandler>();
        var logger = new Logger(FileWriteHandler, new LoggerConfiguration { UseConsole = true });

        A.CallTo(() => FileWriteHandler.WriteToFile(A<FileWriteModel>.Ignored, A<bool>.Ignored)).Returns(true);
        var message = "Test message";
        var type = LogType.INFO;

        // Act
        var result = logger.Log(message, type);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ShouldNotLog()
    {
        try
        {
            // Arrange
            var FileWriteHandler = A.Fake<IFileWriteHandler>();
            var logger = new Logger();

            A.CallTo(() => FileWriteHandler.WriteToFile(A<FileWriteModel>.Ignored, A<bool>.Ignored)).Returns(false);
            var message = "";
            var type = LogType.INFO;
            // Act
            var result = logger.Log(message, type);
        }
        catch (System.Exception ex)
        {
            Assert.IsType<LoggerException>(ex);
            Assert.Equal("Message cannot be null or empty", ex.Message);
        }
    }

}