using System.ComponentModel.DataAnnotations;

namespace BrowserGame.Models
{
	public class Message
	{
		public int Id { get; set; }

		[Display(Name = "From")]
		public string SenderName { get; set; }
		public int SenderId { get; set; }

		[Required]
		[StringLength(maximumLength: 100, MinimumLength = 1)]
		[Display(Name = "Recipient")]
		public string RecipientName { get; set; }
		public int RecipientId { get; set; }

		[DataType(DataType.DateTime)]
		[Display(Name = "Sent At")]
		public DateTime SentAt { get; set; } = DateTime.Now;
		public bool Unread { get; set; } = true;

		[Required]
		[StringLength(maximumLength: 100, MinimumLength = 1)]
		public string Title { get; set; }

		[Required]
		[StringLength(maximumLength: 2000, MinimumLength = 1)]
		[Display(Name = "Message Body")]
		public string MessageBody { get; set; }
	}
}
