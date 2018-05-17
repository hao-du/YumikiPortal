import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { BaseService } from '../base/base.service.js'
import { Task } from '../models/task.model.js'
import { TaskDashboard } from '../models/taskdashboard.model.js'

@Injectable()
export class TaskService extends BaseService {
    constructor(httpClient: HttpClient) {
        super(httpClient);
    }

    getTaskDashboard(): Observable<TaskDashboard> {
        return this.doGet<TaskDashboard>('/api/ontime/task/getdashboard');
    }

    getTasks(taskType: number): Observable<Task[]> {
        return this.doGet<Task[]>('/api/ontime/task/gettasks?taskType=' + taskType);
    }

    getTask(id: string): Observable<Task> {
        return this.doGet<Task>('/api/ontime/task/get?id=' + id);
    }

    saveTask(task: Task): Observable<Task> {
        return this.doPost<Task>('/api/ontime/task/save', task);
    }
}
