namespace Project.Test;

using System;
using Project;

public class ProjectTestProgram
{
    [Fact]
    void TestRunApplicationWithExit()
    {
        // Test that RunApplication exits when "Exit" is selected
        var callCount = 0;
        var choiceSelector = new Func<string[], string>((options) =>
        {
            callCount++;
            return "Exit"; // Always return exit to exit the loop
        });

        // This should not throw an exception and should exit cleanly
        Program.RunApplication(choiceSelector);

        Assert.Equal(1, callCount);
    }

    [Fact]
    void TestRunApplicationWithInvalidChoice()
    {
        // Test that RunApplication handles invalid choices
        var callCount = 0;
        var choiceSelector = new Func<string[], string>((options) =>
        {
            callCount++;
            if (callCount == 1)
                return "InvalidChoice"; // First call returns invalid choice
            else
                return "Exit"; // Second call exits
        });

        // This should handle the invalid choice and continue to exit
        Program.RunApplication(choiceSelector);

        Assert.Equal(2, callCount);
    }

    [Fact]
    void TestMainMethodExists()
    {
        // Verify Main method exists (Main methods are typically private)
        var method = typeof(Program).GetMethod("Main", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic, null, new Type[] { typeof(string[]) }, null);
        Assert.NotNull(method);
        Assert.True(method.IsStatic);
    }

    [Fact]
    void TestRunApplicationMethodExists()
    {
        // Verify RunApplication method exists
        var method = typeof(Program).GetMethod("RunApplication", new Type[] { typeof(Func<string[], string>) });
        Assert.NotNull(method);
        Assert.True(method.IsStatic);
        Assert.True(method.IsPublic);
    }

    [Fact]
    void TestProgramClassIsPublic()
    {
        // Verify the Program class is public
        var type = typeof(Program);
        Assert.NotNull(type);
        Assert.True(type.IsClass);
        Assert.True(type.IsPublic);
    }
}