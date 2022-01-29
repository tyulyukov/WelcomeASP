using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelcomeASP.Data.Entities.Trello
{
    public class Card
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ColumnId { get; set; }
        public Column Column { get; set; }
        public String Title { get; set; }
        public String Body { get; set; }
    }
}
