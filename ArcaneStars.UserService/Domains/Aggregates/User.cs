using ArcaneStars.Infrastructure.Secrets;
using ArcaneStars.Service.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.UserService.Domains.Aggregates
{
    public class User : Entity<int>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Mobile { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        /// <summary>
        /// 用户分区（用户属于哪个app）
        /// </summary>
        public UserPartition Partition { get; set; } = UserPartition.JianPing;

        /// <summary>
        /// 用户渠道（用户来源）
        /// </summary>
        public UserChannel Channel { get; set; } = UserChannel.JianPingApp;

        public bool IsSuspend { get; set; } = false;

        public bool CheckPassword(string password)
        {
            var encryptPassword = SecretProvider.EncryptToMD5(password);
            return encryptPassword == this.Password;
        }

        public void SetSuspend()
        {
            this.IsSuspend = true;
        }

        public void HidePassword()
        {
            this.Password = "";
        }
    }

    public static class UserFactory
    {
        public static User CreateInstance(string userName, string mobile, string email, string password, string nickName, string operatedBy, UserPartition partition = UserPartition.JianPing, UserChannel channel = UserChannel.JianPingApp)
        {
            var now = DateTime.Now;
            return new User
            {
                Mobile = mobile,
                Channel = channel,
                CreatedBy = operatedBy,
                CreatedOn = now,
                Email = email,
                NickName = nickName,
                Partition = partition,
                Password = SecretProvider.EncryptToMD5(password),
                UpdatedBy = operatedBy,
                UpdatedOn = now,
                UserName = userName,
                IsSuspend = false
            };
        }
    }

    public enum UserPartition
    {
        JianPing = 1
    }

    public enum UserChannel
    {
        JianPingApp = 1
    }
}
