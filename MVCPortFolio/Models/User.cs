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
        [Required(ErrorMessage = "이름을 입력하세요!")]
        [DataType(DataType.Text)]
        [StringLength(200)]
        public string UserName { get; set; }
        /// <summary>
        /// 유저이메일
        /// </summary>
        [Required(ErrorMessage = "이메일을 입력하세요!")]
        [DataType(DataType.EmailAddress)]
        [StringLength(200)]
        public string Email { get; set; }
        /// <summary>
        /// 유저패스워드
        /// </summary>
        [Required(ErrorMessage = "비밀번호를 입력하세요!")]
        [DataType(DataType.Password)]
        [StringLength(200)]
        public string Password { get; set; }


    }
}
