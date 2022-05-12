using Dapper_Crud_API.Enum;
using System.ComponentModel.DataAnnotations;

namespace Dapper_Crud_API.Model
{
    public class Dapper_Test
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public Status Status { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
