import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { TarefasService } from '../services/tarefas.service';
import { Tarefa } from '../models/tarefa.model';
import { CommonModule } from '@angular/common';
import { TarefaEditar } from './tarefa-editar/tarefa-editar.component';
import { TarefaCriar } from './tarefa-criar/tarefa-criar.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-tarefas',
  imports: [CommonModule, TarefaEditar, TarefaCriar],
  templateUrl: './tarefas.component.html',
  styleUrl: './tarefas.component.css',
})
export class TarefasComponent implements OnInit{

  tarefas: Tarefa[] = [];
  selectedTarefa: Tarefa | null = null;
  adicionando = false;
  editando = false;
  excluindo = false;

  constructor(
    private tarefasService: TarefasService,
    private toastr: ToastrService,
    private cd: ChangeDetectorRef
  ) { }

  ngOnInit(){
    this.listAll();
  }

  listAll(){
    this.tarefasService.listAll().subscribe((data) => {
      this.tarefas = data.sort((a, b) => a.id - b.id);
      this.cd.markForCheck();
    })
  }

  getDadosTarefa(tarefa: Tarefa){
    this.editando = false;
    this.excluindo = false;

    if (this.selectedTarefa && this.selectedTarefa.id === tarefa.id){
      this.selectedTarefa = null;
      return;
    }

    this.selectedTarefa = tarefa;
    this.tarefasService.getById(tarefa.id).subscribe((data) => {
      this.selectedTarefa = data;
      this.cd.markForCheck();
    }, async (error) => {
      console.log(error)   
      this.toastr.error(error.message)
    })
  }

  changeEditando(){
    this.editando = !this.editando;
    this.excluindo = false;
  }

  changeExcluindo(){
    this.editando = false;
    this.excluindo = !this.excluindo;
  }

  deleteTarefa(tarefa: Tarefa){
    this.tarefasService.delete(tarefa.id).subscribe(() => {
      var index = this.tarefas.findIndex(t => t.id == tarefa.id)
      this.tarefas.splice(index, 1)
      this.toastr.success("Tarefa apagada")
      this.cd.markForCheck();
    }, async (error) => {
      console.log(error)   
      this.toastr.error(error.message)
    })
  }

  confirmarEdicao(tarefa: Tarefa){
    this.tarefasService.update(tarefa).subscribe(() => {
      this.editando = false;
      this.selectedTarefa = tarefa;
      var index = this.tarefas.findIndex(t => t.id == tarefa.id)
      this.tarefas[index] = tarefa;
      this.toastr.success("Tarefa atualizada")
      this.cd.markForCheck();
    }, async (error) => {
      console.log(error)  
      this.toastr.error(error.message) 
    })

  }

  updateCriacao(){
    this.listAll()
  }

  closeTarefas(){
    this.selectedTarefa = null;
    this.editando = false; 
    this.excluindo = false
  }
}
