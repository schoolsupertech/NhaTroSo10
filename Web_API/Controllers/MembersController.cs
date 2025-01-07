using Microsoft.AspNetCore.Mvc;
using BusinessObjects.Models;
using Repositories;
using Web_API.Models;
using Microsoft.AspNetCore.Cors;

namespace Web_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MembersController(IMemberService memberService) : ControllerBase
{
    private readonly IMemberService _memberService = memberService;

    // GET: api/Members
    [HttpGet]
    public IActionResult GetMembers()
    {
        return Ok(_memberService.GetMembers());
    }

    // GET: api/Members/5
    [HttpGet("{id}")]
    public IActionResult GetMemberById(int id)
    {
        var member = _memberService.GetMemberById(id);

        if (member is null)
        {
            return NotFound();
        }

        return Ok(member);
    }

    // PUT: api/Members/5
    [HttpPut("{id}")]
    public IActionResult PutMember(int id, MemberRequestUpdate member)
    {
        try
        {
            if (id > 0)
            {
                member.MemberId = id;
                if (_memberService.UpdateMember(ConvertObjectOnPut(member)))
                {
                    return CreatedAtAction(nameof(GetMemberById), new { id = member.MemberId }, member);
                }
                else
                {
                    return NotFound("Id: " + id + " is not exists");
                }
            }
            else
            {
                return BadRequest("Id must greater than 0");
            }
        }
        catch (Exception ex)
        {
            return Problem(statusCode: 500, detail: ex.Message);
        }
    }

    // POST: api/Members
    [HttpPost]
    public IActionResult PostMember(MemberRequest member)
    {
        try
        {
            //member.MemberId = _memberService.GetMaxId() + 1;
            if (member.RoomId <= 0)
            {
                member.RoomId = null;
            }
            if (_memberService.CreateMember(ConvertObjectOnPost(member)))
            {
                return CreatedAtAction(nameof(GetMemberById), new { id = member.MemberId }, member);
            }
            else
            {
                return BadRequest("This member is already exists");
            }
        }
        catch (Exception ex)
        {
            return Problem(statusCode: 500, detail: ex.Message);
        }
    }

    // DELETE: api/Members/5
    [HttpDelete("{id}")]
    public IActionResult DeleteMember(int id)
    {
        try
        {
            if (_memberService.DelteMember(id))
            {
                return Ok("Successfully deleted!");
            }
            else
            {
                return NotFound("Id: " + id + " is not exists");
            }
        }
        catch (Exception ex)
        {
            return Problem(statusCode: 500, detail: ex.Message);
        }
    }

    private static Member ConvertObjectOnPost(MemberRequest request)
    {
        Member result = new()
        {
            MemberId = request.MemberId,
            RoomId = request.RoomId,
            FullName = request.FullName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            IdentificationCard = request.IdentificationCard,
            DateOfIssue = request.DateOfIssue,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            PermanentAddress = request.PermanentAddress,
            TemporaryAddress = request.TemporaryAddress
        };

        return result;
    }

    private static Member ConvertObjectOnPut(MemberRequestUpdate request)
    {
        Member result = new()
        {
            MemberId = request.MemberId,
            RoomId = request.RoomId,
            FullName = request.FullName!,
            DateOfBirth = request.DateOfBirth ?? default,
            Gender = request.Gender!,
            IdentificationCard = request.IdentificationCard!,
            DateOfIssue = request.DateOfIssue ?? default,
            PhoneNumber = request.PhoneNumber!,
            Email = request.Email,
            PermanentAddress = request.PermanentAddress!,
            TemporaryAddress = request.TemporaryAddress!
        };

        return result;
    }
}
