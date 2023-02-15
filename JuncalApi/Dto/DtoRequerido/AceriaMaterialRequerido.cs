namespace JuncalApi.Dto.DtoRequerido
{
    public class AceriaMaterialRequerido
    {
        public int Id { get; set; }

        public string Nombre { get; set; } 

        public int IdAceria { get; set; }

        public int IdMaterial { get; set; }

        public decimal? Precio { get; set; }

        public string Cod { get; set; } 
    }
}
