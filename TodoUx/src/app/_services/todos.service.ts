import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from '../_models/task';

@Injectable({
  providedIn: 'root'
})
export class TodosService {

  todoApiUrl: string = 'http://localhost:5184/';

  constructor(private http: HttpClient) { }

  getTodos() : Observable<Task[]>{
    return this.http.get<Task[]>(this.todoApiUrl + 'todoitems');
  }
}
