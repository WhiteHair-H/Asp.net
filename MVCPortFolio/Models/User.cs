using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPortFolio.Models
{
    public class User
    {
        /// <summary>
        /// 유저번호
        /// </summary>
        [Key]
        public int UserNo { get; set; }
        /// <summary>
        /// 유저이름
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 유저이메일
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 유저패스워드
        /// </summary>
        public string Password { get; set; }


    }
}
