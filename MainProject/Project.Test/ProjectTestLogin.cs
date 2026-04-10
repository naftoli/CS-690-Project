namespace Project.Test;

using System;
using System.IO;
using Project;

public class ProjectTestLogin : IDisposable
{
    private string testFilePath;

    public ProjectTestLogin()
    {
        // Create a unique temp file for each test
        testFilePath = Path.Combine(Path.GetTempPath(), $"test_users_{Guid.NewGuid()}.txt");
    }

    public void Dispose()
    {
        // Clean up temp file after each test
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
    }

    [Fact]
    void TestGetFilePath()
    {
        Login login = new Login(testFilePath);
        Assert.Equal(testFilePath, login.getFilePath());
    }

    [Fact]
    void TestRegisterUser()
    {
        Login login = new Login(testFilePath);
        string username = "testuser";
        string password = "testpass";
        
        bool result = login.RegisterUser(username, password);
        
        Assert.True(result);
        Assert.True(login.UserExists(username));
    }

    [Fact]
    void TestRegisterMultipleUsers()
    {
        Login login = new Login(testFilePath);
        
        bool result1 = login.RegisterUser("user1", "pass1");
        bool result2 = login.RegisterUser("user2", "pass2");
        bool result3 = login.RegisterUser("user3", "pass3");
        
        Assert.True(result1);
        Assert.True(result2);
        Assert.True(result3);
        Assert.Equal(3, login.GetAllUsers().Count);
    }

    [Fact]
    void TestRegisterDuplicateUser()
    {
        Login login = new Login(testFilePath);
        string username = "duplicate";
        
        bool firstRegistration = login.RegisterUser(username, "pass1");
        bool secondRegistration = login.RegisterUser(username, "pass2");
        
        Assert.True(firstRegistration);
        Assert.False(secondRegistration); // Should fail on duplicate
    }

    [Fact]
    void TestRegisterEmptyUsername()
    {
        Login login = new Login(testFilePath);
        
        bool result = login.RegisterUser("", "password");
        
        Assert.False(result);
    }

    [Fact]
    void TestRegisterEmptyPassword()
    {
        Login login = new Login(testFilePath);
        
        bool result = login.RegisterUser("username", "");
        
        Assert.False(result);
    }

    [Fact]
    void TestRegisterEmptyBoth()
    {
        Login login = new Login(testFilePath);
        
        bool result = login.RegisterUser("", "");
        
        Assert.False(result);
    }

    [Fact]
    void TestRegisterWhitespaceUsername()
    {
        Login login = new Login(testFilePath);
        
        bool result = login.RegisterUser("   ", "password");
        
        Assert.False(result);
    }

    [Fact]
    void TestAuthenticateUser()
    {
        Login login = new Login(testFilePath);
        string username = "testuser";
        string password = "testpass";
        
        login.RegisterUser(username, password);
        bool authenticated = login.AuthenticateUser(username, password);
        
        Assert.True(authenticated);
    }

    [Fact]
    void TestAuthenticateInvalidUsername()
    {
        Login login = new Login(testFilePath);
        login.RegisterUser("testuser", "testpass");
        
        bool authenticated = login.AuthenticateUser("wronguser", "testpass");
        
        Assert.False(authenticated);
    }

    [Fact]
    void TestAuthenticateInvalidPassword()
    {
        Login login = new Login(testFilePath);
        login.RegisterUser("testuser", "testpass");
        
        bool authenticated = login.AuthenticateUser("testuser", "wrongpass");
        
        Assert.False(authenticated);
    }

    [Fact]
    void TestAuthenticateNonexistentUser()
    {
        Login login = new Login(testFilePath);
        login.RegisterUser("testuser", "testpass");
        
        bool authenticated = login.AuthenticateUser("nonexistent", "somepass");
        
        Assert.False(authenticated);
    }

    [Fact]
    void TestAuthenticateEmptyUsername()
    {
        Login login = new Login(testFilePath);
        login.RegisterUser("testuser", "testpass");
        
        bool authenticated = login.AuthenticateUser("", "testpass");
        
        Assert.False(authenticated);
    }

    [Fact]
    void TestAuthenticateEmptyPassword()
    {
        Login login = new Login(testFilePath);
        login.RegisterUser("testuser", "testpass");
        
        bool authenticated = login.AuthenticateUser("testuser", "");
        
        Assert.False(authenticated);
    }

    [Fact]
    void TestUserExists()
    {
        Login login = new Login(testFilePath);
        login.RegisterUser("testuser", "testpass");
        
        bool exists = login.UserExists("testuser");
        bool notExists = login.UserExists("nonexistent");
        
        Assert.True(exists);
        Assert.False(notExists);
    }

    [Fact]
    void TestGetAllUsers()
    {
        Login login = new Login(testFilePath);
        login.RegisterUser("user1", "pass1");
        login.RegisterUser("user2", "pass2");
        login.RegisterUser("user3", "pass3");
        
        var allUsers = login.GetAllUsers();
        
        Assert.Equal(3, allUsers.Count);
        Assert.Contains(("user1", "pass1"), allUsers);
        Assert.Contains(("user2", "pass2"), allUsers);
        Assert.Contains(("user3", "pass3"), allUsers);
    }

    [Fact]
    void TestGetAllUsersEmpty()
    {
        Login login = new Login(testFilePath);
        
        var allUsers = login.GetAllUsers();
        
        Assert.Empty(allUsers);
    }

    [Fact]
    void TestAuthenticateWithMultipleUsers()
    {
        Login login = new Login(testFilePath);
        login.RegisterUser("user1", "pass1");
        login.RegisterUser("user2", "pass2");
        login.RegisterUser("user3", "pass3");
        
        Assert.True(login.AuthenticateUser("user1", "pass1"));
        Assert.True(login.AuthenticateUser("user2", "pass2"));
        Assert.True(login.AuthenticateUser("user3", "pass3"));
        Assert.False(login.AuthenticateUser("user1", "pass3")); // Wrong password
    }

    [Fact]
    void TestCaseSensitiveUsername()
    {
        Login login = new Login(testFilePath);
        login.RegisterUser("TestUser", "password");
        
        // Should be case-sensitive
        Assert.True(login.AuthenticateUser("TestUser", "password"));
        Assert.False(login.AuthenticateUser("testuser", "password"));
    }

    [Fact]
    void TestCaseSensitivePassword()
    {
        Login login = new Login(testFilePath);
        login.RegisterUser("user", "TestPass");
        
        // Should be case-sensitive
        Assert.True(login.AuthenticateUser("user", "TestPass"));
        Assert.False(login.AuthenticateUser("user", "testpass"));
    }
}
