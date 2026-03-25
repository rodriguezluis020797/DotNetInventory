using System.Security.Cryptography;

namespace DNIMVC.Models.Identity;

/// <summary>
///     Represents a JWT refresh token issued to a user upon authentication.
///     Refresh tokens are used to obtain new JWT access tokens without requiring
///     the user to log in again. Each token is tied to a specific user and session.
/// </summary>
public class RefreshTokenModel
{
    /// <summary>
    ///     Creates an empty object.
    /// </summary>
    public RefreshTokenModel()
    {
    }

    /// <summary>
    ///     Creates a new refresh token for the specified user.
    ///     Automatically generates a cryptographically secure token string,
    ///     sets the expiration to 7 days from now, and records the creation time.
    /// </summary>
    /// <param name="applicationUserId">The ID of the user this token belongs to.</param>
    public RefreshTokenModel(Guid applicationUserId)
    {
        ApplicationUserId = applicationUserId;
        Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        ExpireDate = DateTime.UtcNow.AddDays(7);
        CreateDate = DateTime.UtcNow;
    }

    /// <summary>
    ///     The primary key of this refresh token.
    /// </summary>
    public Guid RefreshTokenId { get; set; }

    /// <summary>
    ///     Foreign key referencing the user this token belongs to.
    /// </summary>
    public Guid ApplicationUserId { get; set; }

    /// <summary>
    ///     The cryptographically secure, base64-encoded token string.
    ///     Generated using <see cref="RandomNumberGenerator" /> to ensure
    ///     it is unpredictable and safe for use as a security credential.
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    ///     The UTC date and time this token expires.
    ///     Tokens are valid for 7 days from creation.
    /// </summary>
    public DateTime ExpireDate { get; set; }

    /// <summary>
    ///     The UTC date and time this token was created.
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    ///     The UTC date and time this token was revoked, if applicable.
    ///     A null value indicates the token has not been revoked.
    ///     Tokens are revoked on logout or when a token rotation detects reuse.
    /// </summary>
    public DateTime? RevokeDate { get; set; }

    /// <summary>
    ///     Returns true if the token has passed its expiration date.
    /// </summary>
    public bool IsExpired => DateTime.UtcNow >= ExpireDate;

    /// <summary>
    ///     Returns true if the token has been explicitly revoked.
    /// </summary>
    public bool IsRevoked => RevokeDate.HasValue;

    /// <summary>
    ///     Returns true if the token is valid for use — not expired and not revoked.
    ///     This is the primary check used during token refresh validation.
    /// </summary>
    public bool IsActive => !IsExpired && !IsRevoked;

    /// <summary>
    ///     Navigation property to the user this token belongs to.
    /// </summary>
    public ApplicationUserModel ApplicationUser { get; set; }
}