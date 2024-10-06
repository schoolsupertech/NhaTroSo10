using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Web_API.Models;

public class MemberRequestUpdate
{
    [Key]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int MemberId { get; set; }

    public int? RoomId { get; set; }

    public string? FullName { get; set; }

    [DisplayName("Date of birth")]
    [DisplayFormat(DataFormatString = ("{0:dd MMM yyyy}"))]
    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? IdentificationCard { get; set; }

    [DisplayName("Date of issue")]
    [DisplayFormat(DataFormatString = ("{0:dd MMM yyyy}"))]
    public DateTime? DateOfIssue { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? PermanentAddress { get; set; }

    public string? TemporaryAddress { get; set; }
}
