using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Model;

namespace WebApplication1.ModelDTO;

public class MessageDTO
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Messaggio { get; set; }


	public MessageDTO()
	{

	}
	public MessageDTO(Message c)
	{
		(Nome,Email,Messaggio) = (c.Nome, c.Email, c.Messaggio);
	}
}