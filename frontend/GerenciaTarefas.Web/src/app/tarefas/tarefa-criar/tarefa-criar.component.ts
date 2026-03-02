import { ChangeDetectorRef, Component } from '@angular/core';
import { Tarefa } from '../../models/tarefa.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TarefasService } from '../tarefas.service';

@Component({
  selector: 'app-tarefa-criar',
  imports: [CommonModule, FormsModule],
  templateUrl: './tarefa-criar.component.html',
  styleUrls: [
    './tarefa-criar.component.css',
    '../tarefas.component.css'
  ],
})
export class TarefaCriar {
  adicionando = false;
  tarefa: Tarefa = new Tarefa();

  constructor(private tarefasService: TarefasService, private cd: ChangeDetectorRef ) { 
  }

  cancelarAdicao(){
    this.adicionando = false;
  }

  confirmarAdicao(){
    this.tarefasService.create(this.tarefa).subscribe(() => {
      this.adicionando = false;
      this.cd.markForCheck();
    });
  }
}
