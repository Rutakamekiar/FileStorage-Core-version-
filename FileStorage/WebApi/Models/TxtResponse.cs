namespace WebApi.Models
{
    public class TxtResponse
    {
        public TxtResponse(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}