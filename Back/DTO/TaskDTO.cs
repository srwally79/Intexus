using System.Data.SqlTypes;

namespace DTO{
    public class TaskDTO{
        public int Id {get; set;}
        public required string Nombre { get; set;}
        public DateTime FechaInicio {get; set;}
        public DateTime FechaFin {get; set;}
        public int Completada {get; set;}
    }

    public class InsertTaskDTO{
        public required string Nombre { get; set;}
        public required string FechaInicio {get; set;}
        public required string FechaFin {get; set;}
        public int Completada {get; set;}
    }
}
