import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import 'rxjs/add/observable/throw';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

export class BaseService {
    protected httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this.httpClient = httpClient;
    }

    doGet<T>(url: string): Observable<T> {
        return this.httpClient.get<T>(url).pipe(catchError(this.errorHandler));
    }

    doPost<T>(url: string, obj: T): Observable<T> {
        return this.httpClient.post<T>(url, obj, httpOptions).pipe(catchError(this.errorHandler));
    }

    errorHandler(response: HttpErrorResponse) {
        return Observable.throw(response.error || 'Server error');
    }
}
