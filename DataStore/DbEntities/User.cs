using System;
using System.Collections.Generic;

namespace DataStore.DbEntities;

public partial class User
{
    public long UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public string PasswordHasg { get; set; } = null!;

    public string? Street { get; set; }

    public string City { get; set; } = null!;

    public string? State { get; set; }

    public string PostCode { get; set; }

    public string? Country { get; set; }

    public bool? Active { get; set; }

    public DateTime CreatedOnUtc { get; set; }
}
