using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LocalizaAPI.Domain.Entities
{
    //[Table("perfil")]
    //public class Perfil
    //{

    //    [Key]
    //    public int Id { get; set; }
    //    public string Nome { get; set; }

    //}

    public enum Perfil
    {
        Operador = 1,
        Cliente = 2
    }
}
