namespace MyKoloWebApi.DTOs
{
    public class AddExpenseDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}