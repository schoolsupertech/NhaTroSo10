using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Web_API.Models;

public class MemberRequest
{
    [Key]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int MemberId { get; set; }

    public int? RoomId { get; set; }

    public string FullName { get; set; } = null!;

    [DisplayName("Date of birth")]
    [DisplayFormat(DataFormatString = ("{0:dd MMM yyyy}"))]
    public DateTime DateOfBirth { get; set; }

    public string Gender { get; set; } = null!;

    public string IdentificationCard { get; set; } = null!;

    [DisplayName("Date of issue")]
    [DisplayFormat(DataFormatString = ("{0:dd MMM yyyy}"))]
    public DateTime DateOfIssue { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public string PermanentAddress { get; set; } = null!;

    public string TemporaryAddress { get; set; } = null!;
}
