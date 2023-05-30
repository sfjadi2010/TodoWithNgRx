import { Component, OnInit } from '@angular/core';
import { TodosService } from './_services/todos.service';
import { Task } from './_models/task';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'TodoUx';
  tasks: Task[] = [];

  constructor(private todosService: TodosService) { }

  ngOnInit() {
    this.todosService.getTodos().subscribe((data) => {
      this.tasks = data;
    });
  }
}
