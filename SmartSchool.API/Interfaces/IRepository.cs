using SmartSchool.API.Helpers;
using SmartSchool.API.Models;

namespace SmartSchool.API.Interfaces;
public interface IRepository
{
    void Add<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    bool SaveChanges();

    // Alunos
    Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false);
    Task<PageList<Aluno>> GetAllAlunosByDisciplinaIdAsync(PageParams pageParams, int disciplinaId, bool includeProfessor = false);
    Task<Aluno> GetAlunoByIdAsync(int alunoid, bool includeProfessor);

    // Professores
    Professor[] GetAllProfessores(bool includeAlunos = false);
    Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false);
    Professor GetProfessorById(int professorId, bool includeAlunos = false);

}
