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
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    })
    return this.http.get<Tarefa[]>(this.url, { headers });
  }

  getById(id: number) {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    })
    return this.http.get<Tarefa>(this.url + '/' + id, { headers });
  }

  create(tarefa: Tarefa) {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    })
    return this.http.post(this.url, tarefa, { headers });
  }

  update(tarefa: Tarefa){
    var body = {
      titulo: tarefa.titulo,
      descricao: tarefa.descricao,
      status: tarefa.status,
      prioridade: tarefa.prioridade
    }
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    })
    return this.http.put(this.url + '/' + tarefa.id, body, { headers }  );
  }

  delete(id: number) {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    })
    return this.http.delete(this.url + '/' + id, { headers });
  }
}
