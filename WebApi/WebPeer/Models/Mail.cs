using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebPeer.Models
{
    /// <summary>
    /// Почта
    /// </summary>
    [DataContract]
    public class Mail
    {
        /// <summary>
        /// Тема письма
        /// </summary>
        [DataMember]
        public string Subject { get; set; }

        /// <summary>
        /// Текст письма
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// ID получателя
        /// </summary>
        [DataMember]
        [Required]
        public string ReceiverId { get; set; }

        /// <summary>
        /// ID отправителя
        /// </summary>
        [DataMember]
        [Required]
        public string SenderId { get; set; }
    }
}
