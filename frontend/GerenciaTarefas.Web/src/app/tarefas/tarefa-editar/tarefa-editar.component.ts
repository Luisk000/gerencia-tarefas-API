import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { Tarefa } from '../../models/tarefa.model';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-tarefa-editar',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './tarefa-editar.component.html',
  styleUrls: [
    './tarefa-editar.component.css',
    '../tarefas.component.css'
  ],
})
export class TarefaEditar implements OnInit{
  @Input() tarefa!: Tarefa;
  @Output() cancelaEmit = new EventEmitter();
  @Output() confirmaEmit = new EventEmitter<Tarefa>();

  tarefaForm!: FormGroup;

  constructor(private formBuilder: FormBuilder){}

  ngOnInit() {
    this.initForm()
  }

  initForm(){
    this.tarefaForm = this.formBuilder.group({
      titulo: [this.tarefa.titulo, [Validators.required]],
      descricao: [this.tarefa.descricao, [Validators.required]],
      prioridade: [this.tarefa.prioridade, [Validators.required]],
      status: [this.tarefa.status, [Validators.required]]
    })
  }

  cancelarEdicao(){
    this.cancelaEmit.emit();
  }

  confirmarEdicao(){
    var tarefa = new Tarefa(
      this.tarefaForm.get('titulo')?.value,
      this.tarefaForm.get('descricao')?.value,
      this.tarefaForm.get('prioridade')?.value,
      this.tarefaForm.get('status')?.value
    )
    tarefa.id = this.tarefa.id;
    this.confirmaEmit.emit(tarefa);
  }
}
