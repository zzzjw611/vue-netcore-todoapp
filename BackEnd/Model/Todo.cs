namespace BackEnd.Model
{
    public class Todo
    {
        public int Id { get; set; }           // Unique ID
        public string Title { get; set; }     // task content
        public bool Completed { get; set; }   // finished or not 
    }
}
