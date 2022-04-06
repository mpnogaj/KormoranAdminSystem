using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace KormoranShared.Models
{
	[Table("states")]
	public class State
	{
		[Key]
		[Column("state_id", TypeName = "int(11)")]
		public int Id { get; set; }
		
		[Required]
		[Column("name")]
		public string? Name { get; set; }

        public override string ToString()
        {
			return JsonSerializer.Serialize(this);
        }
    }
}