import { Component, OnInit } from '@angular/core';
import { AlunoService } from 'src/app/services/aluno.service'; 
import { ProfessorService } from 'src/app/services/professor.service';
import { Aluno } from 'src/app/models/Aluno';
import { Professor } from 'src/app/models/Professor';
import { PaginatedResult } from 'src/app/models/Pagination';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  public titulo = 'Dashboard';
  public alunos: Aluno[] = [];
  public professores: Professor[] = [];

  constructor(
    private alunoService: AlunoService,
    private professorService: ProfessorService
  ) { }

  ngOnInit() {
    this.carregarDados();
  }

  carregarDados() {
    // Busca a lista de alunos e armazena no array
    this.alunoService.getAll().subscribe(
      (paginatedResult: PaginatedResult<Aluno[]>) => {
        this.alunos = paginatedResult.result;
      },
      error => console.error('Erro ao carregar alunos:', error)
    );

    // Busca a lista de professores e armazena no array
    this.professorService.getAll().subscribe(
      (professores: Professor[]) => {
        this.professores = professores;
      },
      error => console.error('Erro ao carregar professores:', error)
    );
  }
}