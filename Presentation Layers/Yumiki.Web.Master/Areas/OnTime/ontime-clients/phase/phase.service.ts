import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { BaseService } from '../base/base.service.js'
import { Phase } from '../models/phase.model.js'

@Injectable()
export class PhaseService extends BaseService {
    constructor(httpClient: HttpClient) {
        super(httpClient);
    }

    getPhases(isActive: boolean): Observable<Phase[]> {
        return this.doGet<Phase[]>('/api/ontime/phase/getall?isActive=' + isActive);
    }

    getPhase(id: string): Observable<Phase> {
        return this.doGet<Phase>('/api/ontime/phase/get?id=' + id);
    }

    savePhase(phase: Phase): Observable<Phase> {
        return this.doPost<Phase>('/api/ontime/phase/save', phase);
    }
}
