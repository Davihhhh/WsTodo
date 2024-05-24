using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{

    [Table("groups", Schema = "todoapp")]
    public class Group
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

    }
}
