import { ApiResponse } from '../models/api-response';
import { HttpClient, HttpParams } from '@angular/common/http';
import { API_URL, API_VERSION } from '../injection.tokens';
import { Inject, Injectable } from '@angular/core';
import { ClienteModel } from "../models/cliente";
import { Observable } from 'rxjs';
import { ListOptions } from '../models/list.options';
import { PageResponse } from '../models/page-response';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClienteService
{
  baseUrl: string = 'Cliente';

  constructor(
    private http: HttpClient,
    @Inject(API_URL) private apiUrl: string,
    @Inject(API_VERSION) private apiVersion: string
  ) {
    this.baseUrl = `${this.apiUrl}/${this.apiVersion}/${this.baseUrl}`;
  }

  listAll(options: ListOptions): Observable<PageResponse<ClienteModel>> {
    const body = options.toBody();

    const prm = new HttpParams({
      fromObject: body
    });

    return this.http
      .get<PageResponse<ClienteModel>>(`${this.baseUrl}/paginated`, { params: prm });
  }
  

  save(data: ClienteModel) : Observable<ApiResponse<ClienteModel>> {
    if(data.id) {
      return this
        .http
        .put(this.baseUrl + '/' + data.id, data)
        .pipe(
          map((response:any) => response as ApiResponse<ClienteModel>)
        )
    }
    else {
        return this
          .http
          .post(this.baseUrl, data)
          .pipe(
            map((response:any) => response as ApiResponse<ClienteModel>)
          )
    };
  }

  delete(id: number) : Observable<ApiResponse<ClienteModel>> {
    return this
      .http
      .delete(this.baseUrl + '/' + id)
      .pipe(
        map((response:any) => response as ApiResponse<ClienteModel>)
      )
  }
}
