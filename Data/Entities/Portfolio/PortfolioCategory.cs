using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WelcomeASP.Data.Entities.Portfolio
{
    public class PortfolioCategory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Slug { get; set; }
        public List<PortfolioItem> Items { get; set; }
    }
}
