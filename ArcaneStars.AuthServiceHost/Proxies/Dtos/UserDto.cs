using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArcaneStars.AuthServiceHost.Proxies.Dtos
{

    public class UserDto
    {
        public int Id { get; set; }

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
