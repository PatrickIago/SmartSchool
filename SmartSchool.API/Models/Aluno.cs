namespace SmartSchool.API.Models;
public class Aluno
{
    public Aluno() { }
    public int Id { get; set; }
    public int Matricula { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Telefone { get; set; }
    public DateTime DataNasc { get; set; }
    public DateTime DataInic { get; set; }
    public DateTime? DataFim { get; set; } = null;
    public bool Ativo { get; set; } = true;

    public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }

    public Aluno(int id, string nome, string sobrenome, string telefone, int matricula, DateTime dataNasc)
    {
        Id = id;
        Matricula = matricula;
        Nome = nome;
        Sobrenome = sobrenome;
        Telefone = telefone;
        DataNasc = dataNasc;
    }
}
