using Microsoft.AspNetCore.Components;

public class LoginService
{
    public static readonly Dictionary<string, string> users = new Dictionary<string, string>()
    {
        {"Admin", "Admin@123" },
        {"Staff", "Staff@123" }
    };

    public static string CurrentUserRole { get; private set; }

    public static bool ValidateCredentials(string username, string password)
    {
        if (users.ContainsKey(username) && users[username] == password)
        {
            CurrentUserRole = username;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsAdmin(string username)
    {
        return username == "Admin";
    }

    public static bool IsStaff(string username)
    {
        return username == "Staff";
    }

    public static bool ValidateAdminPassword(string adminPassword)
    {        
        string correctAdminPassword = "Admin@123";
        return adminPassword == correctAdminPassword;
    }

    public static void Logout()
    {        
        CurrentUserRole = null;        
    }
}
