namespace SmartSchool.API.Models;
// Classe intermediaria
public class AlunoCurso
{
    public int AlunoId { get; set; }
    public Aluno Aluno { get; set; }
    public int CursoId { get; set; }
    public Curso Curso { get; set; }
    public DateTime DataIni { get; set; } = DateTime.Now;
    public DateTime? DataFim { get; set; }
    public int? Nota { get; set; } = null;

    public AlunoCurso() { }

    public AlunoCurso(int alunoId, int cursoId)
    {
        AlunoId = alunoId;
        CursoId = cursoId;
    }
}
