﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KormoranShared.Models
{
	[Table("states")]
	public class State
	{
		[Key]
		[Column("state_id")]
		public int Id { get; set; }

		[Required]
		[Column("name")]
		public string Name { get; set; }

		public override string ToString()
		{
			return Name ?? string.Empty;
		}
	}
}