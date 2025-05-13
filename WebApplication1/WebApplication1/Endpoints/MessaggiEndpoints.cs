using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;
using WebApplication1.ModelDTO;

namespace WebApplication1.Endpoints
{
    public static class MessaggiEndpoints
    {
        public static void MapUserMessageEndpoints(this WebApplication app)
        {
            // Endpoint per inviare un messaggio
            app.MapPost("/api/messages", async ([FromBody] MessageDTO messageDTO, AppDbContext db) =>
            {
                if (string.IsNullOrEmpty(messageDTO.Nome))
                {
                    return Results.BadRequest("Tutti i campi sono obbligatori");
                }

            var nuovoMessage = new Message
            {
                Nome = messageDTO.Nome,
                Email = messageDTO.Email,
                Messaggio = messageDTO.Messaggio
            };

                db.Messages.Add(nuovoMessage);
                await db.SaveChangesAsync();

                return Results.Ok("Messaggio inviato con successo!");
            });

            // Endpoint per ottenere tutti i messaggi
            app.MapGet("/api/messages", async (AppDbContext db) =>
            {
                var messages = await db.Messages.ToListAsync();
                return Results.Ok(messages);
            });

            // Endpoint per eliminare un messaggio
            app.MapDelete("/api/messages/{id}", async (int id, AppDbContext db) =>
            {
                var message = await db.Messages.FindAsync(id);
                if (message == null)
                {
                    return Results.NotFound("Messaggio non trovato");
                }

                db.Messages.Remove(message);
                await db.SaveChangesAsync();

                return Results.Ok("Messaggio rimosso con successo!");
            });
        }
    }
}