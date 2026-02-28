import { Component, signal } from '@angular/core';
import { TarefasComponent } from './tarefas/tarefas.component';

@Component({
  selector: 'app-root',
  imports: [TarefasComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('GerenciaTarefas.Web');
}
