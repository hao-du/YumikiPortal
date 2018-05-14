import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
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
        return this.httpClient.get<T>(url)
            .catch(this.errorHandler);
    }

    doPost<T>(url: string, obj: T): Observable<T> {
        return this.httpClient.post<T>(url, obj, httpOptions)
            .catch(this.errorHandler);
    }

    errorHandler(response: HttpErrorResponse) {
        console.log(response);

        return Observable.throw(response.error || 'Server error');
    }
}
