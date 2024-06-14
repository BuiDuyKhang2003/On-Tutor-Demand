using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    [Table("ChatMessage", Schema = "dbo")]
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Conversation")]
        public int ConversationId { get; set; }

        [ForeignKey("Sender")]
        public int SenderId { get; set; }

        public string Content { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime EditedDate { get; set; }

        public virtual Conversation Conversation { get; set; }
        public virtual Account Sender { get; set; }
    }

}
