import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { BaseService } from '../base/base.service.js'
import { User } from '../models/user.model.js'

@Injectable()
export class AssignmentService extends BaseService {
    constructor(httpClient: HttpClient) {
        super(httpClient);
    }

    getUsers(): Observable<User[]> {
        return this.doGet<User[]>('/api/ontime/assignment/getusers');
    }

    getAssignments(userID:string): Observable<User> {
        return this.doGet<User>('/api/ontime/assignment/getassignments?userID=' + userID);
    }
}
