namespace BookstoreAppCommand.Models.Events
{
    public class ReservedBookEvent
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}

