import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { BaseService } from '../base/base.service.js'
import { Task } from '../models/task.model.js'
import { TaskDashboard } from '../models/taskdashboard.model.js'
import { TaskPaging } from '../models/taskpaging.model.js'

@Injectable()
export class TaskService extends BaseService {
    constructor(httpClient: HttpClient) {
        super(httpClient);
    }

    getTaskDashboard(phaseID: string, projectID: string): Observable<TaskDashboard> {
        return this.doGet<TaskDashboard>('/api/ontime/task/getdashboard?phaseID=' + phaseID + '&projectID=' + projectID);
    }

    getTasks(taskType: number, phaseID: string, projectID: string): Observable<TaskPaging> {
        return this.doGet<TaskPaging>('/api/ontime/task/gettasks?taskType=' + taskType + '&phaseID=' + phaseID + '&projectID=' + projectID);
    }

    getTask(id: string): Observable<Task> {
        return this.doGet<Task>('/api/ontime/task/get?id=' + id);
    }

    saveTask(task: Task): Observable<Task> {
        return this.doPost<Task>('/api/ontime/task/save', task);
    }
}
