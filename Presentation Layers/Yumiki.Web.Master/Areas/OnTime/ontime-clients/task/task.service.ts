import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { BaseService } from '../base/base.service.js'
import { Task } from '../models/task.model.js'
import { PagingTask } from '../models/task.paging.model.js'

@Injectable()
export class TaskService extends BaseService {
    constructor(httpClient: HttpClient) {
        super(httpClient);
    }

    getTasks(getDefaultMetadata:boolean, taskType: number, phaseID: string, projectID: string): Observable<PagingTask> {
        return this.doGet<PagingTask>('/api/ontime/task/gettasks'
            + '?getDefaultMetadata= ' + getDefaultMetadata
            + '&taskType=' + taskType
            + '&phaseID=' + phaseID
            + '&projectID=' + projectID);
    }

    getTask(id: string): Observable<Task> {
        return this.doGet<Task>('/api/ontime/task/get?id=' + id);
    }

    saveTask(task: Task): Observable<Task> {
        return this.doPost<Task>('/api/ontime/task/save', task);
    }
}
