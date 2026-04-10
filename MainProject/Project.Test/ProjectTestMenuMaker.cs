namespace Project.Test;

using System;
using Project;

public class ProjectTestMenuMaker
{
    [Fact]
    void TestMenuHelperWithValidChoice()
    {
        var options = new string[] { "Option1", "Option2", "Option3" };
        var result = MenuMaker.MenuHelper(options, "Option2");
        
        Assert.Equal("Option2", result);
    }

    [Fact]
    void TestMenuHelperWithInvalidChoice()
    {
        var options = new string[] { "Option1", "Option2", "Option3" };
        
        var exception = Assert.Throws<ArgumentException>(() => 
            MenuMaker.MenuHelper(options, "InvalidOption"));
        
        Assert.Contains("Selected choice is not in the available options", exception.Message);
    }

    [Fact]
    void TestMenuHelperWithEmptyOptions()
    {
        var options = new string[] { };
        
        var exception = Assert.Throws<ArgumentException>(() => 
            MenuMaker.MenuHelper(options, "AnyOption"));
        
        Assert.Contains("Selected choice is not in the available options", exception.Message);
    }

    [Fact]
    void TestMenuHelperMethodExists()
    {
        // Verify the interactive MenuHelper method exists
        var method = typeof(MenuMaker).GetMethod("MenuHelper", new Type[] { typeof(string[]) });
        Assert.NotNull(method);
        Assert.True(method.IsStatic);
        Assert.True(method.IsPublic);
    }

    [Fact]
    void TestMainMenuMethodExists()
    {
        // Verify MainMenu method exists
        var method = typeof(MenuMaker).GetMethod("MainMenu", new Type[] { });
        Assert.NotNull(method);
        Assert.True(method.IsStatic);
        Assert.True(method.IsPublic);
    }

    [Fact]
    void TestMenuMakerClassIsPublic()
    {
        // Verify the MenuMaker class is public
        var type = typeof(MenuMaker);
        Assert.NotNull(type);
        Assert.True(type.IsClass);
        Assert.True(type.IsPublic);
    }
}