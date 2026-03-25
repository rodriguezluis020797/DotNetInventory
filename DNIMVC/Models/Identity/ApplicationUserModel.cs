using Microsoft.AspNetCore.Identity;

namespace DNIMVC.Models.Identity;

/// <summary>
///     Represents an application user in the identity system.
///     Inherits from <see cref="IdentityUser{TKey}" /> where TKey is <see cref="Guid" />,
///     which provides built-in identity fields such as Id, UserName, Email, PasswordHash,
///     SecurityStamp, and lockout/two-factor support. Custom fields specific to this
///     application are defined below.
/// </summary>
public sealed class ApplicationUserModel : IdentityUser<Guid>
{
    /// <summary>
    ///     Creates an empty object.
    /// </summary>
    public ApplicationUserModel()
    {
    }

    /// <summary>
    ///     Creates a new application user to be saved onto the database.
    /// </summary>
    /// <param name="firstName">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="email">The email of the user.</param>
    public ApplicationUserModel(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        NormalizedEmail = Email.ToLower();
        CreateDate = DateTime.Now;
    }

    /// <summary>
    ///     The user's first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    ///     The user's last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    ///     The UTC date and time the user account was created.
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    ///     Indicates whether the user account is active.
    ///     Inactive users are denied access without being deleted from the system.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    ///     The collection of refresh tokens associated with this user.
    ///     Each token represents an active session.
    /// </summary>
    public ICollection<RefreshTokenModel> RefreshTokens { get; set; }
}