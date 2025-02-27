using Microsoft.AspNetCore.Mvc;
using DTO;
using DAL;
using Util;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("tasks")]
public class TaskController : ControllerBase{
    private readonly AppDBContext _context;
    private string error = "";

    public TaskController(AppDBContext context){
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> InsertTask(InsertTaskDTO TaskParam){
        try{
            error = UtilTask.Validaciones(TaskParam);
            if(error == ""){
                var Tarea = new TaskDTO{
                    Nombre = TaskParam.Nombre,
                    FechaInicio = DateTime.Parse(TaskParam.FechaInicio),
                    FechaFin = DateTime.Parse(TaskParam.FechaFin),
                    Completada = TaskParam.Completada
                };
                _context.Tasks.Add(Tarea);
                await _context.SaveChangesAsync();
                return Ok("Tarea insertada");
            }else{
                return BadRequest(error);
            }
        }catch (Exception ex){
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<ActionResult> GetTask(){
        try{
            List<TaskDTO> listado = await _context.Tasks.ToListAsync();
            return Ok(listado);
        }catch (Exception ex){
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPatch("{Id}")]
    public async Task<ActionResult> PatchTask(int Id, [FromBody] int Estado){
        try{
            error = UtilTask.ValidarCompletada(Estado);
            if(error == ""){
                TaskDTO? Tarea = _context.Tasks.FirstOrDefault(t => t.Id == Id);
                if(Tarea != null){
                    Tarea.Completada = Estado;
                    await _context.SaveChangesAsync();
                    return Ok("Tarea actualizada");
                }else{
                    return BadRequest("Tarea no existe");
                }
            }else{
                return BadRequest(error);
            }
        }catch (Exception ex){
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteTask(int Id){
        try{
            TaskDTO? Tarea = _context.Tasks.FirstOrDefault(t => t.Id == Id);
            if(Tarea != null){
                _context.Tasks.Remove(Tarea);
                await _context.SaveChangesAsync();
                return Ok("Tarea eliminada");
            }else{
                return BadRequest("Tarea no existe");
            }
        }catch (Exception ex){
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}

