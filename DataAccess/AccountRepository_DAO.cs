using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class AccountRepository_DAO
{
/*    private static AccountRepository_DAO? instance;
    private static readonly object instanceLock = new();
    private readonly MotelManagement2024DbContext _context = new();

    private AccountRepository_DAO() { }
    public static AccountRepository_DAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                instance ??= new AccountRepository_DAO();
                return instance;
            }
        }
    }

    // ==== Gets ==== //
    public UserAccount? GetUserAccountById(int id)
    {
        return _context.UserAccounts.FirstOrDefault(acc => acc.Id == id);
    }

    public UserAccount? GetUserAccountByEmail(string email)
    {
        return _context.UserAccounts.FirstOrDefault(acc => acc.Email == email);
    }

    public UserAccount? GetUserAccountByFullName(string fullName)
    {
        return _context.UserAccounts.FirstOrDefault(acc => acc.FullName.Contains(fullName));
    }

    // ==== Lists ==== //
    public List<UserAccount> ListUserAccount() => [.. _context.UserAccount];

    // ==== Check login ==== //
    public UserAccount? CheckUserAccountLogin(string email, string password)
    {
        return _context.UserAccounts.FirstOrDefault(acc => acc.Email!.Equals(email) && acc.Password.Equals(password));
    }

    // ==== CRUD ==== //
    public bool CreateUserAccount(UserAccount newAccount)
    {
        try
        {
            if (GetUserAccountById(newAccount.Id) != null)
            {
                return false;
            }
            else
            {
                _context.UserAccounts.Add(newAccount);
                _context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return true;
    }

    public bool UpdateUserAccount(UserAccount userAccount)
    {
        try
        {
            var getUserAccount = GetUserAccountById(userAccount.Id);
            if (getUserAccount != null)
            {
                _context.Entry(getUserAccount).State = EntityState.Detached;
                _context.Entry(getUserAccount).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return true;
    }

    public bool RemoveUserAccount(int id)
    {
        try
        {
            if (GetUserAccountById(id) != null)
            {
                var rm = _context.UserAccounts.First(acc => acc.Id == id);
                _context.Remove(rm);
                _context.SaveChanges();
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return true;

        public int GetMaxId() => _context.UserAccounts.Max(acc => acc.Id);
    }*/
}
