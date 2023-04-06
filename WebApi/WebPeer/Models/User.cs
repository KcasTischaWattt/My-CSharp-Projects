using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebPeer.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    [DataContract]
    public class User : IComparable
    {
        /// <summary>
        /// Адрес почты
        /// </summary>
        [DataMember]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [DataMember]
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Реализация интерфейса IComparable
        /// </summary>
        /// <param name="obj">ПОльзователь, с которым сравнивают</param>
        /// <returns>результат сравнения</returns>
        public int CompareTo(object obj)
        {
            return Email.CompareTo(((User)obj).Email);
        }
    }
}
