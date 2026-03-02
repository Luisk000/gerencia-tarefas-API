import { ChangeDetectorRef, Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Tarefa } from '../../models/tarefa.model';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { TarefasService } from '../../services/tarefas.service';
import { MetadataService } from '../../services/metadata.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-tarefa-criar',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './tarefa-criar.component.html',
  styleUrls: [
    './tarefa-criar.component.css',
    '../tarefas.component.css'
  ],
})
export class TarefaCriar implements OnInit{

  @Output() updateEmitter = new EventEmitter();
  @Output() closeTarefasEmitter = new EventEmitter();

  adicionando = false;
  tarefaForm!: FormGroup;
  prioridades: string[] = [];

  constructor(
    private tarefasService: TarefasService,
    private metadataService: MetadataService,
    private toastr: ToastrService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.initForm()
    this.metadataService.getPrioridades().subscribe((data) => {
      this.prioridades = data;
    }, async (error) => {
      console.log(error)   
      this.toastr.error(error.message)
    })
  }

  initForm(){
    this.tarefaForm = this.formBuilder.group({
      titulo: ["", [Validators.required]],
      descricao: ["", [Validators.required]],
      prioridade: ["", [Validators.required]]
    })
  }

  startAdicionando(){
    this.adicionando = true;
    this.initForm();
    this.closeTarefasEmitter.emit();
  }

  cancelarAdicao(){
    this.adicionando = false;
  }

  confirmarAdicao(){
    var tarefa = new Tarefa(
      this.tarefaForm.get('titulo')?.value,
      this.tarefaForm.get('descricao')?.value,
      this.tarefaForm.get('prioridade')?.value,
    );

    this.tarefasService.create(tarefa).subscribe(() => {
      this.adicionando = false;
      this.initForm();
      this.toastr.success("Tarefa adicionada")
      this.updateEmitter.emit();
    }, async (error) => {
      console.log(error)   
      this.toastr.error(error.message)
    });
  }
}
