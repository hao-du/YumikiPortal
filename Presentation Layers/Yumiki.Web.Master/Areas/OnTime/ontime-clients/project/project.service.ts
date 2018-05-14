import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { BaseService } from '../base/base.service.js'
import { Project } from '../models/project.model.js'

@Injectable()
export class ProjectService extends BaseService {
    constructor(httpClient: HttpClient) {
        super(httpClient);
    }

    getProjects(isActive: boolean): Observable<Project[]> {
        return this.doGet<Project[]>('/api/ontime/project/getall?isActive=' + isActive);
    }

    getProject(id: string): Observable<Project> {
        return this.doGet<Project>('/api/ontime/project/get?id=' + id);
    }

    saveProject(project: Project): Observable<Project> {
        return this.doPost<Project>('/api/ontime/project/save', project);
    }
}
