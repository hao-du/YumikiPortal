import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { Project } from '../models/project.model.js'

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class ProjectService {
    constructor(private httpClient: HttpClient) { }

    getProjects(): Observable<Project[]> {
        return this.httpClient.get<Project[]>('/api/ontime/project/getall');
    }

    getProject(id : string): Observable<Project> {
        return this.httpClient.get<Project>('/api/ontime/project/get?id=' + id);
    }

    saveProject(project: Project): Observable<Project> {
        return this.httpClient.post<Project>('/api/ontime/project/save', project, httpOptions);
    }
}
