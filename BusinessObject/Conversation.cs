using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    [Table("Conversation", Schema = "dbo")]
    public class Conversation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Initiator")]
        public int InitiatorId { get; set; }

        [ForeignKey("Receiver")]
        public int ReceiverId { get; set; }

        public string Subject { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastMessageDate { get; set; }

        public virtual Account Initiator { get; set; }
        public virtual Account Receiver { get; set; }
        public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    }
}
