import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { TarefasService } from './tarefas.service';
import { Tarefa } from '../models/tarefa.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tarefas',
  imports: [CommonModule],
  templateUrl: './tarefas.component.html',
  styleUrl: './tarefas.component.css',
})
export class TarefasComponent implements OnInit{

  tarefas: Tarefa[] = [];

  constructor(
    private tarefasService: TarefasService, 
    private cd: ChangeDetectorRef
  ) { }

  ngOnInit(){
    this.tarefasService.listAll().subscribe((tarefas) => {
      this.tarefas = tarefas;
      this.cd.markForCheck();
    })
  }

}
