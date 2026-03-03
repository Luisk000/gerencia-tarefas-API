import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Tarefa } from '../models/tarefa.model';

@Injectable({
  providedIn: 'root',
})
export class TarefasService {

  url = 'https://localhost:44353/api/tarefas';
  
  constructor(private http: HttpClient) { }

  listAll() {
    return this.http.get<Tarefa[]>(this.url);
  }

  getById(id: number) {
    return this.http.get<Tarefa>(this.url + '/' + id);
  }

  create(tarefa: Tarefa) {
    return this.http.post(this.url, tarefa);
  }

  update(tarefa: Tarefa){
    var body = {
      titulo: tarefa.titulo,
      descricao: tarefa.descricao,
      status: tarefa.status,
      prioridade: tarefa.prioridade
    }
    return this.http.put(this.url + '/' + tarefa.id, body);
  }

  delete(id: number) {
    return this.http.delete(this.url + '/' + id);
  }
}
