import { Component, OnInit, OnDestroy, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Aluno } from 'src/app/models/Aluno';
import { Professor } from 'src/app/models/Professor';
import { ProfessorService } from 'src/app/services/professor.service';
import { AlunoService } from 'src/app/services/aluno.service';
import { PaginatedResult, Pagination } from 'src/app/models/Pagination';

@Component({
  selector: 'app-alunos',
  templateUrl: './alunos.component.html',
  styleUrls: ['./alunos.component.css']
})
export class AlunosComponent implements OnInit, OnDestroy {

  public modalRef!: BsModalRef;
  public alunoForm!: FormGroup;
  public titulo = 'Alunos';
  public alunoSelecionado?: Aluno | null;
  public textSimple!: string;
  public profsAlunos!: Professor[]; 
  public alunos: Aluno[] = []; 
  public aluno!: Aluno;
  public pagination!: Pagination; 
  public msnDeleteAluno!: string;
  public modeSave = 'post';

  private unsubscriber = new Subject();

  constructor(
    private alunoService: AlunoService,
    private route: ActivatedRoute,
    private professorService: ProfessorService,
    private fb: FormBuilder,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) {
    this.criarForm();
  }

  ngOnInit(): void {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 5, 
      totalItems: 0,
      totalPages: 0       
    } as Pagination;
    this.carregarAlunos();
  }

  carregarAlunos(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    const id = idParam ? +idParam : 0;
    
    this.spinner.show();

    this.alunoService.getAll(this.pagination.currentPage, this.pagination.itemsPerPage)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe(
        (paginatedResult: PaginatedResult<Aluno[]>) => {
          this.alunos = paginatedResult.result;


          if (paginatedResult.pagination) {
            this.pagination = paginatedResult.pagination;
          }

          if (id > 0) {
            const alunoEncontrado = this.alunos.find(a => a.id === id);
            if (alunoEncontrado) {
              this.alunoSelect(alunoEncontrado.id);
            }
          }
        },
        (error: any) => {
          this.toastr.error('Alunos não carregados!');
          console.error(error);
        },
        () => this.spinner.hide() 
      );
  }

  professoresAlunos(template: TemplateRef<any>, id: number): void {
    this.spinner.show();
    this.professorService.getByAlunoId(id)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe(
        (professores: Professor[]) => {
          if (!professores || professores.length === 0) {
            this.spinner.hide();
            this.toastr.warning('Nenhum professor encontrado para este aluno', 'Atenção');
            return;
          }
          this.profsAlunos = professores;
          this.modalRef = this.modalService.show(template);
          this.spinner.hide();
        },
        (error: any) => {
          this.spinner.hide();
          if (this.modalRef) this.modalRef.hide();
          this.toastr.error(`Erro: ${error.message}`);
          console.error(error);
        }
      );
  }

  criarForm(): void {
    this.alunoForm = this.fb.group({
      id: [0],
      nome: ['', Validators.required],
      sobrenome: ['', Validators.required],
      telefone: ['', Validators.required],
      ativo: []
    });
  }

  trocarEstado(aluno: Aluno): void {
    this.spinner.show();
    const novoEstado = !aluno.ativo;
    this.alunoService.trocarEstado(aluno.id, novoEstado)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe(
        () => {
          aluno.ativo = novoEstado; 
          const acao = aluno.ativo ? 'ativado' : 'desativado';
          this.toastr.success(`Aluno ${acao} com sucesso!`); 
        },
        (error: any) => {
          this.toastr.error(`Erro ao tentar alterar o estado do aluno!`); 
          console.error(error); 
        },
        () => this.spinner.hide() 
      );
  }

  saveAluno(): void {
    if (this.alunoForm.valid) {
      this.spinner.show();
      if (this.modeSave === 'post') {
        this.aluno = { ...this.alunoForm.value };
      } else {
        this.aluno = { id: this.alunoSelecionado!.id, ...this.alunoForm.value };
      }

      const execucao = (this.modeSave === 'post')
        ? this.alunoService.post(this.aluno)
        : this.alunoService.put(this.aluno);

      execucao
        .pipe(takeUntil(this.unsubscriber))
        .subscribe(
          () => {
            this.carregarAlunos();
            this.toastr.success('Aluno salvo com sucesso!');
          },
          (error: any) => {
            this.toastr.error(`Erro: Aluno não pode ser salvo!`);
            console.error(error);
          },
          () => this.spinner.hide()
        );
    }
  }

  alunoSelect(alunoId: number): void { 
    this.modeSave = 'put';
    this.spinner.show();
    this.alunoService.getById(alunoId).subscribe(
      (alunoReturn: Aluno) => {
        this.alunoSelecionado = alunoReturn;
        this.alunoForm.patchValue(this.alunoSelecionado);
      },
      (error) => {
        this.toastr.error("Erro ao carregar aluno");
      },
      () => this.spinner.hide()
    );
  }

  voltar(): void {
    this.alunoSelecionado = null;
  }

  openModal(template: TemplateRef<any>, alunoId: number): void {
    this.professoresAlunos(template, alunoId);
  }

  closeModal(): void {
    this.modalRef.hide();
  }

  ngOnDestroy(): void {
    this.unsubscriber.next();
    this.unsubscriber.complete();
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.carregarAlunos();
  } 
}