import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { Project } from '../models/project.model.js'

@Injectable()
export class ProjectService {
    constructor(private httpClient: HttpClient) { }

    getProjects(): Observable<Project[]> {
        return this.httpClient.get<Project[]>('/api/ontime/project/getall');
    }
}
