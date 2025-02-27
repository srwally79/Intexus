using DTO;

namespace Util{
    public class UtilTask(){
        private static string error = "";
        public static string Validaciones(InsertTaskDTO TaskParam){
            error = "";
            if(TaskParam.Nombre == ""){
                error += "El nombre es necesario\n";
            }
            if(!DateTime.TryParse(TaskParam.FechaInicio,out DateTime FechaInicioDate)){
            error += "La fecha de inicio es necesaria\n"; 
            }
            if(!DateTime.TryParse(TaskParam.FechaFin,out DateTime FechaFinDate)){
            error += "La fecha final es necesaria\n"; 
            }
            error = ValidarCompletada(TaskParam.Completada);
            if (FechaInicioDate > FechaFinDate){
                error +="Las fechas son erroneas\n";
            }
            return error;
        }

        public static string ValidarCompletada(int Estado){
            string temperror = "";
            if(Estado.ToString() == ""){
                temperror += "No se indica si la tarea esta completada o no\n"; 
            }
            if(Estado < 0 || Estado > 1){
                temperror += "El formato de tarea completada es erroneo\n";
            }
            return temperror;
        }
    }
}