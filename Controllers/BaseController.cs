using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Warehouse.Models;

public class BaseController<TEntity, TDbContext> : ControllerBase
    where TEntity : class
    where TDbContext : DbContext
{
    protected readonly TDbContext _context;

    public BaseController(TDbContext context)
    {
        _context = context;
    }
    public async Task<ActionResult<IEnumerable<T>>> GetAllEntities<T>() where T : class
    {
        if (_context.Set<T>() == null)
        {
            return NotFound();
        }
        var entities = await _context.Set<T>().ToListAsync();
        if (entities == null || !entities.Any())
        {
            return NotFound();
        }
        return Ok(entities);
    }

    protected async Task<T> GetEntityById<T>(int id) where T : class
    {
        if (_context.Set<T>() == null)
        {
            return null;
        }
        return await _context.Set<T>().FindAsync(id);
    }

    protected async Task<IActionResult> UpdateEntity<T>(T entity) where T : class
    {
        int id = (int)entity.GetType().GetProperty($"{entity.GetType().Name}ID").GetValue(entity);
        var existingEntity = await GetEntityById<T>(id);

        if (existingEntity == null)
        {
            return NotFound();
        }

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    protected async Task<ActionResult<T>> CreateEntity<T>(T entity) where T : class
    {
        if (_context.Set<T>() == null)
        {
            return Problem($"Entity set 'APIdbContext.{typeof(T).Name}s' is null.");
        }

        PropertyInfo idProperty = entity.GetType().GetProperty($"{typeof(T).Name}ID");
        if (idProperty == null)
        {
            return BadRequest();
        }

        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();

        return CreatedAtAction($"Get{typeof(T).Name}", new { id = idProperty.GetValue(entity) }, entity);
    }



    protected async Task<IActionResult> DeleteEntity<T>(int id) where T : class
    {
        if (_context.Set<T>() == null)
        {
            return NotFound();
        }

        var entity = await GetEntityById<T>(id);
        if (entity == null)
        {
            return NotFound();
        }

        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
