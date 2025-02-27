using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DTO;

public class UnitTest{
    private AppDBContext GetInMemoryDbContext(){
        var options = new DbContextOptionsBuilder<AppDBContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        var context = new AppDBContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    #region Patch
    [Fact]
    public async Task TareaExiste() {
        var context = GetInMemoryDbContext();
        var tarea = new TaskDTO { Id = 1, Nombre = "Prueba", FechaInicio = DateTime.Now, FechaFin = DateTime.Now.AddDays(1), Completada = 0 };
        context.Tasks.Add(tarea);
        await context.SaveChangesAsync();
        var controller = new TaskController(context);
        var result = await controller.PatchTask(1, 1);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Tarea actualizada", okResult.Value);
    }

    [Fact]
    public async Task TareaNoExiste() {
        var context = GetInMemoryDbContext();
        var tarea = new TaskDTO { Id = 1, Nombre = "Prueba", FechaInicio = DateTime.Now, FechaFin = DateTime.Now.AddDays(1), Completada = 0 };
        context.Tasks.Add(tarea);
        await context.SaveChangesAsync();
        var controller = new TaskController(context);
        var result = await controller.PatchTask(99, 1);
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Tarea no existe", badRequest.Value);
    }

    [Fact]
    public async Task EstadoInvalido() {
        var context = GetInMemoryDbContext();
        var controller = new TaskController(context);
        controller = new TaskController(null);
        var result = await controller.PatchTask(1, 1);
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }
    #endregion

    #region insert
    [Fact]
    public async Task InsertTask() {
        var context = GetInMemoryDbContext();
        var tarea = new InsertTaskDTO { Nombre = "Prueba", FechaInicio = DateTime.Now.ToString(), FechaFin = DateTime.Now.AddDays(1).ToString(), Completada = 0 };
        var controller = new TaskController(context);
        var result = await controller.InsertTask(tarea);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Tarea insertada", okResult.Value);
    }

    [Fact]
    public async Task InsertTareaFechaInvalida() {
       var context = GetInMemoryDbContext();
        var tarea = new InsertTaskDTO { Nombre = "Prueba", FechaInicio = DateTime.Now.ToString(), FechaFin = DateTime.Now.AddDays(-1).ToString(), Completada = 0 };
        var controller = new TaskController(context);
        var result = await controller.InsertTask(tarea);
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Las fechas son erroneas\n", badRequest.Value);
    }

    [Fact]
    public async Task InsertTareaInvalido() {
       var context = GetInMemoryDbContext();
        InsertTaskDTO? tarea = null;
        var controller = new TaskController(context);
        var result = await controller.InsertTask(tarea);
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    #endregion
}
