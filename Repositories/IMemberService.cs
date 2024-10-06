using BusinessObjects.Models;

namespace Repositories;

public interface IMemberService
{
    bool CreateMember(Member member);
    bool DelteMember(int id);
    Member? GetMemberById(int id);
    Member? GetMemberByName(string name);
    List<Member> GetMembers();
    List<Member> GetMembersPaginated(int page, int pageSize);
    bool UpdateMember(Member modMember);
    int GetMaxId();
}