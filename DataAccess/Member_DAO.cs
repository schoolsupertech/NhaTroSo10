using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class Member_DAO
{
    private static Member_DAO? instance;
    private static readonly object instanceLock = new();
    private readonly MotelManagement2024DbContext _context = new();

    private Member_DAO() { }
    public static Member_DAO Instance
    {
        get
        {
            lock(instanceLock)
            {
                return instance ??= new Member_DAO();
            }
        }
    }

    // ==== Get's ==== //
    public Member? GetMemberById(int id)
    {
        return _context.Members.FirstOrDefault(m => m.MemberId == id);
    }

    public Member? GetMemberByName(string name)
    {
        return _context.Members.FirstOrDefault(m => m.FullName.Contains(name));
    }

    // ==== Lists ==== //
    public List<Member> GetMembers()
    {
        return [.. _context.Members];
    }

    // ==== Pagination ==== //
    public List<Member> GetMembersPaginated(int page, int pageSize)
    {
        return [.. _context.Members.AsNoTracking().Skip((page - 1) * pageSize).Take(pageSize)];
    }

    // ==== CRUD ==== //
    public bool CreateMember(Member member)
    {
        try
        {
            if(GetMemberById(member.MemberId) is not null)
            {
                return false;
            }
            _context.Members.Add(member);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return true;
    }

    public bool UpdateMember(Member modMember)
    {
        try
        {
            var currentMember = GetMemberById(modMember.MemberId);
            if (currentMember is null)
            {
                return false;
            }

            if(!string.IsNullOrEmpty(modMember.FullName))
            {
                currentMember.FullName = modMember.FullName;
            }
            if(modMember.DateOfBirth != default)
            {
                currentMember.DateOfBirth = modMember.DateOfBirth;
            }
            if (!string.IsNullOrEmpty(modMember.Gender))
            {
                currentMember.Gender = modMember.Gender;
            }
            if (!string.IsNullOrEmpty(modMember.IdentificationCard))
            {
                currentMember.IdentificationCard = modMember.IdentificationCard;
            }
            if (modMember.DateOfIssue != default)
            {
                currentMember.DateOfIssue = modMember.DateOfIssue;
            }
            if (!string.IsNullOrEmpty(modMember.PhoneNumber))
            {
                currentMember.PhoneNumber = modMember.PhoneNumber;
            }
            if (!string.IsNullOrEmpty(modMember.PermanentAddress))
            {
                currentMember.PermanentAddress = modMember.PermanentAddress;
            }
            if (!string.IsNullOrEmpty(modMember.TemporaryAddress))
            {
                currentMember.TemporaryAddress = modMember.TemporaryAddress;
            }

            currentMember.UpdatedDate = DateTime.Now;

            _context.Entry(currentMember).State = EntityState.Modified;
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return true;
    }

    public bool DeleteMember(int id)
    {
        try
        {
            if (GetMemberById(id) is null)
            {
                return false;
            }
            var rmMember = _context.Members.First(m => m.MemberId == id);
            _context.Members.Remove(rmMember);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return true;
    }

    public int GetMaxId() => _context.Members.Max(m => m.MemberId);
}
