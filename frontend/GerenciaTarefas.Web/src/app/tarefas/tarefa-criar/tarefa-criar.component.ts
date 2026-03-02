import { ChangeDetectorRef, Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Tarefa } from '../../models/tarefa.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TarefasService } from '../../services/tarefas.service';
import { MetadataService } from '../../services/metadata.service';

@Component({
  selector: 'app-tarefa-criar',
  imports: [CommonModule, FormsModule],
  templateUrl: './tarefa-criar.component.html',
  styleUrls: [
    './tarefa-criar.component.css',
    '../tarefas.component.css'
  ],
})
export class TarefaCriar implements OnInit{

  @Output() updateEmitter = new EventEmitter();

  adicionando = false;
  tarefa: Tarefa = new Tarefa();
  prioridades: string[] = [];

  constructor(
    private tarefasService: TarefasService,
    private metadataService: MetadataService
  ) { }

  ngOnInit(): void {
    this.metadataService.getPrioridades().subscribe((data) => {
      this.prioridades = data;
    })
  }

  cancelarAdicao(){
    this.adicionando = false;
  }

  confirmarAdicao(){
    this.tarefasService.create(this.tarefa).subscribe(() => {
      this.adicionando = false;
      this.tarefa = new Tarefa();
      this.updateEmitter.emit();
    });
  }
}
