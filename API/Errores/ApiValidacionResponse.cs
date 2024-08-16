namespace API.Errores
{
    public class ApiValidacionResponse : ApiErrorResponse
    {
        public ApiValidacionResponse() : base (400)
        {
            
        }

        public IEnumerable<string> Errores { get; set; }
    }
}
