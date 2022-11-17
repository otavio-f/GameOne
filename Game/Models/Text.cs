namespace Game.Models
{
    //TODO: Which is better:
    //  - fetch all languages in a single class (every Text has a list of strings)
    //  - or fetch a single language text (composite key for content id and lang)
    public class Text
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public string Content { get; set; }

        public override string ToString() => Content;
        
    }
}