namespace SmartSchool.API.Models;
public class Professor
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Registro { get; set; }
    public string SobreNome { get; set; }
    public string? Telefone { get; set; }
    public DateTime DataInic { get; set; }
    public DateTime? DataFim { get; set; } = null;
    public bool Ativo { get; set; } = true;
    public IEnumerable<Disciplina> Disciplinas { get; set; }
    public Professor() { }

    public Professor(int id, string nome, string sobreNome, int registro)
    {
        Id = id;
        Registro = registro;
        SobreNome = sobreNome;
        Nome = nome;
    }
}
