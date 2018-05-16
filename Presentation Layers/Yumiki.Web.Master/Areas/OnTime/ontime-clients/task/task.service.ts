import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { BaseService } from '../base/base.service.js'
import { Task } from '../models/task.model.js'

@Injectable()
export class TaskService extends BaseService {
    constructor(httpClient: HttpClient) {
        super(httpClient);
    }

    getTasks(): Observable<Task[]> {
        return this.doGet<Task[]>('/api/ontime/task/getall');
    }

    getTask(id: string): Observable<Task> {
        return this.doGet<Task>('/api/ontime/task/get?id=' + id);
    }

    saveTask(task: Task): Observable<Task> {
        return this.doPost<Task>('/api/ontime/task/save', task);
    }
}
