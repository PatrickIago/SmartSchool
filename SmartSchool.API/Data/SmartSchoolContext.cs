using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Models;
namespace SmartSchool.API.Data;
public class SmartSchoolContext : DbContext
{
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Professor> Professores { get; set; }
    public DbSet<Disciplina> Disciplinas { get; set; }
    public DbSet<AlunoDisciplina> AlunosDisciplinas { get; set; }
    public DbSet<AlunoCurso> AlunosCursos { get; set; }
    public DbSet<Curso> Cursos { get; set; }

    public SmartSchoolContext(DbContextOptions<SmartSchoolContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Chave composta para a tabela de associação
        modelBuilder.Entity<AlunoDisciplina>()
            .HasKey(AD => new { AD.AlunoId, AD.DisciplinaId });

        modelBuilder.Entity<AlunoCurso>()
           .HasKey(AL => new { AL.AlunoId, AL.CursoId });

        // Data estática para usar no lugar de DateTime.Now
        var dataFixa = DateTime.Parse("2025-06-07");

        modelBuilder.Entity<Professor>()
            .HasData(new List<Professor>(){
            new Professor(id: 1, nome: "Lauro", sobreNome: "Oliveira", registro: 1),
            new Professor(id: 2, nome: "Roberto", sobreNome: "Soares", registro: 2),
            new Professor(id: 3, nome: "Ronaldo", sobreNome: "Marconi", registro: 3),
            new Professor(id: 4, nome: "Rodrigo", sobreNome: "Carvalho", registro: 4),
            new Professor(id: 5, nome: "Alexandre", sobreNome: "Montanha", registro: 5),
            });


        modelBuilder.Entity<Curso>()
            .HasData(new List<Curso>(){
            new Curso(id: 1, nome: "Tecnologia da Informação"),
            new Curso(id: 2, nome: "Sistemas de Informação"),
            new Curso(id: 3, nome: "Ciência da Computação")
            });

        modelBuilder.Entity<Disciplina>()
            .HasData(new List<Disciplina>{
            new Disciplina(id: 1, nome: "Matemática", professorId: 1, cursoId: 1) { CargaHoraria = 90 },
            new Disciplina(id: 2, nome: "Matemática", professorId: 1, cursoId: 3) { CargaHoraria = 90 },
            new Disciplina(id: 3, nome: "Física", professorId: 2, cursoId: 3) { CargaHoraria = 75 },
            new Disciplina(id: 4, nome: "Português", professorId: 3, cursoId: 1) { CargaHoraria = 75 },
            new Disciplina(id: 5, nome: "Inglês", professorId: 4, cursoId: 1) { CargaHoraria = 60 },
            new Disciplina(id: 6, nome: "Inglês", professorId: 4, cursoId: 2) { CargaHoraria = 60 },
            new Disciplina(id: 7, "Inglês", 4, 3) { CargaHoraria = 60 },
            new Disciplina(id: 8, "Programação", 5, 1) { CargaHoraria = 120, PrerequeistoId = 1 },
            new Disciplina(id: 9, "Programação", 5, 2) { CargaHoraria = 120 },
            new Disciplina(id: 10, "Programação", 5, 3) { CargaHoraria = 120, PrerequeistoId = 2 }
            });

        modelBuilder.Entity<Aluno>()
            .HasData(new List<Aluno>(){
            new Aluno(id: 1, matricula: 1, nome: "Marta", sobrenome: "Kent", telefone: "33225555", dataNasc: DateTime.Parse("2005-05-28")) { DataInic = dataFixa },
            new Aluno(id: 2, matricula: 2, nome: "Paula", sobrenome: "Isabela", telefone: "3354288", dataNasc: DateTime.Parse("2005-05-28")) { DataInic = dataFixa },
            new Aluno(id: 3, matricula: 3, nome: "Laura", sobrenome: "Antonia", telefone: "55668899", dataNasc: DateTime.Parse("2005-05-28")) { DataInic = dataFixa },
            new Aluno(id: 4, matricula: 4, nome: "Luiza", sobrenome: "Maria", telefone: "6565659", dataNasc: DateTime.Parse("2005-05-28")) { DataInic = dataFixa },
            new Aluno(id: 5, matricula: 5, nome: "Lucas", sobrenome: "Machado", telefone: "565685415", dataNasc: DateTime.Parse("2005-05-28")) { DataInic = dataFixa },
            new Aluno(id: 6, matricula: 6, nome: "Pedro", sobrenome: "Alvares", telefone: "456454545", dataNasc: DateTime.Parse("2005-05-28")) { DataInic = dataFixa },
            new Aluno(id: 7, matricula: 7, nome: "Paulo", sobrenome: "José", telefone: "9874512", dataNasc: DateTime.Parse("2005-05-28")) { DataInic = dataFixa }
            });

        modelBuilder.Entity<AlunoDisciplina>()
            .HasData(new List<AlunoDisciplina>() {
            new AlunoDisciplina(alunoId: 1, disciplinaId: 2),
            new AlunoDisciplina(alunoId: 1, disciplinaId: 4),
            new AlunoDisciplina(alunoId: 1, disciplinaId: 5),
            new AlunoDisciplina(alunoId: 2, disciplinaId: 1),
            new AlunoDisciplina(alunoId: 2, disciplinaId: 2),
            new AlunoDisciplina(alunoId: 2, disciplinaId: 5),
            new AlunoDisciplina(alunoId: 3, disciplinaId: 1),
            new AlunoDisciplina(alunoId: 3, disciplinaId: 2),
            new AlunoDisciplina(alunoId: 3, disciplinaId: 3),
            new AlunoDisciplina(alunoId: 4, disciplinaId: 1),
            new AlunoDisciplina(alunoId: 4, disciplinaId: 4),
            new AlunoDisciplina(alunoId: 4, disciplinaId: 5),
            new AlunoDisciplina(alunoId: 5, disciplinaId: 4),
            new AlunoDisciplina(alunoId: 5, disciplinaId: 5),
            new AlunoDisciplina(alunoId: 6, disciplinaId: 1),
            new AlunoDisciplina(alunoId: 6, disciplinaId: 2),
            new AlunoDisciplina(alunoId: 6, disciplinaId: 3),
            new AlunoDisciplina(alunoId: 6, disciplinaId: 4),
            new AlunoDisciplina(alunoId: 7, disciplinaId: 1),
            new AlunoDisciplina(alunoId: 7, disciplinaId: 2),
            new AlunoDisciplina(alunoId: 7, disciplinaId: 3),
            new AlunoDisciplina(alunoId: 7, disciplinaId: 4),
            new AlunoDisciplina(alunoId: 7, disciplinaId: 10)
            });
    }
}

