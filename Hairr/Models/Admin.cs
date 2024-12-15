using System.ComponentModel.DataAnnotations;

namespace Hairr.Models
{
	public class Admin
	{
		[Key]
        public int AdminID { get; set; }

		[StringLength(20)]
	
		public string? UserName { get; set; }

		[StringLength(20)]
		public string? Password { get; set; }

		[StringLength(1)]
		public string? Role { get; set; }

    }
}
