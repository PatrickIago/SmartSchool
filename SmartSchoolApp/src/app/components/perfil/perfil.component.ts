import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent implements OnInit {
  public titulo = 'Perfil';
  public perfilForm!: FormGroup;

  public usuarioFake = {
    nome: 'Usuario teste',
    sobrenome: 'teste',
    email: 'teste@admin.com',
    funcao: 'Administrador'
  };

  constructor(private fb: FormBuilder, private toastr: ToastrService) { }

  ngOnInit() {
    this.criarFormulario();
  }

  criarFormulario() {
    this.perfilForm = this.fb.group({
      nome: [this.usuarioFake.nome, Validators.required],
      sobrenome: [this.usuarioFake.sobrenome, Validators.required],
      email: [this.usuarioFake.email, [Validators.required, Validators.email]]
    });
  }

  // Resolve o erro 'Property salvarPerfil does not exist'
  salvarPerfil() {
    if (this.perfilForm.valid) {
      this.toastr.success('Perfil atualizado com sucesso!');
    }
  }
}