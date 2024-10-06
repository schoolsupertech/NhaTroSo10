using BusinessObjects.Models;
using DataAccess;

namespace Repositories;

public class MemberService : IMemberService
{
    public Member? GetMemberById(int id) => Member_DAO.Instance.GetMemberById(id);
    public Member? GetMemberByName(string name) => Member_DAO.Instance.GetMemberByName(name);
    public List<Member> GetMembers() => Member_DAO.Instance.GetMembers();
    public List<Member> GetMembersPaginated(int page, int pageSize) => Member_DAO.Instance.GetMembersPaginated(page, pageSize);
    public bool CreateMember(Member member) => Member_DAO.Instance.CreateMember(member);
    public bool UpdateMember(Member modMember) => Member_DAO.Instance.UpdateMember(modMember);
    public bool DelteMember(int id) => Member_DAO.Instance.DeleteMember(id);
    public int GetMaxId() => Member_DAO.Instance.GetMaxId();
}
