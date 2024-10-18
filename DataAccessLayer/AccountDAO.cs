using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccessLayer
{
    public class AccountDAO : SingletonBase<AccountDAO>
    {
        public AccountMember GetAccountById(string acccountid)
        {
            AccountMember member = new AccountMember();
            if (acccountid.Equals("PS0001"))
            {
                member.MemberId = "PS0001";
                member.MemberPassword = "@1";
                member.MemberRole = 1;
            }
            else
            {
                return null;
            }
            return member;
        }
    }
}
