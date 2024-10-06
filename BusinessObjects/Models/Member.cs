using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models;

public partial class Member
{
    [Key]
    public int MemberId { get; set; }

    public int? RoomId { get; set; }

    public string FullName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Gender { get; set; } = null!;

    public string IdentificationCard { get; set; } = null!;

    public DateTime DateOfIssue { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public string PermanentAddress { get; set; } = null!;

    public string TemporaryAddress { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Room? Room { get; set; }
}
