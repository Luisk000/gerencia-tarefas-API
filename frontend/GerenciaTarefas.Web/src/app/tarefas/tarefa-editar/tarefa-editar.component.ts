import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { Tarefa } from '../../models/tarefa.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TarefasService } from '../../services/tarefas.service';

@Component({
  selector: 'app-tarefa-editar',
  imports: [CommonModule, FormsModule],
  templateUrl: './tarefa-editar.component.html',
  styleUrls: [
    './tarefa-editar.component.css',
    '../tarefas.component.css'
  ],
})
export class TarefaEditar{
  @Input() tarefa!: Tarefa;
  @Output() cancelaEmit = new EventEmitter();
  @Output() confirmaEmit = new EventEmitter();

  cancelarEdicao(){
    this.cancelaEmit.emit();
  }

  confirmarEdicao(){
    this.confirmaEmit.emit();
  }
}
